//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//namespace M_AnimationManager
//{
//	public class BattleActionMgr : IActionInterface {
//
//		public BattleActionMgr(ActionManager am) : base(am)
//		{
//
//		}
//
//	
//		/// <summary>
//		/// 攻击动画的触发判断
//		/// </summary>
//		public void EqipAttack()
//		{
//			//轻攻击动画触发
//			//触发条件：按下轻攻击键且处于默认状态下或攻击中状态下
////		if (Am.MPlay.Controller.LAttack &&
////		    (Am.CheckState("EqipMove") || Am.CheckStateTag("LAttack")))
////		{
////			Am.MPlay.MyAnimator.SetTrigger("LAttack");
////		}
//		
//			if (Am.MPlay.Controller.LAttack)
//			{
//				Am.MPlay.MyAnimator.SetTrigger("LAttack");
//			}
//
//			//重攻击动画触发
//			//触发条件：按下重攻击键且处于默认状态下或攻击中状态下
////		if (Am.MPlay.Controller.WAttack &&
////		    (Am.CheckState("EqipMove") || Am.CheckStateTag("WAttack")))
////		{
////			Am.MPlay.MyAnimator.SetTrigger("WAttack");
////		}
//		
//			if (Am.MPlay.Controller.WAttack)
//			{
//				Am.MPlay.MyAnimator.SetTrigger("WAttack");
//			}
//		}
//
//	
//		//-------------------------------------------------------------------------------
//	
//	
//		/// <summary>
//		/// 防御触发判断
//		/// 防御动画在Defense层，需要动态调整层权重
//		/// </summary>
//		public void EqipBlock()
//		{
//			//只有在IDLE状态下才能防御
//			if (Am.CheckState("EqipMove"))
//			{
//				//根据防御键状态来判定是否触发防御动画
//				Am.MPlay.MyAnimator.SetBool("Block", Am.MPlay.Controller.BDefense);
//			
//				//按下防御键权重插值到1
//				if (Am.MPlay.Controller.BDefense)
//				{
//					Am.MPlay.MyAnimator.SetLayerWeight(Am.MPlay.MyAnimator.GetLayerIndex("Defense"),
//						Mathf.Lerp(Am.MPlay.MyAnimator.GetLayerWeight(Am.MPlay.MyAnimator.GetLayerIndex("Defense")),
//							1, 0.2f));
//				}
//				//松开防御键权重插值回0
//				else
//				{
//					Am.MPlay.MyAnimator.SetLayerWeight(Am.MPlay.MyAnimator.GetLayerIndex("Defense"),
//						Mathf.Lerp(Am.MPlay.MyAnimator.GetLayerWeight(Am.MPlay.MyAnimator.GetLayerIndex("Defense")),
//							0, 0.2f));
//				}
//			}
//			//其他动画状态权重都会拉回0
//			else
//			{
//				//权重插值回0
//				Am.MPlay.MyAnimator.SetLayerWeight(Am.MPlay.MyAnimator.GetLayerIndex("Defense"),
//					Mathf.Lerp(Am.MPlay.MyAnimator.GetLayerWeight(Am.MPlay.MyAnimator.GetLayerIndex("Defense")),
//						0, 0.3f));
//			}
//		}//EqipBlock_End
//	
//	
//		//-------------------------------------------------------------------------------
//	}
//}
//
