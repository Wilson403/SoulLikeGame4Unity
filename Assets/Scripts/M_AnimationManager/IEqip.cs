/*
 * IEqip ：武装状态下动画管理
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
	public abstract class IEqip : IActionInterface
	{

		protected IEqip(ActionManager am) : base(am)
		{

		}
		
	#region Public Methods

		public virtual void SetAttackAnimation()
		{
			
		}

		public virtual void SetBlockAnimation()
		{
			
		}

	#endregion

	} //Class_End

} //NameSpace_End
