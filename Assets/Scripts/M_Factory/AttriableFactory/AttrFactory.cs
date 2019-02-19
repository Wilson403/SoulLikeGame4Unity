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
        private Dictionary<int, BaseAttr> EnemyDB;
        private Dictionary<int, WeaponAttr> WeaponDB;

        public AttrFactory()
        {
            InitPlayerAttr();
            InitEnemyAttr();
            InitWeaponAttr();
        }

        private void InitPlayerAttr()
        {
            PlayerDB = new Dictionary<int, BaseAttr>()
            {
                {1, new BaseCharactorAttr(2, 4, 2, "PlayerA", 100)}
            };
        }
        
        private void InitEnemyAttr()
        {
            EnemyDB = new Dictionary<int, BaseAttr>()
            {
                {1, new BaseEnemyAttr(2, 4, 2, "EnemyB", 100)}
            };
        }
        
        private void InitWeaponAttr()
        {
            WeaponDB = new Dictionary<int, WeaponAttr>()
            {
                {1, new WeaponAttr(10, 0)},
                {2, new WeaponAttr(0, 9)}
            };
        }

        public override PlayerAttr GetPlayerAttr(int attrID)
        {
            if (!PlayerDB.ContainsKey(attrID)) return null;
            var playerAttr = new PlayerAttr();
            playerAttr.SetPlayerAttr(PlayerDB[attrID]);
            return playerAttr;
        }

        public override EnemyAttr GetEnemyAttr(int attrID)
        {
            if (!EnemyDB.ContainsKey(attrID)) return null;
            var enemyAttr = new EnemyAttr();
            //enemyAttr.SetPlayerAttr(PlayerDB[attrID]);
            return null;
        }

        public override WeaponAttr GetWeaponAttr(int attrID)
        {
            if (!WeaponDB.ContainsKey(attrID)) return null;
            return WeaponDB[attrID];
        }
    } 
} 
