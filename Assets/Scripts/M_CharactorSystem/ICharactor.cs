/*
 * ICharactor ：角色抽象类，定义了一些角色通用的功能
 * 程序员 ：Wilson
 * 日期 ：2018/12/15
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：None
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AIMgr.CharactorAIMgr;
using GameAttr.CharactorAttr;
using M_AnimationManager;
using M_ControllerSystem;

namespace M_CharactorSystem
{
	public abstract class ICharactor : MonoBehaviour
	{
		
		

	#region Public_Attriable

		public Transform LWeapontrans
		{
			get { return _lweapontrans; }
		}

		public Transform RWeapontrans
		{
			get { return _rweapontrans; }
		}

	#endregion


		//----------------------------------------------------------------------


	#region Public_Variable

		public bool BKilled; //是否阵亡
		public Animator MyAnimator; //角色控制柄挂载的动画组件
		public GameObject MyModel; //角色模型
		public bool BAttack; //是否可以攻击
		public Vector3 MovingVec; //移动值
		public Vector3 DeltaPos; //动画自身的移动量
		public float WalkSpeed = 2; //走路（散步）速度
		public float RunSpeed = 2; //奔跑速度

	#endregion


		//----------------------------------------------------------------------


	#region Protected_Variable

		protected CapsuleCollider Col; //获取角色控制柄的碰撞体
		protected Rigidbody Rig; //角色控制柄挂载的刚体
		protected ICharactorAttr CharactorAttr = null; //角色属性引用
		protected ICharactorAI CharactorAi = null; //角色AI引用
		protected GameObject Sensor; //传感器,用于检测地面
		protected ActionManager MyActionManager;

	#endregion


		//----------------------------------------------------------------------


	#region Private_Variable

		private Transform _lweapontrans;
		private Transform _rweapontrans;
		private float _raylength = 2f; //射线长度
		private IWeapon _weapon; //武器对象（桥接模式）
		

	#endregion


		//----------------------------------------------------------------------


	#region Public_Methods

		//取得武器攻击力
		public int GetWeaponAtkValue(int index)
		{
			return _weapon.GetAtkValue(index);
		}

		//取得武器防御力
		public int GetDefenseValue(int index)
		{
			return _weapon.GetDefenseValue(index);
		}

	#endregion


		//----------------------------------------------------------------------


	#region Virtual_Methods

		public virtual void Attack(ICharactor theTarget)
		{
			_weapon.WeaponAttack(theTarget);
		}


		public virtual void UnderAttack(ICharactor theTarget)
		{
			//被敌人攻击
		}

		//计算角色的伤害值
		//拥有武器的角色要在重写方法中加入武器伤害的计算
		public virtual int GetAtkValue()
		{
			return CharactorAttr.GetMaxHarmValue();
		}

		public virtual void StopMove()
		{
			//停止移动
		}

		public virtual void MoveTo(Vector3 theTargetPosition)
		{
			//移动至目标点
		}

		public virtual void LookTarget(ICharactor theTarget)
		{
			//盯住目标
		}

		//朝向判断
		public virtual float UsefulView(ICharactor theTarget)
		{
			var direction = Vector3.Normalize(theTarget.transform.position - transform.position);
			var value = Vector3.Dot(direction, transform.forward);
			var rad = Mathf.Acos(value); //反余弦函数求弧度
			return rad * Mathf.Rad2Deg; //转换为度数返回
		}


	#endregion


		//----------------------------------------------------------------------


	#region Protected_Methods

		protected void InitActionManager()
		{
			MyActionManager = new ActionManager(this);
		}

		protected void SetAnimation(IGeneral general,IEqip eqip,IUnEqip unEqip)
		{
			MyActionManager.SetAnimation(general, eqip, unEqip);
		}

		//设置武器对象
		protected void SetWeapon(IWeapon weapon)
		{
			_weapon = weapon;
		}

		//武器对象初始化
		protected void WeaponInit()
		{
			if (_weapon != null)
			{
				_weapon.SetWeapon(MyModel.transform, out _lweapontrans, out _rweapontrans);
				_weapon.SetWeaponOwner(this);
			}
		}

		//取得武器属性
		public void GetWeaponAttr()
		{
			if (_weapon != null)
				_weapon.GetWeaponAttr();
		}

		//设置角色属性
		protected void SetCharactorAttr(ICharactorAttr theCharactorAttr)
		{
			CharactorAttr = theCharactorAttr;
		}

		//设置AI
		protected void SetAiTarget(ICharactorAI theAiTarget)
		{
			CharactorAi = theAiTarget;
		}

		//更新AI
		protected void AiStateUpdate(List<ICharactor> targets)
		{
			CharactorAi.Update(targets);
		}

		//通知AI有角色被移除
		protected void RemoveAi(ICharactor theTarget)
		{
			CharactorAi.RemoveAiTarget(theTarget);
		}

		//检查角色是否位于地面
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

	#endregion


		//----------------------------------------------------------------------


	#region Private_Methods

		//角色位于地面上
		private void OnGround()
		{
			MyAnimator.SetBool("BGround", true);
			Col.radius = 0.5f;
		}

		//角色不在地面上
		private void NotGround()
		{
			MyAnimator.SetBool("BGround", false);
			Col.radius = 0f;
		}

	#endregion

	} //Class_End

} //NameSpace_End

