/*
 * IAssetFactory ：资源工厂抽象类
 * 程序员 ：Wilson
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：None
 */
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace M_Factory.AssetFactory
{
    public abstract class IAssetFactory
    {
        public abstract GameObject LoadPlayer(string assetname);
        public abstract GameObject LoadEnemy(string assetname);
        public abstract GameObject LoadWeapon(string assetname);
    } 
} 
