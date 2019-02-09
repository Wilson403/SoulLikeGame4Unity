/*
 * ResourcesProxyFactory ：ResourceFactory代理，在ResourceFactory的基础上加入了缓存的功能
 * 程序员 ：Wilson
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：None
 */
 
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace M_Factory.AssetFactory
{
    public class ResourcesProxyFactory : IAssetFactory
    {
        //实际负责载入者
        private readonly ResourseFactory _resourseFactory;
        private readonly Dictionary<string, Object> _playerDict;
        private readonly Dictionary<string, Object> _enemyDict;
        private readonly Dictionary<string, Object> _weaponDict;


        //==========================================================================


        //初始化
        public ResourcesProxyFactory()
        {
            _resourseFactory = new ResourseFactory();
            _playerDict = new Dictionary<string, Object>();
            _enemyDict = new Dictionary<string, Object>();
            _weaponDict = new Dictionary<string, Object>();
        }

        //加载角色
        public override GameObject LoadPlayer(string assetname)
        {
            if (!_playerDict.ContainsKey(assetname))
            {
                var res = _resourseFactory.LoadAssetFromResourcesPath(Path.Combine(ResourseFactory.PlayerPath,
                    assetname));
                _playerDict.Add(assetname, res);
            }

            return Object.Instantiate(_playerDict[assetname]) as GameObject;
        }

        //加载敌人
        public override GameObject LoadEnemy(string assetname)
        {
            if (!_enemyDict.ContainsKey(assetname))
            {
                var res = _resourseFactory.LoadAssetFromResourcesPath(Path.Combine(ResourseFactory.EnemyPath,
                    assetname));
                _enemyDict.Add(assetname, res);
            }

            return Object.Instantiate(_enemyDict[assetname]) as GameObject;
        }

        //加载武器
        public override GameObject LoadWeapon(string assetname)
        {
            if (!_weaponDict.ContainsKey(assetname))
            {
                var res = _resourseFactory.LoadAssetFromResourcesPath(Path.Combine(ResourseFactory.WeaponPath,
                    assetname));
                _weaponDict.Add(assetname, res);
            }

            return Object.Instantiate(_weaponDict[assetname]) as GameObject;
        }
    }
}
 
