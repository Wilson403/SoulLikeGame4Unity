/*
 * CharactorSystem ：
 * 程序员 ：Wilson
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：None
 */
 
using System.Collections;
using System.Collections.Generic;
using BaseClass;
using M_CharactorSystem.M_Enemy;
using M_CharactorSystem.M_Player;
using UnityEngine;

namespace M_CharactorSystem
{
    public class CharactorSystem : IGameSystem
    {
        private readonly List<IPlayer> _players = new List<IPlayer>();
        private readonly List<IEnemy> _enemies = new List<IEnemy>();
        
        public CharactorSystem(GameManageHub hub) : base(hub)
        {
           
        }

        public void AddPlayer(IPlayer thePlayer)
        {
            _players.Add(thePlayer);
        }

        public void AddEnemy(IEnemy theEnemy)
        {
            _enemies.Add(theEnemy);
        }
        
        public override void Update()
        {
            foreach (var player in _players)
            {

                player.Update();
            }

            foreach (var enemy in _enemies)
            {

                enemy.Update();
            }
        }

        public void FixedUpdate()
        {
            foreach (var player in _players)
            {
                player.FixedUpdate();
            }
        }

    } 
} 
