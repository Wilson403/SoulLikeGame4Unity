/*
 * AttackAIState ：
 * 程序员 ：Wilson
 * 日期 ：2018/01/01
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：None
 */
 
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using M_CharactorSystem;
using UnityEngine;

namespace AIMgr.CharactorAIMgr
{
    public class AttackAIState : IAIState
    {
        private readonly ICharactor _attackTarget = null;
        private ChaseAiState _chaseAi = null;

        public AttackAIState(ICharactor theAttackTarget)
        {
            _attackTarget = theAttackTarget;
        }

        public override void Start()
        {
            
            _chaseAi = new ChaseAiState(_attackTarget);
            //设置当前的Ai状态为Attack
            CharactorAi.SetCurAiState(AiState.Attack);
        }

        //测试阶段，Ai一直处于锁敌状态
        //配合动画事件调整锁敌频率(未实现)
        public override void Update(List<ICharactor> targets)
        {
            base.Update(targets);
            CharactorAi.LookTarget(_attackTarget);
            
            if (CharactorAi.TargetRange(_attackTarget,2))
            {
                CharactorAi.Attack(_attackTarget);
                if (_attackTarget.GetHpState())
                {
                    CharactorAi.ChangeAiState(new IdleAIState());
                }
            }
            else
            {
                CharactorAi.ChangeAiState(_chaseAi);
            }
        }

    } //Class_End
	
} //NameSpace_End
