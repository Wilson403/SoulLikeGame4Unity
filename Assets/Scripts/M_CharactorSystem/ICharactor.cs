using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AIMgr.CharactorAIMgr;
using BaseClass;
using GameAttr.CharactorAttr;
using M_AnimationManager;
using M_ControllerSystem;
using M_Factory;
using M_WeaponSystem;
using UnityEngine.AI;

namespace M_CharactorSystem
{
	public abstract class ICharactor
	{
		//名字
		protected string m_Name;

		//模型名
		protected string m_Assetname;

		//角色控制柄
		protected GameObject m_CharactorHandle;
		
		//角色模型
		protected GameObject m_Model = null;

		//角色ID
		protected int m_Id;

		protected int m_AttrID;
		
		//左手武器
		protected IWeapon m_LWeapon = null;
		
		//右手武器
		protected IWeapon m_RWeapon = null;

		//属性
		protected ICharactorAttr m_Attribute = null;

		//AI
		protected ICharactorAI m_AI = null;

		protected CameraControl m_CameraControl;
		
		//控制器对象
		protected IController m_Controller;
		
		//控制器状态
		protected ControllerState m_ControllerState; 

		protected GameObject m_CameraPoint;

		//碰撞体
		protected CapsuleCollider m_Col;

		//刚体
		protected Rigidbody m_Rig;

		//动画组件
		protected Animator m_Animator;

		//传感器,用于检测地面
		protected GameObject Sensor;

		//动作管理器
		protected ActionManager MyActionManager;

		//射线长度
		private const float _raylength = 2f;
		

		public float WalkSpeed; //走路（散步）速度
		public float RunSpeed; //奔跑速度
		public float JumpHeight; //跳跃高度，变量_jumpHeight的y轴分量
		
		//武器手持挂载点
		public Transform m_Lweapontrans;
		public Transform m_Rweapontrans;
		
		//武器背部挂载点
		public Transform m_SwordPos;
		public Transform m_ShieldPos;

		
		protected NavMeshAgent _agent; //代理组件

		public abstract void Init();
		public abstract void Update();

		public void EnableWeaponCol(bool isEnable)
		{
			m_RWeapon.WeaponEnable(isEnable);
		}

		//设置模型-
		public virtual void SetCharactorModel(GameObject go)
		{
			m_CharactorHandle = go;
			m_Model = m_CharactorHandle.transform.Find("model").gameObject;
			
			Sensor = m_CharactorHandle.transform.Find("Sensor").gameObject;
			m_Col = m_CharactorHandle.GetComponent<CapsuleCollider>();
			m_Rig = m_CharactorHandle.GetComponent<Rigidbody>();
			m_Animator = m_Model.GetComponent<Animator>();
		}
		
		
		//设置武器的挂载点信息
		protected virtual void SetWeaponPoint()
		{
			m_Lweapontrans = UnityTool.DeepFind(m_Model.transform, "weaponHandleL").transform;
			m_Rweapontrans = UnityTool.DeepFind(m_Model.transform, "weaponHandleR").transform;

			m_SwordPos = UnityTool.DeepFind(m_Model.transform, "SwordPos").transform;
			m_ShieldPos = UnityTool.DeepFind(m_Model.transform, "ShieldPos").transform;
		}

		public GameObject GetCharactorHandle()
		{
			return m_CharactorHandle;
		}

		//取得模型
		public GameObject GetModel()
		{
			return m_Model;
		}

		//取得相机点
		public GameObject GetCameraPoint()
		{
			return m_CameraPoint;
		}

		public void SetCameraControl(CameraControl theCameraControl)
		{
			m_CameraControl = theCameraControl;
		}

		public CameraControl GetCameraControl()
		{
			return m_CameraControl;
		}

		public void SetController(IController theController,ControllerState state)
		{
			m_Controller = theController;
			m_ControllerState = state;
		}

		//取得模型名
		public string GetAssetName()
		{
			return m_Assetname;
		}

		public Animator GetAnimator()
		{
			return m_Animator;
		}

		//销毁模型
		public void Release()
		{
			if (m_Model != null)
				Object.Destroy(m_Model);
		}

		//取得名字
		public string GetName()
		{
			return m_Name;
		}

		public int GetAttrID()
		{
			return m_AttrID;
		}

		public void SetID(int id)
		{
			m_Id = id;
		}

		public int GetID()
		{
			return m_Id;
		}


		public void SetActionManager(ActionManager theActionManager)
		{
			MyActionManager = theActionManager;
		}

		//设置左手武器
		public void SetLWeapon(IWeapon weapon)
		{
			m_LWeapon = weapon;
			m_LWeapon.SetWeaponOwner(this);
		}

		//设置右手武器
		public void SetRWeapon(IWeapon weapon)
		{
			m_RWeapon = weapon;
			m_RWeapon.SetWeaponOwner(this);
		    
		}

		//取得武器攻击力
		public int GetWeaponAtkValue(int index)
		{
			int value;
			switch (index)
			{
				case 0:
					value = m_LWeapon.GetAtkValue();
					break;
				case 1:
					value = m_RWeapon.GetAtkValue();
					break;
				default:
					value = 0;
					break;
			}

			return value;
		}

		//取得武器防御力
		public int GetDefenseValue(int index)
		{
			int value;
			switch (index)
			{
				case 0:
					value = m_LWeapon.GetDefenseValue();
					break;
				case 1:
					value = m_RWeapon.GetDefenseValue();
					break;
				default:
					value = 0;
					break;
			}

			return value;
		}

		protected virtual void SetWeaponPos()
		{
			var lweapon = GetLWeaponModel();
			UnityTool.SetParent(m_ShieldPos, lweapon);

			var rweapon = GetRWeaponModel();
			UnityTool.SetParent(m_SwordPos, rweapon);
		}

		public Transform GetLWeaponModel()
		{
			return m_LWeapon.GetGameObject().transform;
		}

		public Transform GetRWeaponModel()
		{
			return m_RWeapon.GetGameObject().transform;
		}

	#region SetAI

		//设置AI
		public void SetCharactorAI(ICharactorAI theAi)
		{
			m_AI = theAi;
		}

		//更新AI
		protected void AiStateUpdate(List<ICharactor> targets)
		{
			m_AI.Update(targets);
		}

		//通知AI有角色被移除
		public void RemoveAi(ICharactor theTarget)
		{
			m_AI.RemoveAiTarget(theTarget);
		}

	#endregion
		
		//攻击
		public virtual void Attack(ICharactor theTarget)
		{
			//m_Weapon.WeaponAttack(theTarget);
		}
		
		
		
		
		//设置角色属性
		public void SetCharactorAttr(ICharactorAttr theCharactorAttr)
		{
			m_Attribute = theCharactorAttr;
			m_Attribute.InitAttr();

			WalkSpeed = m_Attribute.GetWalkSpeed();
			RunSpeed = m_Attribute.GetRunSpeed();
			JumpHeight = m_Attribute.GetJumpHeight();
		}
		
		//计算角色的伤害值
		//拥有武器的角色要在重写方法中加入武器伤害的计算
		public virtual int GetAtkValue()
		{
			return m_Attribute.GetMaxHarmValue() + m_Attribute.GetAtkplusValue();
		}

		//停止移动
		public virtual void StopMove()
		{
			
		}

		//移动至目标点
		public virtual void MoveTo(Vector3 theTargetPosition)
		{
			
		}

		//盯住目标
		public virtual void LookTarget(ICharactor theTarget)
		{
			
		}
		
		

		//被敌人攻击
		public virtual void UnderAttack(ICharactor theTarget)
		{
			
		}

		//加入相机
		public virtual void AddCamera(CameraControl theCamera)
		{
			
		}
		public NavMeshAgent GetAgent()
		{
			return _agent;
		}

		/// <summary>
		/// 检查是否位于地面
		/// </summary>
		protected void CheckBOnGround()
		{
			if (Sensor != null)
			{
				//定义射线打向地面，来检查角色是否位于平面
				RaycastHit hit;
				if (Physics.Raycast(Sensor.transform.position, Sensor.transform.TransformDirection(Vector3.up) * -1,
					out hit, _raylength,
					1 << 10))
				{
					OnGround();
				}
				else
				{
					NotGround();
				}
			}
		}
		

		//角色位于地面上
		private void OnGround()
		{
			m_Animator.SetBool("BGround", true);
			m_Col.radius = 0.5f;
		}

		//角色不在地面上
		private void NotGround()
		{
			m_Animator.SetBool("BGround", false);
			m_Col.radius = 0f;
		}
		
		

		public void SetAnimation(IGeneral general,IEqip eqip,IUnEqip unEqip)
		{
			MyActionManager.SetAnimation(general, eqip, unEqip);
		}
		
		//朝向判断
		public virtual float UsefulView(ICharactor theTarget)
		{
			var direction = Vector3.Normalize(theTarget.GetModel().transform.position - m_Model.transform.position);
			var value = Vector3.Dot(direction, m_Model.transform.forward);
			var rad = Mathf.Acos(value); //反余弦函数求弧度
			return rad * Mathf.Rad2Deg; //转换为度数返回
		}

		public bool GetHpState()
		{
			return BKilled;
		}

//=============================旧代码===============================================================

		



		public bool BKilled = false; //是否阵亡
		public bool BAttack; //是否可以攻击
		public Vector3 MovingVec; //移动值
		public Vector3 DeltaPos; //动画自身的移动量
		
		

	} //Class_End

} //NameSpace_End

