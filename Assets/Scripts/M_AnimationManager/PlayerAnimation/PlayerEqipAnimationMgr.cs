/*
 * PlayerEqipAnimationMgr ：
 * 程序员 ：Wilson
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：None
 */
 
using System.Collections;
using System.Collections.Generic;
using M_AnimationManager;
using M_CharactorSystem;
using UnityEngine;

namespace M_AnimationManager.PlayerAnimation
{
	public class PlayerEqipAnimationMgr : IEqip
	{
		private readonly Player _player;
		
		public PlayerEqipAnimationMgr(ActionManager am) : base(am)
		{
			_player = Charactor as Player;
		}
		
		public override void SetAttackAnimation()
		{
			//轻攻击动画触发
			if (_player.Controller.LAttack)
			{
				_player.MyAnimator.SetTrigger("LAttack");
			}
			
			//重攻击动画触发
			if (_player.Controller.WAttack)
			{
				_player.MyAnimator.SetTrigger("WAttack");
			}
			
//================================旧代码，可删除========================================================
//			//触发条件：按下轻攻击键且处于默认状态下或攻击中状态下
//		if (Am.MPlay.Controller.LAttack &&
//		    (Am.CheckState("EqipMove") || Am.CheckStateTag("LAttack")))
//		{
//			Am.MPlay.MyAnimator.SetTrigger("LAttack");
//		}
			
			//触发条件：按下重攻击键且处于默认状态下或攻击中状态下
//		if (Am.MPlay.Controller.WAttack &&
//		    (Am.CheckState("EqipMove") || Am.CheckStateTag("WAttack")))
//		{
//			Am.MPlay.MyAnimator.SetTrigger("WAttack");
//		}
//========================================================================================
			
		}

		public override void SetBlockAnimation()
		{
			//只有在IDLE状态下才能防御
			if (CheckState("EqipMove"))
			{
				//根据防御键状态来判定是否触发防御动画
				CurAnimator.SetBool("Block", _player.Controller.BDefense);
			
				//按下防御键权重插值到1
				if (_player.Controller.BDefense)
				{
					SetAnimLayerWeight("Defense", 1, 0.2f);
				}
				//松开防御键权重插值回0
				else
				{
					SetAnimLayerWeight("Defense", 0, 0.2f);
				}
			}
			//其他动画状态权重都会拉回0
			else
			{
				//权重插值回0
				SetAnimLayerWeight("Defense", 0, 0.3f);
			}
		}

	} //Class_End
	
} //NameSpace_End
