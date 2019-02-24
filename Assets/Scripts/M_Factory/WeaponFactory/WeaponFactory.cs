using System.Collections;
using System.Collections.Generic;
using GameAttr.WeaponAttr;
using M_WeaponSystem;
using UnityEngine;

namespace M_Factory.WeaponFactory
{
    public class M_WeaponFactory : IWeaonFactory
    {
        public override IWeapon CreatWeapon(WeaponType weaponType)
        {
            //资源名称
            var assetName = "";
            //属性ID
            var attrID = 0;
            //武器对象
            IWeapon weapon = null;

            switch (weaponType)
            {
                //剑
                case WeaponType.Sword:
                    assetName = "SwordHandle";
                    attrID = 1;
                    weapon = new WeaponSword();
                    break;
                //盾
                case WeaponType.Shield:
                    assetName = "ShieldHandle";
                    attrID = 2;
                    weapon = new WeaponShield();
                    break;
                case WeaponType.None:
                default:
                    return null;
                    break;
            }

            //使用武器工厂产生武器
            var assetFactory = MainFactory.Instance.GetAssetFactory();
            var go = assetFactory.LoadWeapon(assetName);
            weapon.SetGameObject(go);
            
            //使用属性工厂给予武器属性
            var factory = MainFactory.Instance.GetrAttrFactory();
            weapon.SetWeaponAttr(factory.GetWeaponAttr(attrID));
            
            return weapon;
        }
        
        
    } 
} 
