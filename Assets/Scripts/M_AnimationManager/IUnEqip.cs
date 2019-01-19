/*
 * IUnEqip ：非武装状态动画管理
 * 程序员 ：Wilson
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：None
 */
 
using System.Collections;
using System.Collections.Generic;
using M_CharactorSystem;
using UnityEngine;

namespace M_AnimationManager
{
	public abstract class IUnEqip : IActionInterface
	{

		protected IUnEqip(ActionManager am) : base(am)
		{

		}

	#region Public Methods

		public virtual void SetJumpAnimation()
		{

		}

	#endregion


	} //Class_End

} //NameSpace_End
