using System.Collections;
using System.Collections.Generic;
using BaseClass;
using UnityEngine;

namespace M_CharactorSystem.CharactorBuilder
{
    /// <summary>
    /// 它是一个组装指挥者，角色工厂持有。子系统之一。
    /// 为什么要作为子系统？因为角色构建完毕后需要通知其他系统，定义为子系统方便通信
    /// </summary>
    public class CharactorBuildSystem : IGameSystem
    {
        private int _gameobjectID = 0;
        public CharactorBuildSystem(GameManageHub hub) : base(hub)
        {
            
        }

        /// <summary>
        /// 构建出角色，负责流程
        /// </summary>
        /// <param name="theBuilder"></param>
        public void ConstructPlayer(ICharactorBuilder theBuilder)
        {
            theBuilder.LoadAsset(++_gameobjectID);
            theBuilder.AddController();
            theBuilder.AddCamera();
            theBuilder.AddActionManager();
            theBuilder.AddCharactorAttr();
            theBuilder.AddWeapon();
            theBuilder.AddCharactorSystem(Hub);
        }

        public void ConstructEnemy(ICharactorBuilder theBuilder)
        {
            theBuilder.LoadAsset(++_gameobjectID);
            theBuilder.AddCharactorAttr();
            //theBuilder.AddWeapon();
            theBuilder.AddAI();
            theBuilder.AddCharactorSystem(Hub);
        }
    } 
} 
