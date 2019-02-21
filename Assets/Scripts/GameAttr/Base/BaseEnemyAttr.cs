using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameAttr.Base
{
    public class BaseEnemyAttr : BaseCharactorAttr
    {
        //暴击率
        private readonly int CritRate = 0;
        
        //初始化
        public BaseEnemyAttr(float walkSpeed, float runSpeed, float jumpHeight, string assetName, int maxHp,int critRate) : base(
            walkSpeed, runSpeed, jumpHeight, assetName, maxHp)
        {
            CritRate = critRate;
        }

        //得到暴击率
        public int GetCritRate()
        {
            return CritRate;
        }
    } 
} 
