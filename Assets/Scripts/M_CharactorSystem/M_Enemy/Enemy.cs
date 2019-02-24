/*
 * Enemy ：敌人类，实现类
 * 程序员 ：Wilson
 * 日期 ：2018/12/15
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：None
 */

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;
using AIMgr;
using AIMgr.CharactorAIMgr;
using M_CharactorSystem.M_Enemy;
using M_Factory;
using M_Factory.AssetFactory;
using M_Factory.AttriableFactory;

namespace M_CharactorSystem.M_Enemy
{
    public class Enemy : IEnemy
    {
        public Enemy()
        {
            m_AttrID = 1;
            m_Name = "Enemy001";
            m_Assetname = "Enemy";
        }

        public override void Init()
        {
            base.Init();
            
            Targets = new List<ICharactor>();
            
            EventMgr.Instance.AddListener(EventMgr.EVENT_TYPE.EnemyFsmEnter, EnterEvent); //进入状态

            SetNextTarget(false);
            
            m_Animator.SetTrigger("ToEqip");
            m_Animator.SetFloat("Z", 1);
            m_AI.ChangeAiState(new IdleAIState());
            m_AI.SetAiCharactor(theTarget: this);  
        }
     

        public override void Update()
        {
            base.Update();
            CheckBOnGround();
            FindEnemy();
            AiStateUpdate(Targets);
            

            _hasPath = _agent.hasPath;
            _pathPending = _agent.pathPending;
            PathStatus = _agent.pathStatus;

            if (m_AI.CurAiState == AiState.Idle)
            {
                WaitNextTarget();
                _agent.speed = WalkSpeed;
            }
            else
            {
                _agent.speed = RunSpeed;
                if (Targets.Count != 0)
                {
                    Targets.Clear();
                }
            }
            
            StopTime();
            
            if (m_AI.CurAiState == AiState.Idle || m_AI.CurAiState == AiState.Chase)
            {
                SetMoveState(_isStop);
            }
            else
            {
                SetMoveState(true);
            }
          
          
        }

  
        //停止移动
        public override void StopMove()
        {
            SetMoveState(isStop: true);
        }

        //前往某目标位置
        public override void MoveTo(Vector3 theTargetPosition)
        {
            _agent.isStopped = _isStop;
            _agent.destination = theTargetPosition;
        }

        //攻击
        public override void Attack(ICharactor theTarget)
        {
            m_Animator.SetTrigger("LAttack");
        }

        //锁定目标
        public override void LookTarget(ICharactor theTarget)
        {
            var direction = theTarget.GetModel().transform.position - m_CharactorHandle.transform.position;
            var lookRotation = Quaternion.LookRotation(direction);
            m_CharactorHandle.transform.rotation = Quaternion.Slerp(m_Model.transform.rotation, lookRotation, 0.1f);
        }

   
        //设置移动状态（移动 or 停止）
        private void SetMoveState(bool isStop)
        {
            SetMoveAnim(isStop ? 0 : m_AI.CurAiState == AiState.Chase ? 2 : 1);
            _agent.isStopped = isStop;
        }

        //导航至下个固定目标点
        //由于要实现到达每个固定点后停留一段时间，所以使用协程
        private void WaitNextTarget()
        {
            if (_agent.remainingDistance < 1f)
            {
                _isStop = true;
                SetNextTarget(true); //自动遍历true
                IsAttach = true;
            }
        #region 旧的巡航解决方案

            //路径计算完成的情况下 || 无效路径 || 无法到达的路径
//            if ((!_hasPath && !_pathPending) || PathStatus == NavMeshPathStatus.PathInvalid ||
//                PathStatus == NavMeshPathStatus.PathPartial)
//            {
//                _isStop = true;  
//                SetNextTarget(true); //自动遍历true
//                yield return new WaitForSeconds(2f); //停留2秒 
//                _isStop = false;
//            }

        #endregion
        }

        private void StopTime()
        {
            if (IsAttach)
            {
                Timer += Time.deltaTime;
                if (Timer > StayTime)
                {
                    _isStop = false;
                    IsAttach = false;
                    Timer = 0.0f;
                }
            }
        }

       

        //设置走路动画
        private float SetMoveAnim(int num)
        {
            m_Animator.SetFloat("Z", Mathf.Lerp(m_Animator.GetFloat("Z"), num, 0.1f));
            return m_Animator.GetFloat("Z");
        }

        //寻找周围的对象
        private void FindEnemy()
        {
            Collider[] hitColliders;
            ExplosionDamage(m_Model.transform.position, FindRadius, out hitColliders);
            if (hitColliders.Length > 0)
            {
                if (Targets.Count < hitColliders.Length)
                {
                    for (var i = 0; i < hitColliders.Length; i++)
                    {
                        //将检测到的角色加入目标列表
                        //注意，空对象也会加入。使用前要检查是否存在空对象
                        var id = hitColliders[i].gameObject.GetInstanceID();
                        Targets.Add(MainFactory.Instance.GetCharactorFactory().GetPlayer(id));
                    }
                }
            }
        }


        //球形碰撞器用于检测范围内的目标
        private void ExplosionDamage(Vector3 center, float radius, out Collider[] hitColliders)
        {
            hitColliders = Physics.OverlapSphere(center, radius, 1 << 11);
        }

        /// <summary>
        /// 固定路径点导航
        /// </summary>
        /// <param name="isIncrease">到达导航点后是否自动前往下个导航点</param>
        private void SetNextTarget(bool isIncrease)
        {
            //增长步数
            int incStep = isIncrease ? 1 : 0;
            //下一个路径点
            Transform nextTargeTransform = null;

            while (nextTargeTransform == null)
            {
                //解决第一个路径点为空时会出现死循环的Bug
                if (WallpointNetWork.WallPoints[0] == null && CurrentIndex == WallpointNetWork.WallPoints.Count - 1)
                {
                    CurrentIndex = 0;
                }

                //下一个路径点下标值得计算
                int nextIndex = (CurrentIndex + incStep >= WallpointNetWork.WallPoints.Count)
                    ? 0
                    : CurrentIndex + incStep;
                nextTargeTransform = WallpointNetWork.WallPoints[nextIndex];

                //不为空的话作为导航点，空的话遍历下个元素
                if (nextTargeTransform != null)
                {
                    _agent.destination = nextTargeTransform.position;
                    CurrentIndex = nextIndex;
                    return;
                }

                CurrentIndex++;
            }

        }

 
        
        public void EnterEvent(EventMgr.EVENT_TYPE Event_Type, Component Sender, object Param = null)
        {
            switch ((string) Param)
            {
                case "Normal":
                    OnNormalEnter();
                    Debug.Log("Enemynormal");
                    break;
                case "General":
                    OnGeneralEnter();
                    Debug.Log("EnemyGeneral");
                    break;
                default:
                    Debug.Log("EnterEvent Error");
                    break;
            }
        }

        void OnNormalEnter()
        {
            _isStop = true;
        }

        void OnGeneralEnter()
        {
            _isStop = false;
        }

    } //Class_End

} //NameSpace_End