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
				
				//角色方向变更
				//只有摇杆值不为0时才进行角色方向变更
				if (_player.Controller.DMag > 0f)
				{
					//利用线性插值使角色旋转变得平滑
					_player.MyModel.transform.forward =
						Vector3.Slerp(_player.MyModel.transform.forward,
							_player.Controller.GetRVec(_player.transform),
							0.3f);
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

		//触发跳跃躲闪动作
		public override void SetJumpAnimation()
		{
			if (CurAnimator.GetBool("Move"))
			{
				if (_player.Controller.BJump)
				{
					if (!_player.CameraControl.LockState)
						CurAnimator.SetTrigger("Jump");
					else
					{
						if (CheckState("EqipMove"))
						{
							CurAnimator.SetTrigger("Dodge_Roll");
						}
					}
				}

				else if (_player.Controller.BStep)
				{
					if (_player.CameraControl.LockState)
					{
						if (CheckState("EqipMove"))
							CurAnimator.SetTrigger("Dodge_Step");
					}
				}
			}
		}

		public override void RootMotionValue(Vector3 delta,out Vector3 deltaPos)
		{
			/*
			 1.如果是轻攻击第一击且处于相机锁定状态，会有很长的一段滑动距离
			 2.滑动距离是根据与锁定物体的距离来动态调整的 
			 */
			if (CheckState("LAttack_A") && _player.CameraControl.LockState)
			{
				deltaPos = delta * (_player.CameraControl.ObjectDistance > 5
					           ? 5
					           : _player.CameraControl.ObjectDistance * 0.6f);
			}
			//一般状态下的根运动值
			else
			{
				deltaPos = delta * 0.5f;
			}
		}

	#endregion

	} //Class_End

} //NameSpace_End
