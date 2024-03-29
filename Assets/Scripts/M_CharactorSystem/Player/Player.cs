﻿using System;
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
			
			//状态机动画管理
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
			MyActionManager.General.GetJumpAnimation();
			MyActionManager.Eqip.SetAttackAnimation();
			MyActionManager.Eqip.SetBlockAnimation();

			//触发锁定，相机会锁定敌人
			if (Controller.BLock)
			{
				CameraControl.LockUnLock();
			}

			if (Input.GetKeyDown(KeyCode.Space))
			{
				UnderAttack(this);
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
			var dir = Vector3.Normalize(theTarget.transform.position - transform.position);
			var value = UsefulView(theTarget); 

			//背面
			if (value >= 0 && value < 60)
			{
				//没有找到相应的动画，用正面被攻击的替代
				MyActionManager.General.GetDamageAnimation(0, 0); //触发被攻击动画
			}

			//侧面
			else if (value >= 60 && value < 120)
			{
				//右
				if (dir.x > 0)
				{
					MyActionManager.General.GetDamageAnimation(-1, 0); //触发被攻击动画
				}
				//左
				else if (dir.x < 0)
				{
					MyActionManager.General.GetDamageAnimation(1, 0); //触发被攻击动画
				}
			}
			
			//正面
			else if (value >= 120 && value <= 180)
			{
				MyActionManager.General.GetDamageAnimation(0, 0); //触发被攻击动画
			}
			
			//计算伤害值
			CharactorAttr.GetRemainHp(theTarget);
			if (CharactorAttr.GetNowHp() <= 0)
			{
				MyActionManager.General.GetDeadAnimation(); //触发死亡动画
			}
			
//			Debug.Log("最大生命值" + CharactorAttr.GetMaxHp());
//			Debug.Log("当前生命值" + CharactorAttr.GetNowHp());
//			Debug.Log("当前伤害" + GetAtkValue());
			
		}
		
		public override int GetAtkValue()
		{
			return base.GetAtkValue() + GetWeaponAtkValue(1);
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
			MyActionManager.General.RootMotionValue(delta, out _deltaPos);
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

