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
		//private Transform _rweapontrans; //角色武器的右手挂载点

		private Collider _col; //角色左手武器碰撞器
		//private Collider _colR; //角色右手武器碰撞器

		private ICharactor WeaponOwner = null; //武器的拥有者

		private WeaponAttr _weaponAttr = null; //左手武器属性
		//private WeaponAttr _rweaponAttr = null; //右手武器属性

		private GameObject _weaponObject; //左手武器模型
		//private GameObject _rweaponObject; //右手武器模型

		//设置Unity模型
		public void SetGameObject(GameObject lweaponObject)
		{
			_weaponObject = lweaponObject;
			//_rweaponObject = rweaponObject;
		}

		public GameObject GetGameObject()
		{
			return _weaponObject;
		}

		public void SetWeaponAttr(WeaponAttr lAttr)
		{
			_weaponAttr = lAttr;
			//_rweaponAttr = rAttr;
		}

//		//设置武器手持挂载点信息
//		public void SetWeapon(Transform theTarget, out Transform lweapontrans, out Transform rweapontrans)
//		{
//			_lweapontrans = UnityTool.DeepFind(theTarget, "weaponHandleL").transform;
//			_rweapontrans = UnityTool.DeepFind(theTarget, "weaponHandleR").transform;
//
//			lweapontrans = _lweapontrans;
//			rweapontrans = _rweapontrans;
//		}

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
			_col = _weaponObject.GetComponent<Collider>();
			return _weaponAttr;
		}

//		//获取右手武器属性
//		public WeaponAttr GetRWeaponAttr()
//		{
//			_col = _weaponObject.GetComponent<Collider>();
//			return _weaponAttr;
//		}



		/// <summary>
		/// 取得武器的攻击力。0:L,1:R
		/// </summary>
		/// <param name="index">（0 or 1)</param>
		/// <returns></returns>
		public int GetAtkValue()
		{
//			if (index == 0 && _lweaponAttr != null)
//			{
//				return _lweaponAttr.AtkValue;
//			}
//			else if (index == 1 && _rweaponAttr != null)
//			{
//				return _rweaponAttr.AtkValue;
//			}
//			else
//			{
//				Debug.Log("武器属性获取未知错误");
//				return 0;
//			}
			return _weaponAttr.AtkValue;
		}

		/// <summary>
		/// 取得武器的防御力。0:L,1:R
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public int GetDefenseValue()
		{
//			if (index == 0 && _lweaponAttr != null)
//			{
//				return _lweaponAttr.DefenseValue;
//			}
//			else if (index == 1 && _rweaponAttr != null)
//			{
//				return _rweaponAttr.DefenseValue;
//			}
//			else
//			{
//				Debug.Log("武器属性获取未知错误");
//				return 0;
//			}
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

	#region WeaponEnable

//	//启用左武器的攻击功能
//	public void LWeaponEnable()
//	{
//		_colL.enabled = true;
//	}
//
//	//启用右武器的攻击功能
//	public void RWeaponEnable()
//	{
//		_colR.enabled = true;
//	}
//
//	//禁用武器触发功能
//	public void WeaponDisenable()
//	{
//		_colL.enabled = false;
//		_colR.enabled = false;
//	}

	#endregion


	}
}
