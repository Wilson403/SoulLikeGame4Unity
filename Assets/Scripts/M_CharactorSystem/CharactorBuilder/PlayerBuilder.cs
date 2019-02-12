/*
 * PlayerBuilder ：
 * 程序员 ：Wilson
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：None
 */
 
using System.Collections;
using System.Collections.Generic;
using M_Factory;
using M_Factory.AssetFactory;
using UnityEngine;

namespace M_CharactorSystem.CharactorBuilder
{
    public class PlayerBuildParam : ICharactorBuildParam
    {
        public int Lv;
    }

    public class PlayerBuilder : ICharactorBuilder
    {
       private PlayerBuildParam _theParam = null;

       public override void SetBuildParam(ICharactorBuildParam theParam)
       {
           _theParam = theParam as PlayerBuildParam;
       }

       public override void LoadAsset()
       {
           var assetFactory = MainFactory.GetAssetFactory();
           var go = assetFactory.LoadPlayer(_theParam.NewCharactor.GetAssetName());
       }
    }
} 
