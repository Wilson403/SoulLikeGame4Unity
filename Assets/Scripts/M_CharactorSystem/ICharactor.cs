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

namespace M_CharactorSystem
{
	public abstract class ICharactor : MonoBehaviour
	{

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

	#endregion


		//----------------------------------------------------------------------


	#region Virtual_Methods

		public virtual void Attack(ICharactor theTarget)
		{
			//攻击敌人
		}


		public virtual void UnderAttack(ICharactor theTarget)
		{
			//被敌人攻击
		}

		//计算角色的伤害值
		//拥有武器的角色要在重写方法中加入武器伤害的计算
		public virtual int GetAtkValue()
		{
			return 0;
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

		public virtual bool UsefulView(ICharactor theTarget)
		{
			return false;
			//是否在有效视野
		}

	#endregion


		//----------------------------------------------------------------------


	#region Protected_Methods

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

	#endregion

	} //Class_End
	
} //NameSpace_End

