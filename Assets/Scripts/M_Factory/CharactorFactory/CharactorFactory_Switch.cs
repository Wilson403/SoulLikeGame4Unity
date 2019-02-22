using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using GameAttr.WeaponAttr;
using M_CharactorSystem;
using M_CharactorSystem.CharactorBuilder;
using M_CharactorSystem.M_Enemy;
using M_CharactorSystem.M_Player;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace M_Factory.CharactorFactory
{
    /// <summary>
    /// 这是角色工厂，Switch_Case版本。
    /// 当游戏需要多种不同种类的角色时，可以Switch选择产生不同对象
    /// 本Demo只定义了敌我各一种对象，以后可以扩展
    /// </summary>
    public class CharactorFactory_Switch : ICharactorFactory
    {
        private readonly Dictionary<int, IPlayer> m_PlayerDict = new Dictionary<int, IPlayer>();

        private readonly Dictionary<int, IEnemy> m_EnemyDict = new Dictionary<int, IEnemy>();
        

        //组装指挥者
        private readonly CharactorBuildSystem theCharactorBuildSystem =
            new CharactorBuildSystem(GameManageHub.GetInstance());

        
        //产生玩家角色
        public override Player CreatePlayer(Vector3 spawnPos, int lv, WeaponType ltype, WeaponType rtype)
        {
            var theParam = new PlayerBuildParam()
            {
                NewCharactor = new Player(),
                SpawnPosition = spawnPos,
                Lv = lv,
                LWeaponType = ltype,
                RWeaponType = rtype
            };

            var theBuilder = new PlayerBuilder();
            theBuilder.SetBuildParam(theParam);

            theCharactorBuildSystem.ConstructPlayer(theBuilder);

            m_PlayerDict.Add(theParam.CharactorID, (IPlayer) theParam.NewCharactor);
            m_PlayerDict.Add(theParam.WeaponID, (IPlayer) theParam.NewCharactor);

            return (Player) theParam.NewCharactor;
        }

        //产生敌方角色
        public override Enemy CreateEnemy(Vector3 spawnPos,WeaponType ltype,WeaponType rtype)
        {
            var theParam = new EnemyBuildParam()
            {
                NewCharactor = new Enemy(),
                SpawnPosition = spawnPos,
                LWeaponType = ltype,
                RWeaponType = rtype
            };

            var theBuilder = new EnemyBuilder();
            theBuilder.SetBuildParam(theParam);

            theCharactorBuildSystem.ConstructEnemy(theBuilder);

            m_EnemyDict.Add(theParam.CharactorID, (IEnemy) theParam.NewCharactor);
            m_EnemyDict.Add(theParam.WeaponID, (IEnemy) theParam.NewCharactor);

            return (Enemy) theParam.NewCharactor;
        }

        public override IPlayer GetPlayer(int key)
        {
            if (!m_PlayerDict.ContainsKey(key))
            {
                Debug.Log("Player未知Key");
                return null;
            }

            return m_PlayerDict[key];
        }

        public override IEnemy GetEnemy(int key)
        {
//            foreach (var k in m_EnemyDict.Keys)
//            {
//                Debug.Log(k);
//            }
            if (!m_EnemyDict.ContainsKey(key))
            {
                Debug.Log("Enemy未知key");
                return null;
            }
            
            return m_EnemyDict[key];
        }
    }
} 
