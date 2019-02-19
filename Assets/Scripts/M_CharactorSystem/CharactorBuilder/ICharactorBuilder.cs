using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace M_CharactorSystem.CharactorBuilder
{
    /// <summary>
    /// 定义角色功能的组装方法
    /// </summary>
    public abstract class ICharactorBuilder
    {
        /// <summary>
        /// 设置构建参数
        /// </summary>
        /// <param name="theParam"></param>
        public abstract void SetBuildParam(ICharactorBuildParam theParam);

        /// <summary>
        /// 加载角色模型
        /// </summary>
        /// <param name="gameobjectID"></param>
        public abstract void LoadAsset(int gameobjectID);
        
        /// <summary>
        /// 加入角色管理器
        /// </summary>
        /// <param name="hub"></param>
        public abstract void AddCharactorSystem(GameManageHub hub);

        /// <summary>
        /// 添加角色属性
        /// </summary>
        public abstract void AddCharactorAttr();

        /// <summary>
        /// 加入相机
        /// </summary>
        public virtual void AddCamera(){}

        /// <summary>
        /// 加入控制器
        /// </summary>
        public virtual void AddController(){}

        /// <summary>
        /// 加入动画管理器
        /// </summary>
        public virtual void AddActionManager(){}

        /// <summary>
        /// 加入武器
        /// </summary>
        public abstract void AddWeapon();
    } 
} 
