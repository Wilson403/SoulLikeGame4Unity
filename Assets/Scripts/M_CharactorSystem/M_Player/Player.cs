using System;
using System.Collections;
using System.Collections.Generic;
using M_ControllerSystem;
using UnityEngine;
using GameAttr.AttrStrategy;
using GameAttr.CharactorAttr;
using GameAttr.WeaponAttr;
using MyNamespace;
using M_AnimationManager.PlayerAnimation;
using M_WeaponSystem;

namespace M_CharactorSystem.M_Player
{
	public class Player : IPlayer
	{
		public Player()
		{
			m_Name = "Soldier";
			m_Assetname = "Player";
			m_AttrID = 1;
		}

		public override void Init()
		{
			base.Init();

			//状态事件的注册
			EventMgr.Instance.AddListener(EventMgr.EVENT_TYPE.PlayerFsmEnter, EnterEvent); //进入状态
			EventMgr.Instance.AddListener(EventMgr.EVENT_TYPE.PlayerFsmExit, ExitEvent); //离开状态
			EventMgr.Instance.AddListener(EventMgr.EVENT_TYPE.PlayerFsmUpdate, UpdateEvent); //更新状态
		}


		public override void Update()
		{
			base.Update();

			MyActionManager.General.ChangeActionState();
			MyActionManager.General.GetMoveAnimation();
			MyActionManager.General.GetJumpAnimation();
			MyActionManager.Eqip.SetAttackAnimation();
			MyActionManager.Eqip.SetBlockAnimation();
		}

		public override void FixedUpdate()
		{
			if (m_Rig == null) return;

			m_Rig.position += _deltaPos;
			m_Rig.velocity = new Vector3(MovingVec.x, m_Rig.velocity.y, MovingVec.z) + _jumpVec +
			                 _rollVec + _stepVec;
			_jumpVec = Vector3.zero;
			_deltaPos = Vector3.zero;
		}

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
			var tmpZ = m_Animator.GetFloat("Z") * m_Model.transform.forward;
			var tmpX = m_Animator.GetFloat("X") * m_Model.transform.right;
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
			var tmpZ = m_Animator.GetFloat("Z") * m_Model.transform.forward;
			var tmpX = m_Animator.GetFloat("X") * m_Model.transform.right;
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
		private void EnterEvent(EventMgr.EVENT_TYPE Event_Type, Component Sender, object Param = null)
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
		private void ExitEvent(EventMgr.EVENT_TYPE Event_Type, Component Sender, object Param = null)
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
		private void UpdateEvent(EventMgr.EVENT_TYPE Event_Type, Component Sender, object Param = null)
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

