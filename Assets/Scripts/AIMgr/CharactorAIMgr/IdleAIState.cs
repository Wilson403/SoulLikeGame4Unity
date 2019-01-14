using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using M_CharactorSystem;

namespace AIMgr.CharactorAIMgr
{
    public class IdleAIState : IAIState
    {
        private bool _isSetAtkTarget = false; //是否设置了攻击目标

        public IdleAIState()
        {
           
        }

        public override void SetAttackPosition(Vector3 attckPos)
        {
            _isSetAtkTarget = true;
        }

        public override void Start()
        {
            CharactorAi.SetCurAiState(AiState.Idle);
        }

        public override void Update(List<ICharactor> targets)
        {
            Vector3 nowPosition = CharactorAi.GetPosition();
            ICharactor theNearTarget = null;
            float minDist = 999f;

            foreach (var target in targets)
            {
                if (target != null)
                {
                    if (target.BKilled)
                        continue;

                    float dist = Vector3.Distance(nowPosition, target.transform.position);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        theNearTarget = target;
                    }
                }
            }
            
            Debug.Log(theNearTarget);

            if (theNearTarget == null)
            {
                return;
            }

            //如果检测到目标在攻击范围内，则切换到攻击状态
            if (CharactorAi.TargetRange(theNearTarget,2))
            {
                CharactorAi.ChangeAiState(new AttackAIState(theAttackTarget: theNearTarget));
            }
            else //如果不在攻击范围内则切换到追赶状态
            {
                //在有效视野内才可以进行锁定追击
                if (CharactorAi.UsefulView(theNearTarget))
                {
                    CharactorAi.StopMove();
                    CharactorAi.ChangeAiState(new ChaseAiState(theTarget: theNearTarget));
                }
            }
        }
        
    }//Class_End
    
}//NameSpace_End

