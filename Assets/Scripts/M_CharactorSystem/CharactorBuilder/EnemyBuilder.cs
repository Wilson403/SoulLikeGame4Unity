using System.Collections;
using System.Collections.Generic;
using AIMgr.CharactorAIMgr;
using GameAttr.AttrStrategy;
using M_CharactorSystem.M_Enemy;
using M_Factory;
using UnityEngine;

namespace M_CharactorSystem.CharactorBuilder
{
    /// <summary>
    /// 定义敌方角色独有的参数
    /// </summary>
    public class EnemyBuildParam : ICharactorBuildParam
    {
        
    }

    
    /// <summary>
    /// 定义敌方角色的组装方法
    /// </summary>
    public class EnemyBuilder : ICharactorBuilder
    {
        private EnemyBuildParam _theParam = null;
        
        //设置构建参数
        public override void SetBuildParam(ICharactorBuildParam theParam)
        {
            _theParam = theParam as EnemyBuildParam;
        }
        
        //加载角色模型
        public override void LoadAsset(int gameobjectID)
        {
            var assetfactory = MainFactory.GetAssetFactory();
            var go = assetfactory.LoadEnemy(_theParam.NewCharactor.GetAssetName());
           
            //模型初始化
            go.transform.position = _theParam.SpawnPosition;
            go.gameObject.name = "Enemy" + gameobjectID;
           
            _theParam.NewCharactor.SetCharactorModel(go);

            _theParam.GameObjectID = go.GetInstanceID();
        }

        //加入角色管理器
        public override void AddCharactorSystem(GameManageHub hub)
        {
            hub.AddEnemy(_theParam.NewCharactor as IEnemy);
        }

        public override void AddCharactorAttr()
        {
            var attrFactory = MainFactory.GetrAttrFactory();
            var attrID = _theParam.NewCharactor.GetAttrID();
            var attr = attrFactory.GetEnemyAttr(attrID);

            attr.SetAttrStrategy(new EnemyAttrStrategy());
            _theParam.NewCharactor.SetCharactorAttr(attr);
        }

        public override void AddWeapon()
        {
            //取得武器工厂
            var weaponFactory = MainFactory.GetWeaponFactory();
           
            //左右手武器的设置
            _theParam.NewCharactor.SetLWeapon(weaponFactory.CreatWeapon(_theParam.LWeaponType));
            _theParam.NewCharactor.SetRWeapon(weaponFactory.CreatWeapon(_theParam.RWeaponType));
        }

        public override void AddAI()
        {
            var enemyAi = new EnemyAI();
            _theParam.NewCharactor.SetCharactorAI(enemyAi);
        }
    }
} 
