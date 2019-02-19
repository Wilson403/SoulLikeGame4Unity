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
using GameAttr.Base;

namespace GameAttr.CharactorAttr
{
    public abstract class ICharactorAttr
    {
        protected BaseAttr m_BaseAttr = null;
        protected int InitHarmValue;
        protected int MaxHarmValue;
        private int _nowHp; //当前生命值
        private IAttrStrategy _attrStrategy = null; //属性的计算策略

        //设置基础属性
        public void SetAttr(BaseAttr baseAttr)
        {
            m_BaseAttr = baseAttr;
        }

        //取得基础属性
        public BaseAttr GetAttr()
        {
            return m_BaseAttr;
        }

        //设置计算策略
        public void SetAttrStrategy(IAttrStrategy theAttrStrategy)
        {
            _attrStrategy = theAttrStrategy;
        }
        
        //获取当前生命最大值
        public virtual int GetMaxHp()
        {
            return m_BaseAttr.GetMaxHp();
        }

        //回满血
        public void FullHp()
        {
            _nowHp = GetMaxHp();
        }

        //获取当前生命值
        public int GetNowHp()
        {
            return _nowHp;
        }

        public virtual int GetMaxHarmValue()
        {
            return MaxHarmValue;
        }

        //属性初始化
        public void InitAttr()
        {
            _attrStrategy.InitAttr(this);
            FullHp();
        }

        //得到伤害附加值
        public int GetAtkplusValue()
        {
            return _attrStrategy.GetAtkplusValue(this);
        }

        //计算被攻击后的生命值
        public void GetRemainHp(ICharactor theAttacker)
        {
            //获取攻击者的攻击力
            var atkValue = theAttacker.GetAtkValue();
            //减少伤害值
            atkValue -= _attrStrategy.GetDmgDesValue(this);
            //计算生命值
            _nowHp -= atkValue;
        }

        public float GetWalkSpeed()
        {
            return m_BaseAttr.GetWalkSpeed();
        }

        public float GetRunSpeed()
        {
            return m_BaseAttr.GetRunSpeed();
        }

        public float GetJumpHeight()
        {
            return m_BaseAttr.GetJumpHeight();
        }

        public string GetAssetName()
        {
            return m_BaseAttr.GetAssetName();
        }


    }

}

