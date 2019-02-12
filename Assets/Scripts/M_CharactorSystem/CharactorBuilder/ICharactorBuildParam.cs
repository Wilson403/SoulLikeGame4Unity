/*
 * ICharactorBuildParam ：
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
    public abstract class ICharactorBuildParam
    {
        public ICharactor_New NewCharactor = null;
        public Vector3 SpawnPosition;
        public string AssetName;
    } 
} 
