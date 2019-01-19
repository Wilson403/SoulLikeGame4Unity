using System;
using System.Collections;
using System.Collections.Generic;
using M_ControllerSystem;
using UnityEngine;
using GameAttr.AttrStrategy;
using GameAttr.CharactorAttr;
using GameAttr.WeaponAttr;
using M_AnimationManager.PlayerAnimation;
using UnityEngine.EventSystems;

namespace M_CharactorSystem
{
	public class Player : ICharactor
	{
		public bool LeftIsShield = true; //是否左手持盾
		public bool BLockmoving = false;
		public bool BLock = false;
		public CameraControl CameraControl;
		public float JumpHeight; //跳跃高度，变量_jumpHeight的y轴分量
		public bool BArmedSword = false;

		[Header("===剑设置===")] 
		public GameObject SwordPos;
		public GameObject Sword;

		[Header("===盾设置===")] 
		public GameObject ShieldPos;
		public GameObject Shield;

		private float _currentValue; //动画权重的当前值
		private Vector3 _jumpVec; //跳跃高度
		private Vector3 _rollVec; //翻滚速度
		private Vector3 _deltaPos; //根运动量
		private Vector3 _stepVec;
		private IController _controller;
		private ControllerState _controllerState; //控制器状态
		
		public IController Controller
		{
			get { return _controller; }
		}



	#region 枚举（控制器状态）

		public enum ControllerState
		{
			JoyStrick,
			KeyBoard
		}

	#endregion

		private void Awake()
		{
			//初始化控制器,默认为手柄
			_controller = new JoyStrickInput();

			//初始化控制器状态，默认为手柄状态
			_controllerState = ControllerState.JoyStrick;

			//获取人物模型
			MyModel = this.transform.Find("ybot").gameObject;

			//获取角色控制柄下的刚体组件
			Rig = this.GetComponent<Rigidbody>();

			//获取角色控制柄下的碰撞体组件
			Col = this.transform.GetComponent<CapsuleCollider>();

			//获取传感器
			Sensor = this.transform.Find("Sensor").gameObject;

			//如果模型存在，则获取模型下的动画状态机
			if (MyModel)
			{
				MyAnimator = MyModel.GetComponent<Animator>();
			}

			//角色属性初始化
			SetCharactorAttr(new PlayerAttr());
			CharactorAttr.SetAttrStrategy(new PlayerAttrStrategy());
			CharactorAttr.InitAttr();
			
			//武器初始化
			SetWeapon(new WeaponSword());
			WeaponInit();
			
			InitActionManager();
			SetAnimation(new PlayerGeneralAnimationMgr(MyActionManager), new PlayerEqipAnimationMgr(MyActionManager),
				new PlayerUnEqipAnimationMgr(MyActionManager));
		}

		private void Start()
		{
			//首帧就要检查是否位于地面
			CheckBOnGround();

			//状态事件的注册
			EventMgr.Instance.AddListener(EventMgr.EVENT_TYPE.PlayerFsmEnter, EnterEvent); //进入状态
			EventMgr.Instance.AddListener(EventMgr.EVENT_TYPE.PlayerFsmExit, ExitEvent); //离开状态
			EventMgr.Instance.AddListener(EventMgr.EVENT_TYPE.PlayerFsmUpdate, UpdateEvent); //更新状态
		}

		private void Update()
		{
			ChangeController(); //角色控制器自动切换功能
			Controller.Update(); //控制器内部更新逻辑
			BCanMove();
			BFollowObject();
			CheckBOnGround();
			
			MyActionManager.General.ChangeActionState();
			MyActionManager.General.GetMoveAnimation();

			//触发锁定，相机会锁定敌人
			if (Controller.BLock)
			{
				CameraControl.LockUnLock();
			}
			
			//角色方向变更
			//只有摇杆值不为0时才进行角色方向变更
			if (Controller.DMag > 0f)
			{
				//利用线性插值使角色旋转变得平滑
				MyModel.transform.forward =
					Vector3.Slerp(MyModel.transform.forward,
						Controller.GetRVec(transform),
						0.3f);
			}
			
			
			
			
		}

		private void FixedUpdate()
		{
			if (Rig)
			{
				Rig.position += _deltaPos;
				Rig.velocity = new Vector3(MovingVec.x, Rig.velocity.y, MovingVec.z) + _jumpVec +
				               _rollVec + _stepVec;
				_jumpVec = Vector3.zero;
				_deltaPos = Vector3.zero;
			}
		}

	#region 攻击与被攻击抽象层

		//被敌人攻击
		public override void UnderAttack(ICharactor theTarget)
		{
			//计算伤害值
			CharactorAttr.GetDmgDesValue(theTarget);
			if (CharactorAttr.GetNowHp() <= 0)
			{
				Debug.Log("你死了");
			}
		}  

	#endregion

	#region 类私有方法

		//切换控制器
		private void ChangeController()
		{
			int num = 0;
			num = CheckController.CheckCurController();

			if (num == 1 && _controllerState != ControllerState.KeyBoard)
			{
				_controller = new KeyBoardInput();
				_controllerState = ControllerState.KeyBoard;
			}
			else if (num == -1 && _controllerState != ControllerState.JoyStrick)
			{
				_controller = new JoyStrickInput();
				_controllerState = ControllerState.JoyStrick;
			}
			else
			{
				//nothing
			}

		}

		//----------------------------------------------------------------------------------

		//让走路动画启用
		private bool BCanMove()
		{
			if (Controller.DMag > 0f)
			{
				MyAnimator.SetBool("Move", true);
				return true;
			}
			else
			{
				MyAnimator.SetBool("Move", false);
				return false;
			}
		}

		//设置状态机的BFollowObject参数
		//相机是否处于聚焦跟踪状态
		private void BFollowObject()
		{
			if (CameraControl.LockState)
			{
				MyAnimator.SetBool("BFollowObject", true);
			}
			else
			{
				MyAnimator.SetBool("BFollowObject", false);
			}
		}

		//----------------------------------------------------------------------------------

		//移动速度重置
		public void ReGetSpeed()
		{
			MovingVec = Vector3.zero;
		}

	#endregion

	#region 类公开方法

		//使用动画自身的运动
		public void OnUpdateRm(Vector3 delta)
		{
			/*
			 1.如果是轻攻击第一击且处于相机锁定状态，会有很长的一段滑动距离
			 2.滑动距离是根据与锁定物体的距离来动态调整的 
			 */
//			if (_actionManager.CheckState("LAttack_A") && MCameraControl.LockState)
//			{
//				_deltaPos = delta * (MCameraControl.ObjectDistance > 5 ? 5 : MCameraControl.ObjectDistance * 0.6f);
//			}
//			//一般状态下的根运动值
//			else
//			{
//				_deltaPos = delta * 0.5f;
//			}
		}

	#endregion

	#region 动画状态相关方法

		//进入跳跃动画
		private void OnJumpEnter()
		{
			Controller.InputEnable = false;
			_jumpVec = new Vector3(0, JumpHeight, 0);
			BLockmoving = true;
			MovingVec *= 1.5f;

		}

		//进入常态
		private void OnGeneralEnter()
		{
			Controller.InputEnable = true;
			BLockmoving = false;
		}

		//进入跳跃动画
		private void OnFallingEnter()
		{
			Controller.InputEnable = false;
			BLockmoving = false;
		}

		//
		private void OnNormalEnter()
		{
			Controller.InputEnable = false;
			BLockmoving = false;
			Debug.Log("Attack");
		}

		//翻滚动画状态更新时
		private void OnRollUpdate()
		{
			Vector3 tmpZ = MyAnimator.GetFloat("Z") * MyModel.transform.forward;
			Vector3 tmpX = MyAnimator.GetFloat("X") * MyModel.transform.right;
			_rollVec = (tmpX + tmpZ) * 7;
			BLockmoving = true;
			MovingVec = Vector3.zero;
		}

		//翻滚退出时
		private void OnRollExit()
		{
			_rollVec = Vector3.zero;
			BLockmoving = false;
		}

		private void OnStepUpdate()
		{
			Vector3 tmpZ = MyAnimator.GetFloat("Z") * MyModel.transform.forward;
			Vector3 tmpX = MyAnimator.GetFloat("X") * MyModel.transform.right;
			_stepVec = (tmpX + tmpZ) * 6;
			BLockmoving = true;
			MovingVec = Vector3.zero;
		}

		private void OnStepExit()
		{
			_stepVec = Vector3.zero;
			BLockmoving = false;
		}

	#endregion

	#region 动画状态事件

		/// <summary>
		/// StateMachine脚本触发Enter时调用
		/// </summary>
		/// <param name="Event_Type">事件类型</param>
		/// <param name="Sender">发送者对象</param>
		/// <param name="Param">参数，可选</param>
		public void EnterEvent(EventMgr.EVENT_TYPE Event_Type, Component Sender, object Param = null)
		{
			switch ((string) Param)
			{
				case "Normal":
					OnNormalEnter();
					break;
				case "Jump":
					OnJumpEnter();
					break;
				case "General":
					OnGeneralEnter();
					break;
				case "Fall":
					OnFallingEnter();
					break;
				default:
					Debug.Log("EnterEvent Error");
					break;
			}
		}

		/// <summary>
		/// StateMachine脚本触发Exit时调用
		/// </summary>
		/// <param name="Event_Type">事件类型</param>
		/// <param name="Sender">发送者对象</param>
		/// <param name="Param">参数，可选</param>
		public void ExitEvent(EventMgr.EVENT_TYPE Event_Type, Component Sender, object Param = null)
		{
			switch ((string) Param)
			{
				case "Roll":
					OnRollExit();
					break;
				case "Step":
					OnStepExit();
					break;
				default:
					Debug.Log("ExitEvent Error");
					break;
			}
		}

		/// <summary>
		/// StateMachine脚本触发Update时调用
		/// </summary>
		/// <param name="Event_Type">事件类型</param>
		/// <param name="Sender">发送者对象</param>
		/// <param name="Param">参数，可选</param>
		public void UpdateEvent(EventMgr.EVENT_TYPE Event_Type, Component Sender, object Param = null)
		{
			switch ((string) Param)
			{
				case "Roll":
					OnRollUpdate();
					break;
				case "Step":
					OnStepUpdate();
					break;
				default:
					Debug.Log("UpdateEvent Error");
					break;
			}
		}

	#endregion

	} //Class_End
}

