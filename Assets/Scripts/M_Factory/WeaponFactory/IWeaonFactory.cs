using System.Collections;
using System.Collections.Generic;
using GameAttr.WeaponAttr;
using M_WeaponSystem;
using UnityEngine;

namespace M_Factory.WeaponFactory
{
    public abstract class IWeaonFactory
    {
        /// <summary>
        /// 生产武器
        /// </summary>
        /// <param name="weaponType">武器类型</param>
        public abstract IWeapon CreatWeapon(WeaponType weaponType);
    } 
} 
