using UnityEngine;

namespace GameAttr.WeaponAttr
{
    public enum WeaponType
    {
        Sword,
        Shield,
        None
    }

    public class WeaponAttr
    {
        //public readonly WeaponType TheWeaponType;
        public readonly int AtkValue; //伤害值
        public readonly int DefenseValue; //防御值

        public WeaponAttr(int atkValue, int defenseValue)
        {
            //TheWeaponType = type;
            AtkValue = atkValue;
            DefenseValue = defenseValue;
        }
    }
}


