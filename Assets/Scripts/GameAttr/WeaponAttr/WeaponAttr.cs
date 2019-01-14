using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameAttr.WeaponAttr
{
    public enum WeaponType
    {
        Sword,
        Shield,
        None
    }

    public class WeaponAttr : MonoBehaviour
    {
//        public int Atk { get; private set; }
//
//        public WeaponAttr(int atkValue)
//        {
//            this.Atk = atkValue;
//        }
        public WeaponType TheWeaponType = WeaponType.None; //武器类型
        public int AtkValue; //伤害值
        public int DedenseValue; //防御值
    }
}


