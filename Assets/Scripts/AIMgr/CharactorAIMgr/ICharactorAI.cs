/*
 * ICharactorAI ：角色AI抽象类，拥有当前的AI状态。
 * 程序员 ：Wilson
 * 日期 ：2018/12/15
 * 挂载对象 ：None
 * 更多描述 ：它负责Ai状态的切换，同时提供给Ai子状态一些常用的方法
 * 修改记录 ：None
 */

using System.Collections;
using System.Collections.Generic;
using M_CharactorSystem;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace AIMgr.CharactorAIMgr
{
    public abstract class ICharactorAI
    {

    #region Protected_Variable

        protected float AttackRadius = 2; //可以攻击的检测范围

        protected Vector3 AttackPostion = Vector3.zero;

        protected ICharactor TheTarget = null; //受Ai控制的对象

    #endregion


        //----------------------------------------------------------------------


    #region Private_Variable

        private bool _beginRunStart = false; //是否首次执行初始化方法

        private IAIState _aiState = null; //拥有一个AI状态

    #endregion


        //----------------------------------------------------------------------


    #region Attibute

        public AiState CurAiState { get; private set; } //当前Ai状态

    #endregion


        //----------------------------------------------------------------------


    #region Virtual_Methods

        //更换AI状态
        public virtual void ChangeAiState(IAIState state)
        {
            _aiState = state;
            _aiState.SetCharactorAi(this);
            _beginRunStart = true;
        }

        //AI状态更新
        public virtual void Update(List<ICharactor> targets)
        {
            if (_beginRunStart)
            {
                _aiState.Start();
                _beginRunStart = false;
            }

            _aiState.Update(targets);
        }

        //是否在指定距离内
        public virtual bool TargetRange(ICharactor theNearTarget, int range)
        {
            return false;
        }

    #endregion


        //----------------------------------------------------------------------


    #region Public_Methods

        //移除AI对象
        public void RemoveAiTarget(ICharactor theTarget)
        {
            _aiState.RemoveTarget(theTarget);
        }

        //Ai对象设置
        public void SetAiCharactor(ICharactor theTarget)
        {
            TheTarget = theTarget;
        }

        //设置Ai状态
        public void SetCurAiState(AiState theAiState)
        {
            CurAiState = theAiState;
        }

        //获取AI角色的位置
        public Vector3 GetPosition()
        {
            return TheTarget.transform.position;
        }

        //停止移动
        public void StopMove()
        {
            TheTarget.StopMove();
        }
        
        //移动至目标
        public void Moveto(Vector3 theTargetPosition)
        {
            TheTarget.MoveTo(theTargetPosition);
        }

        //攻击目标
        public void Attack(ICharactor theTarget)
        {
            TheTarget.Attack(theTarget);
        }

        public void LookTarget(ICharactor theTarget)
        {
            TheTarget.LookTarget(theTarget);
        }

        public float UsefulView(ICharactor theTarget)
        {
           return TheTarget.UsefulView(theTarget);
        }



    #endregion

    } //Class_End

} //NameSpace_End
