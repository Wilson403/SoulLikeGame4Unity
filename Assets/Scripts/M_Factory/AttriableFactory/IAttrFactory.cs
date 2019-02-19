using System.Collections;
using System.Collections.Generic;
using GameAttr.CharactorAttr;
using GameAttr.WeaponAttr;
using UnityEngine;

namespace M_Factory.AttriableFactory
{
    public abstract class IAttrFactory
    {
        public abstract PlayerAttr GetPlayerAttr(int attrID);
        public abstract EnemyAttr GetEnemyAttr(int attrID);
        public abstract WeaponAttr GetWeaponAttr(int attrID);
    } 
} 
