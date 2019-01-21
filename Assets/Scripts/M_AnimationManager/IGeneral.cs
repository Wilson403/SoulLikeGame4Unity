/*
 * IGeneral ：通用动画管理
 * 程序员 ：Wilson
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：None
 */
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace M_AnimationManager
{
	public abstract class IGeneral : IActionInterface
	{

		protected IGeneral(ActionManager am) : base(am)
		{

		}
		
	#region Public Methods

		public virtual void GetMoveAnimation()
		{
			
		}

		public virtual void ChangeActionState()
		{
			
		}
		
		public virtual void GetJumpAnimation()
		{

		}
		
		public virtual void GetDamageAnimation(float x,float z)
		{

		}

		public virtual void GetDeadAnimation()
		{
			
		}

		public virtual void RootMotionValue(Vector3 delta,out Vector3 deltaPos)
		{
			deltaPos = Vector3.zero;
		}

	#endregion

	} //Class_End

} //NameSpace_End
