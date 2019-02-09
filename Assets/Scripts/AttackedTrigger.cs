/*
 * AttackedTrigger ：
 * 程序员 ：Wilson
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：None
 */
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace M_CharactorSystem
{
	public class AttackedTrigger : MonoBehaviour
	{

	#region Private Variable
		
		private ICharactor _charactor;

	#endregion

		
		//----------------------------------------------------------
		

	#region Unity Messages

		private void Start()
		{
			_charactor = transform.parent.GetComponent<ICharactor>();
		}


		private void OnTriggerEnter(Collider other)
		{
			//other.transform.
			// _charactor.UnderAttack();
			//Debug.Log("我被攻击了"+other.name + "" + other.tag);
		}

	#endregion

	

	#region Override Methods

	#endregion

	} //Class_End

} //NameSpace_End
