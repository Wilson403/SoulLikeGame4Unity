/*
 * IAIState ：角色的AI状态抽象类。
 * 程序员 ：Wilson
 * 日期 ：2018/12/15
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：2019/1/6 -添加了初始化方法Start()
 */

using System.Collections;
using System.Collections.Generic;
using M_CharactorSystem;
using UnityEngine;


//AI状态枚举
public enum AiState
{
    Idle, //待机
    Chase, //追赶
    Attack //攻击
}

namespace AIMgr.CharactorAIMgr
{
    public abstract class IAIState
    {
        //角色AI，状态拥有者
        protected ICharactorAI CharactorAi = null; 
        
        /// <summary>
        /// 设置ICharactorAI对象
        /// </summary>
        /// <param name="charactorAi"></param>
        public void SetCharactorAi(ICharactorAI charactorAi)
        {
            CharactorAi = charactorAi;
        }

        /// <summary>
        /// 设置要攻击的目标
        /// </summary>
        /// <param name="attckPos"></param>
        public virtual void SetAttackPosition(Vector3 attckPos)
        {
            
        }

        /// <summary>
        /// 只执行一次（非Unity生命周期函数Start）
        /// </summary>
        public virtual void Start()
        {
            
        }

        /// <summary>
        /// 更新（非Unity生命周期函数Update）
        /// </summary>
        /// <param name="targets"></param>
        public virtual void Update(List<ICharactor> targets)
        {
            
        }

        /// <summary>
        /// 移除目标
        /// </summary>
        /// <param name="target"></param>
        public virtual void RemoveTarget(ICharactor target)
        {
            
        }

    } //Class_End
    
} //NameSpace_End