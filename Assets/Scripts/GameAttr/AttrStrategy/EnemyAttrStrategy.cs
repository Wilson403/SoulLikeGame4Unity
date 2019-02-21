using System.Collections;
using System.Collections.Generic;
using GameAttr.CharactorAttr;
using UnityEngine;

namespace GameAttr.AttrStrategy
{
    public class EnemyAttrStrategy : IAttrStrategy
    {
        //初始化属性
        public override void InitAttr(ICharactorAttr theCharactorAttr)
        {
            
        }

        //伤害加成
        public override int GetAtkplusValue(ICharactorAttr theCharactorAttr)
        {
            var enemyAttr = theCharactorAttr as EnemyAttr;
            var randomvalue = Random.Range(1, 100);

            if (enemyAttr.GetCritRate() >= randomvalue)
            {
                return (int) (enemyAttr.GetMaxHp() * 0.5f);
            }

            return 0;
        }

        //减伤
        public override int GetDmgDesValue(ICharactorAttr theCharactorAttr)
        {
            return 0;
        }
    }
} 
