/*
 * CharactorFactory ：
 * 程序员 ：Wilson
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：None
 */
 
using System.Collections;
using System.Collections.Generic;
using M_CharactorSystem;
using M_CharactorSystem.CharactorBuilder;
using UnityEngine;

namespace M_Factory.CharactorFactory
{
    public class CharactorFactory_General : ICharactorFactory
    {
        //这是一个角色组装指示者
        private readonly CharactorBuildSystem charactorBuildSystem = new CharactorBuildSystem(GameManageHub.GetInstance());
        
        public override ICharactor_New CreatePlayer(string assetname,int lv)
        {
            var theParam = new PlayerBuildParam();
            theParam.NewCharactor = new Player_New();
            theParam.Lv = lv;
            theParam.AssetName = assetname;
            
            var playerBuilder = new PlayerBuilder();
            playerBuilder.SetBuildParam(theParam);
            charactorBuildSystem.Construct(playerBuilder);
            return theParam.NewCharactor;
        }

        public override ICharactor_New CreateEnemy()
        {
            return null;
        }
    } 
} 
