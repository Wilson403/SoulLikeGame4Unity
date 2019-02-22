using System.Collections;
using System.Collections.Generic;
using GameAttr.WeaponAttr;
using UnityEngine;

namespace M_CharactorSystem.CharactorBuilder
{ 
    /// <summary>
    /// 定义角色组装的参数
    /// </summary>
    public abstract class ICharactorBuildParam
    {
        public ICharactor NewCharactor = null; //角色对象
        public Vector3 SpawnPosition; //位置
        public string AssetName; //角色模型的资源名
        public int CharactorID;
        public int WeaponID;
        public WeaponType LWeaponType = WeaponType.None;
        public WeaponType RWeaponType = WeaponType.None;
    } 
} 
