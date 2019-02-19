/*
 * IAttrStrategy ：定义了与攻击有关的计算方法
 * 程序员 ：Wilson
 * 挂载对象 ：None
 * 更多描述 ：Strategy类
 * 修改记录 ：None
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAttr.CharactorAttr;

namespace GameAttr.AttrStrategy
{
    public abstract class IAttrStrategy
    {
        //初始化属性
        public abstract void InitAttr(ICharactorAttr theCharactorAttr);
        //伤害加成
        public abstract int GetAtkplusValue(ICharactorAttr theCharactorAttr);
        //减伤
        public abstract int GetDmgDesValue(ICharactorAttr theCharactorAttr);                                         
    }
}


