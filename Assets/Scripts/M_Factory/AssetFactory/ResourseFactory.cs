/*
 * ResourseFactory ：Resourcse加载
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
    public class ResourseFactory : IAssetFactory
    {
        //角色路径
        public const string PlayerPath = "Charactor/Player";

        //敌人路径
        public const string EnemyPath = "Charactor/Enemy";

        //武器路径
        public const string WeaponPath = "Charactor/Weapon";


        //==========================================================================


        //加载角色
        public override GameObject LoadPlayer(string assetname)
        {
            return InstanceGameObject(Path.Combine(PlayerPath, assetname));
        }

        //加载敌人
        public override GameObject LoadEnemy(string assetname)
        {
            return InstanceGameObject(Path.Combine(EnemyPath, assetname));
        }

        //加载武器
        public override GameObject LoadWeapon(string assetname)
        {
            return InstanceGameObject(Path.Combine(WeaponPath, assetname));
        }

        /// <summary>
        /// 实例化GameObject
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private GameObject InstanceGameObject(string path)
        {
            var res = LoadAssetFromResourcesPath(path);
            return Object.Instantiate(res) as GameObject;
        }

        /// <summary>
        /// Resourcse加载
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Object LoadAssetFromResourcesPath(string path)
        {
            var res = Resources.Load(path);
            if (res == null)
            {
                Debug.Log("路径加载失败" + path);
            }

            return res;
        }
    }
}
