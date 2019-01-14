/*
 * FSMOnSendMessage ：动画状态事件
 * 程序员 ：Wilson
 * 日期 ：2018/12/5
 * 挂载对象 ：动画状态机
 * 更多描述 ：None
 * 修改记录 ：None
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
	public enum CharactorType
	{
		Player,
		Enemy,
		None
	}

	public class FSMOnSendMessage : StateMachineBehaviour
	{

		public CharactorType Type = CharactorType.Player;
		
		public string[] OnEnterMessage;

		public string[] OnExitMessage;

		public string[] OnUpdateMassages;

		public string[] EnterClearSignal;

		public string[] ExitClearSignal;

		public string[] UpdateClearSignal;


		//-----------------------------------------------------------------------------------------------------


		// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
		override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{

			foreach (string signal in OnEnterMessage)
			{
				if (Type == CharactorType.Player)
				{
					EventMgr.Instance.PostNotification(EventMgr.EVENT_TYPE.PlayerFsmEnter, animator.transform.parent,
						signal);
				}

				if (Type == CharactorType.Enemy)
				{
					EventMgr.Instance.PostNotification(EventMgr.EVENT_TYPE.EnemyFsmEnter, animator.transform.parent,
						signal);
				}
			}

			foreach (string signal in EnterClearSignal)
			{
				animator.ResetTrigger(signal);
			}

		}



		//-----------------------------------------------------------------------------------------------------


		// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
		override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			foreach (string signal in OnUpdateMassages)
			{
				if (Type == CharactorType.Player)
				{
					EventMgr.Instance.PostNotification(EventMgr.EVENT_TYPE.PlayerFsmUpdate, animator.transform.parent,
						signal);
				}
			}

			foreach (string signal in UpdateClearSignal)
			{
				animator.ResetTrigger(signal);
			}
		}


		//-----------------------------------------------------------------------------------------------------


		// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
		override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			if (Type == CharactorType.Player)
			{
				foreach (string signal in OnExitMessage)
				{
					EventMgr.Instance.PostNotification(EventMgr.EVENT_TYPE.PlayerFsmExit, animator.transform.parent,
						signal);
				}
			}

			foreach (string signal in ExitClearSignal)
			{
				animator.ResetTrigger(signal);
			}
		}


		//-----------------------------------------------------------------------------------------------------


		// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
		//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		//
		//}

		// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
		//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		//
		//}
	}
}

