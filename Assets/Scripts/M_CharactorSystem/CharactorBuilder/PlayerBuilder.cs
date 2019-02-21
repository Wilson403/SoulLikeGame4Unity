using System.Collections;
using System.Collections.Generic;
using GameAttr.AttrStrategy;
using M_AnimationManager.PlayerAnimation;
using M_CharactorSystem.M_Player;
using M_ControllerSystem;
using M_Factory;
using UnityEngine;

namespace M_CharactorSystem.CharactorBuilder
{
    /// <summary>
    /// 定义我方角色独有的参数
    /// </summary>
    public class PlayerBuildParam : ICharactorBuildParam
    {
        public int Lv = 0;
    }

    
    /// <summary>
    /// 定义我方角色的组装方法
    /// </summary>
    public class PlayerBuilder : ICharactorBuilder
    {
       private PlayerBuildParam _theParam = null;

       public override void SetBuildParam(ICharactorBuildParam theParam)
       {
           _theParam = theParam as PlayerBuildParam;
       }

       public override void LoadAsset(int gameobjectID)
       {
           //使用资源工厂加载出角色模型
           var assetfactory = MainFactory.GetAssetFactory();
           var go = assetfactory.LoadPlayer(_theParam.NewCharactor.GetAssetName());
           
           //模型初始化
           go.transform.position = _theParam.SpawnPosition;
           go.gameObject.name = "Player" + gameobjectID;
           
           _theParam.NewCharactor.SetCharactorModel(go);
           _theParam.GameObjectID = go.GetInstanceID();
       }

       public override void AddCamera()
       {
           //添加相机控制组件
           var camera = _theParam.NewCharactor.GetCameraPoint().AddComponent<CameraControl>();
           
           //组件参数设置
           camera.SetModel(_theParam.NewCharactor.GetModel());
           camera.SetCharactor(_theParam.NewCharactor);
           
           _theParam.NewCharactor.SetCameraControl(camera);
       }

       public override void AddController()
       {
           //初始化控制器状态，默认为手柄状态
           _theParam.NewCharactor.SetController(new JoyStrickInput(), ControllerState.JoyStrick);
       }

       public override void AddCharactorSystem(GameManageHub hub)
       {
           hub.AddPlayer(_theParam.NewCharactor as IPlayer);
       }

       public override void AddActionManager()
       {
           var actionManager = new ActionManager(_theParam.NewCharactor);
           
           //给角色设置好动作管理器
           _theParam.NewCharactor.SetActionManager(actionManager);
           _theParam.NewCharactor.SetAnimation(new PlayerGeneralAnimationMgr(actionManager),
               new PlayerEqipAnimationMgr(actionManager),
               new PlayerUnEqipAnimationMgr(actionManager));
       }

       public override void AddCharactorAttr()
       {
           var attrFactory = MainFactory.GetrAttrFactory();
           var attrID = _theParam.NewCharactor.GetAttrID();
           var attr = attrFactory.GetPlayerAttr(attrID);

           attr.SetLevel(_theParam.Lv);
           attr.SetAttrStrategy(new PlayerAttrStrategy());
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
    }
} 
