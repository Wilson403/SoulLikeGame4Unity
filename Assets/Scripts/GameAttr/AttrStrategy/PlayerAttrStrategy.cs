/*
 * PlayerAttrStrategy ：计算策略子类（玩家）
 * 程序员 ：Wilson
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：None
 */

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using GameAttr.CharactorAttr;

namespace GameAttr.AttrStrategy
{
    public class PlayerAttrStrategy : IAttrStrategy
    {

    #region Override_Methods

        //属性初始化
        public override void InitAttr(ICharactorAttr theCharactorAttr)
        {
            var theAttr = theCharactorAttr as PlayerAttr;
            if (theAttr == null)
            {
                return;
            }

            var addHp = 0;
            var lv = theAttr.GetLevel();
            if (lv > 0)
            {
                addHp = (lv - 1) * 2;
            }
            
            theAttr.AddHp(addHp);
            //theAttr.Init(theAttr.GetMaxHp());
        }

        //攻击加成
        public override int GetAtkplusValue(ICharactorAttr theCharactorAttr)
        {
            return 0;
        }

        //获取减少伤害值
        //等级越高，减伤越高
        public override int GetDmgDesValue(ICharactorAttr theCharactorAttr)
        {
//            var theAttr = theCharactorAttr as PlayerAttr;
//            if (theAttr == null)
//            {
//                return 0;
//            }
//
//            return (theAttr.GetLevel() - 1) * 2;
            return 0;
        }

    #endregion
        
    }// Class_End
}

