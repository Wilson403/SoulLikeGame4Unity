using System.Collections;
using System.Collections.Generic;
using GameAttr.Base;
using UnityEngine;

namespace GameAttr.CharactorAttr
{
    public class EnemyAttr : ICharactorAttr
    {
        private int _critrate;

        public void SetEnemyAttr(BaseEnemyAttr baseAttr)
        {
            SetAttr(baseAttr);
            _critrate = baseAttr.GetCritRate();
        }


        public int GetCritRate()
        {
            return _critrate;
        }

    }
}

