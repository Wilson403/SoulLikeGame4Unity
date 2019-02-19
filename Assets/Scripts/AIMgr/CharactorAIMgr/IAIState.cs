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

        protected ICharactorAI CharactorAi = null; //角色AI，状态拥有者
        
        //设置ICharactorAI对象
        public void SetCharactorAi(ICharactorAI charactorAi)
        {
            CharactorAi = charactorAi;
        }

        public virtual void SetAttackPosition(Vector3 attckPos)
        {
            //设置要攻击的目标
        }

        public virtual void Start()
        {
            //只执行一次（非Unity生命周期函数Start）
        }

        public virtual void Update(List<ICharactor> targets)
        {
            //更新（非Unity生命周期函数Update）
        }

        public virtual void RemoveTarget(ICharactor target)
        {
            //移除目标
        }

    } //Class_End
    
} //NameSpace_End