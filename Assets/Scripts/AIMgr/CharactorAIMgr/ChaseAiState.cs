/*
 * ChaseAIState ：继承于IAIState,负责Ai对象追赶的逻辑
 * 程序员 ：Wilson
 * 日期 ：2019/01/06
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：1.从Idle状态切入会导致角色旋转延迟
 */
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using M_CharactorSystem;

namespace AIMgr.CharactorAIMgr
{
    public class ChaseAiState : IAIState
    {
        
   
        
        private readonly ICharactor _chasetarget = null; //追击的目标
        

        
        
      
        
    
        
        //建造者
        public ChaseAiState(ICharactor theTarget)
        {
            _chasetarget = theTarget;
        }
        
    
        
        public override void Start()
        {
            //设置当前的Ai状态为Chase（追赶）
            CharactorAi.SetCurAiState(AiState.Chase);
        }

        public override void Update(List<ICharactor> targets)
        {
            //如果目标为空或者死亡，切换回待机模式
            if (_chasetarget == null || _chasetarget.BKilled)
            {
                CharactorAi.ChangeAiState(new IdleAIState());
                return;
            }

            //如果目标在可攻击的范围内，切换到攻击模式
            if (CharactorAi.TargetRange(_chasetarget, 2))
            {
                CharactorAi.StopMove();
                CharactorAi.ChangeAiState(new AttackAIState(theAttackTarget: _chasetarget));
                return;
            }

            //追赶的目标距离过远，回到待机
            if (!CharactorAi.TargetRange(_chasetarget, 15))
            {
                CharactorAi.ChangeAiState(new IdleAIState());
            }

            CharactorAi.Moveto(_chasetarget.GetModel().transform.position);
        }


        
    } //Class_End
	
} //NameSpace_End