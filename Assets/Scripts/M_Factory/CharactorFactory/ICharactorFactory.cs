/*
 * ICharactorFactory ：
 * 程序员 ：Wilson
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：None
 */
 
using System.Collections;
using System.Collections.Generic;
using GameAttr.WeaponAttr;
using M_CharactorSystem;
using M_CharactorSystem.M_Enemy;
using M_CharactorSystem.M_Player;
using UnityEngine;

namespace M_Factory.CharactorFactory
{
    public abstract class ICharactorFactory
    {
        //产生玩家角色
        public abstract Player CreatePlayer(Vector3 spawnPos, int lv, WeaponType ltype, WeaponType rtype);
       
        //产生敌方角色
        public abstract Enemy CreateEnemy(Vector3 spawnPos);

        public abstract IPlayer GetPlayer(int key);

        public abstract IEnemy GetEnemy(int key);
    } 
} 
