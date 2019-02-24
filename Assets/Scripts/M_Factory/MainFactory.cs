/*
 * MainFactory ：
 * 程序员 ：Wilson
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：None
 */
 
using System.Collections;
using System.Collections.Generic;
using GameAttr.CharactorAttr;
using M_Factory.AssetFactory;
using M_Factory.AttriableFactory;
using M_Factory.CharactorFactory;
using M_Factory.WeaponFactory;
using UnityEngine;

namespace M_Factory
{
    /// <summary>
    /// 取得所有的工厂
    /// </summary>
    public class MainFactory : MonoBehaviour
    {
        private static MainFactory _instance;

        public static MainFactory Instance
        {
            get { return _instance; }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
//            else
//            {
//                DestroyImmediate(this);
//            }
        }

        private static IAssetFactory _assetFactory = null;
        private static ICharactorFactory _charactorFactory = null;
        private static IAttrFactory _attrfactory = null;
        private static IWeaonFactory _weaonFactory = null;
       
        
        public IAssetFactory GetAssetFactory()
        {
            if (_assetFactory == null)
                _assetFactory = new ResourcesProxyFactory();

            return _assetFactory;
        }

        public ICharactorFactory GetCharactorFactory()
        {
            if (_charactorFactory == null)
                _charactorFactory = new CharactorFactory_Switch();
  
            return _charactorFactory;
        }
        
        public IAttrFactory GetrAttrFactory()
        {
            if (_attrfactory == null)
                _attrfactory = new AttrFactory();

            return _attrfactory;
        }
        
        public IWeaonFactory GetWeaponFactory()
        {
            if (_weaonFactory == null)
                _weaonFactory = new M_WeaponFactory();

            return _weaonFactory;
        }



    } 
} 
