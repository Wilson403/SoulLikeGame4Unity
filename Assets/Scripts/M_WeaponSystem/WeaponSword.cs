﻿/*
 * WeaponSword ：IWeapon的子类，剑类
 * 程序员 ：Wilson
 * 日期 ：2018/11/28
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：None
 */

using System.Collections;
using System.Collections.Generic;
using M_CharactorSystem;
using UnityEngine;

public class WeaponSword : IWeapon 
{
    public override void WeaponAttack(ICharactor theTarget)
    {
        //通知敌人，你被攻击了！！！同时报上自己的名字
        theTarget.UnderAttack(WeaponOwner);
    }
}