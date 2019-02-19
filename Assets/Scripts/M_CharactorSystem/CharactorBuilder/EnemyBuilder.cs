using System.Collections;
using System.Collections.Generic;
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
        
        public override void AddCamera()
        {
           
        }

        //加载角色模型
        public override void LoadAsset(int gameobjectID)
        {
            
        }

        //加入角色管理器
        public override void AddCharactorSystem(GameManageHub hub)
        {
            
        }

        public override void AddCharactorAttr()
        {
            
        }

        public override void AddWeapon()
        {
            
        }
    }
} 
