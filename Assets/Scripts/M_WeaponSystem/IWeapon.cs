/*
 * IWeapon ：武器抽象接口
 * 程序员 ：Wilson
 * 日期 ：2018/11/28
 * 挂载对象 ：None
 * 更多描述 ：-定义游戏中对于武器的使用方法
 *           -不同武器，攻击力不一样，攻击特效以及音频也可能不一样。
 * 修改记录 ：None
 */

using System.Collections;
using System.Collections.Generic;
using System.Resources;
using M_CharactorSystem;
using UnityEngine;
using GameAttr.WeaponAttr;

namespace M_WeaponSystem
{
	public abstract class IWeapon
	{
		private Transform _weapontrans; //角色武器的左手挂载点
		

		private Collider _col; //角色左手武器碰撞器
		

		private ICharactor WeaponOwner = null; //武器的拥有者

		private WeaponAttr _weaponAttr = null; //左手武器属性
		

		private GameObject _weaponObject; //左手武器模型
		

		//设置Unity模型
		public void SetGameObject(GameObject lweaponObject)
		{
			_weaponObject = lweaponObject;
			_col = _weaponObject.transform.GetChild(0).GetComponent<Collider>();
			_col.enabled = false;
		}

		public GameObject GetGameObject()
		{
			return _weaponObject;
		}

		public void SetWeaponAttr(WeaponAttr lAttr)
		{
			_weaponAttr = lAttr;
		}

		//设置武器的拥有者
		public void SetWeaponOwner(ICharactor theCharactor)
		{
			WeaponOwner = theCharactor;
		}

	#region 获取武器属性旧代码

		//_colL = _lweapontrans.GetComponentInChildren<Collider>();
//		_colR = _rweapontrans.GetComponentInChildren<Collider>();
//
//		if (_lweapontrans.childCount != 0)
//		{
//			_lweaponAttr = _lweapontrans.transform.GetChild(0).GetChild(0).GetComponent<WeaponAttr>();
//		}
//
//		if (_rweapontrans.childCount != 0)
//		{
//			_rweaponAttr = _rweapontrans.transform.GetChild(0).GetChild(0).GetComponent<WeaponAttr>();
//		}

	#endregion

		//获取左手武器属性
		public WeaponAttr GetWeaponAttr()
		{
			
			return _weaponAttr;
		}



		/// <summary>
		/// 取得武器的攻击力。0:L,1:R
		/// </summary>
		/// <param name="index">（0 or 1)</param>
		/// <returns></returns>
		public int GetAtkValue()
		{
			return _weaponAttr.AtkValue;
		}

		/// <summary>
		/// 取得武器的防御力。0:L,1:R
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public int GetDefenseValue()
		{
			return _weaponAttr.DefenseValue;
		}


		//武器攻击
		public void WeaponAttack(ICharactor theTarget)
		{
			theTarget.UnderAttack(WeaponOwner);
		}


		//-----------------------------------------------------------------------------


		//设置武器是否具有攻击的效果
		//为什么不让武器一直具备攻击的效果？
		//因为攻击判定是靠触发器判断的，如果一直保持碰撞体的启用，有时会无意触发攻击效果


	//启用左武器的攻击功能
	public void WeaponEnable(bool isenable)
	{
		_col.enabled = isenable;
	}



	}
}
