/*
 * PlayerGeneralAnimationMgr ：玩家角色动画管理
 * 程序员 ：Wilson
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：None
 */

using System;
using System.Collections;
using System.Collections.Generic;
using M_CharactorSystem;
using UnityEngine;

namespace M_AnimationManager.PlayerAnimation
{
	public class PlayerGeneralAnimationMgr : IGeneral
	{
		private readonly Player _player;

		public PlayerGeneralAnimationMgr(ActionManager am) : base(am)
		{
			_player = Charactor as Player;
		}

	#region Public Methods

		public override void GetMoveAnimation()
		{
			//不处于聚焦状态下
			if (_player.CameraControl.LockState == false)
			{
				//移动的时候调节动画混合树
				if (_player.Controller.DMag > 0)
				{
					AnimSetfloat(true, "Z", _player.Controller.DMag <= 0.8f ? 1f : 2f, 0.1f);
					AnimSetfloat(false, "X", 0, 0);
				}
				else //平滑动画，去掉else块动画切换没有过渡
				{
					//向0值平滑
					AnimSetfloat(true, "Z", 0, 0.1f);
				}

				//移动速度的计算
				if (!_player.BLockmoving)
				{
					if (_player.Controller.DMag > 0)
					{
						_player.MovingVec =
							(_player.Controller.DMag <= 0.8f ? _player.WalkSpeed : _player.RunSpeed) *
							_player.MyModel.transform.forward;
					}
					else
					{
						_player.MovingVec = Vector3.zero;
					}
				}

			} //UnEqipMove_End

			else //相机锁住物体时，即聚焦模式，移动动作变更
			{
				//将旋转量从世界空间转变到局部空间
				var localDvec =
					_player.transform.InverseTransformVector(_player.Controller.GetRVec(_player.transform));

				AnimSetfloat(true, "Z", localDvec.z, 0.1f);
				AnimSetfloat(true, "X", localDvec.x, 0.1f);

				_player.MyModel.transform.forward = _player.transform.forward;

				if (!_player.BLockmoving)
					_player.MovingVec = _player.Controller.GetRVec(_player.transform) * _player.WalkSpeed;
			}
		}

		public override void ChangeActionState()
		{

			if (_player.Controller.GetSelect1())
			{
				if (CheckState("UneqipMove"))
				{
					CurAnimator.SetTrigger("ToEqip");
					CurAnimator.SetBool("B_Eqip", true);
				}

				else if (CheckState("EqipMove"))
				{
					CurAnimator.SetTrigger("ToUnEqip");
					CurAnimator.SetBool("B_Eqip", false);
				}
			}
		}

	#endregion

	} //Class_End

} //NameSpace_End
