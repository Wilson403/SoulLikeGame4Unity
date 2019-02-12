/*
 * MainFactory ：
 * 程序员 ：Wilson
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：None
 */
 
using System.Collections;
using System.Collections.Generic;
using M_Factory.AssetFactory;
using M_Factory.CharactorFactory;
using UnityEngine;

namespace M_Factory
{
    /// <summary>
    /// 取得所有的工厂
    /// </summary>
    public static class MainFactory
    {
        private static IAssetFactory _assetFactory = null;
        private static ICharactorFactory _charactorFactory = null;
        public static IAssetFactory GetAssetFactory()
        {
            if (_assetFactory == null)
            {
                _assetFactory = new ResourcesProxyFactory();
            }

            return _assetFactory;
        }

        public static ICharactorFactory GetCharactorFactory()
        {
            if(_charactorFactory == null)
            {
                _charactorFactory = new CharactorFactory_General();
            }

            return _charactorFactory;
        }
    } 
} 
