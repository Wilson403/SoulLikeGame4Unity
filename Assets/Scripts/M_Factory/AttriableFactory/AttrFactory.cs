using System.Collections;
using System.Collections.Generic;
using GameAttr.Base;
using GameAttr.CharactorAttr;
using GameAttr.WeaponAttr;
using M_CharactorSystem.M_Enemy;
using M_Factory.AttriableFactory;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

namespace M_Factory.AttriableFactory
{
    public class AttrFactory : IAttrFactory
    {
        private Dictionary<int, BaseAttr> PlayerDB;
        private Dictionary<int, BaseEnemyAttr> EnemyDB; 
        private Dictionary<int, WeaponAttr> WeaponDB;

        public AttrFactory()
        {
            InitPlayerAttr();
            InitEnemyAttr();
            InitWeaponAttr();
        }

        //初始化玩家基础属性
        private void InitPlayerAttr()
        {
            PlayerDB = new Dictionary<int, BaseAttr>()
            {
                {1, new BaseCharactorAttr(2, 4, 2, "PlayerA", 100)}
            };
        }
        
        //初始化敌人基础属性
        private void InitEnemyAttr()
        {
            EnemyDB = new Dictionary<int, BaseEnemyAttr>()
            {
                {1, new BaseEnemyAttr(2, 4, 2, "EnemyA", 100, 5)},
                {2, new BaseEnemyAttr(2, 4, 2, "EnemyB", 200, 10)},
                {3, new BaseEnemyAttr(2, 4, 2, "EnemyC", 300, 15)}
            };
        }
        
        //初始化武器的属性
        private void InitWeaponAttr()
        {
            WeaponDB = new Dictionary<int, WeaponAttr>()
            {
                {1, new WeaponAttr(10, 0)},
                {2, new WeaponAttr(0, 9)}
            };
        }

        //获取玩家的基础属性
        public override PlayerAttr GetPlayerAttr(int attrID)
        {
            if (!PlayerDB.ContainsKey(attrID)) return null;
            var playerAttr = new PlayerAttr();
            playerAttr.SetPlayerAttr(PlayerDB[attrID]);
            return playerAttr;
        }

        //获取敌人的基础属性
        public override EnemyAttr GetEnemyAttr(int attrID)
        {
            if (!EnemyDB.ContainsKey(attrID)) return null;
            var enemyAttr = new EnemyAttr();
            enemyAttr.SetEnemyAttr(EnemyDB[attrID]);
            return enemyAttr;
        }

        //获取武器的属性
        public override WeaponAttr GetWeaponAttr(int attrID)
        {
            if (!WeaponDB.ContainsKey(attrID)) return null;
            return WeaponDB[attrID];
        }
    } 
} 
