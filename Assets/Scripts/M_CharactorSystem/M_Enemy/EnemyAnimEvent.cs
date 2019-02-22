using System.Collections;
using System.Collections.Generic;
using M_CharactorSystem;
using UnityEngine;

public class EnemyAnimEvent : MonoBehaviour {

	private ICharactor _charactor; //角色类对象
	private Animator _anim; //动画状态机

	private void Awake()
	{
		_anim = GetComponent<Animator>();
	}

	public void SetCharactor(ICharactor theCharactor)
	{
		_charactor = theCharactor;

	}

	public void RHandTakeWeapon()
	{
		
	}

	
	public void RHandNoWeapon()
	{
		
	}
	


	public void ResetTrigger(string name)
	{
		
	}
	
	public void OpenCol()
	{
		_charactor.EnableWeaponCol(true);
	}

	public void CloseCol()
	{
		_charactor.EnableWeaponCol(false);
	}
}
