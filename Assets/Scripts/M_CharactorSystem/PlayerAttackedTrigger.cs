/*
 * AttackedTrigger ：
 * 程序员 ：Wilson
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：None
 */
 
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using M_Factory;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace M_CharactorSystem
{
	public class PlayerAttackedTrigger : MonoBehaviour
	{

		private ICharactor _charactor;
		private int _id;

		private void Start()
		{
			_id = transform.parent.gameObject.GetInstanceID();
			_charactor = MainFactory.Instance.GetCharactorFactory().GetPlayer(_id);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Sword"))
			{
				var id = other.transform.parent.gameObject.GetInstanceID();
				_charactor.UnderAttack(MainFactory.Instance.GetCharactorFactory().GetEnemy(id));
			}
		}


	} //Class_End

} //NameSpace_End
