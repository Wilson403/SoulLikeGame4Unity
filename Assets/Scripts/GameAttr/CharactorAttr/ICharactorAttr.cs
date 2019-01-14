/*
 * ICharactorAttr ：从ICharactor类中独立出来的属性类
 * 程序员 ：Wilson
 * 挂载对象 ：None
 * 更多描述 ：1.该类同时作为Context类，与属性计算类组（GameAttr.AttrStrategy）共同组成策略模式
 *           2.该类拥有一个_attrStrategy引用
 *           3.角色类（ICharactor）拥有一个ICharactorAttr引用
 * 修改记录 ：None
 */

using System.Collections;
using System.Collections.Generic;
using M_CharactorSystem;
using UnityEngine;
using GameAttr.AttrStrategy;

namespace GameAttr.CharactorAttr
{
    public abstract class ICharactorAttr
    {

    #region Protected_variable

        protected int MaxHp; //最大生命值
        protected float JumpHeight = 3; //跳跃高度
        protected float RollForwardSpeed = 0.0f; //前滚速度
        protected Vector3 RollbackSpeed; //后滚速度

    #endregion


        //------------------------------------------------------------------------------


    #region Private_variable

        private int _nowHp; //当前生命值
        private IAttrStrategy _attrStrategy = null; //属性的计算策略

    #endregion


        //------------------------------------------------------------------------------


    #region Public_Methods

        //设置计算策略
        public void SetAttrStrategy(IAttrStrategy theAttrStrategy)
        {
            _attrStrategy = theAttrStrategy;
        }

        //血量初始化
        public void Init(int maxHp)
        {
            MaxHp = maxHp;
            _nowHp = MaxHp;
        }

        //获取当前生命值
        public int GetNowHp()
        {
            return _nowHp;
        }

        //获取当前生命最大值
        public virtual int GetMaxHp()
        {
            return MaxHp;
        }

        //属性初始化
        public void InitAttr()
        {
            _attrStrategy.InitAttr(this);
        }

        //得到武器伤害附加值
        public int GetAtkplusValue()
        {
            return _attrStrategy.GetAtkplusValue(this);
        }

        //计算被攻击后的生命值
        public void GetDmgDesValue(ICharactor theAttacker)
        {
            //获取攻击者的攻击力
            int atkValue = theAttacker.GetAtkValue();
            //减少伤害值
            atkValue -= _attrStrategy.GetDmgDesValue(this);
            //计算生命值
            _nowHp -= atkValue;
        }

    #endregion

    }

}

