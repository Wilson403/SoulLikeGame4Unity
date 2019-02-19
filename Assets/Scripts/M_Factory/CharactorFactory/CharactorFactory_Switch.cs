using System.Collections;
using System.Collections.Generic;
using System.IO;
using GameAttr.WeaponAttr;
using M_CharactorSystem;
using M_CharactorSystem.CharactorBuilder;
using M_CharactorSystem.M_Enemy;
using M_CharactorSystem.M_Player;
using UnityEngine;

namespace M_Factory.CharactorFactory
{
    /// <summary>
    /// 这是角色工厂，Switch_Case版本。
    /// 当游戏需要多种不同种类的角色时，可以Switch选择产生不同对象
    /// 本Demo只定义了敌我各一种对象，以后可以扩展
    /// </summary>
    public class CharactorFactory_Switch : ICharactorFactory
    {
        private Dictionary<int, IPlayer> m_PlayerDict = new Dictionary<int, IPlayer>();

        private Dictionary<int, IEnemy> m_EnemyDict = new Dictionary<int, IEnemy>();

        //组装指挥者
        private readonly CharactorBuildSystem theCharactorBuildSystem =
            new CharactorBuildSystem(GameManageHub.GetInstance());

        private void InitPlayer()
        {
            m_PlayerDict.Add(1, new Player());
        }

        private void InitEnemy()
        {
            m_EnemyDict.Add(1, new Enemy());
        }

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

            return (Player) theParam.NewCharactor;
        }

        //产生敌方角色
        public override Enemy CreateEnemy()
        {
            var theParam = new EnemyBuildParam();
            return null;
        }
    }
} 
