/*
 * PlayerBuilder ：
 * 程序员 ：Wilson
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：None
 */
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace M_CharactorSystem.CharactorBuilder
{
    public class PlayerBuildParam : ICharactorBuildParam
    {

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
           
       }
    }
} 
