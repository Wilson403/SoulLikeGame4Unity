using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameAttr.Base
{
    public class BaseEnemyAttr : BaseCharactorAttr 
    {
        public BaseEnemyAttr(float walkSpeed, float runSpeed, float jumpHeight, string assetName, int maxHp) : base(
            walkSpeed, runSpeed, jumpHeight, assetName, maxHp)
        {

        }
    } 
} 
