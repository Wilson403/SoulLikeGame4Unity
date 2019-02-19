using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameAttr.Base
{
    public abstract class BaseAttr
    {
        public abstract float GetWalkSpeed();
        public abstract float GetRunSpeed();
        public abstract float GetJumpHeight();
        public abstract string GetAssetName();
        public abstract int GetMaxHp();
    }

} 
