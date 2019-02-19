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

namespace M_CharactorSystem.M_Enemy
{
    public class Enemy : IEnemy
    {

//    #region Public_Attribute
//
//        public AIWallPointNetWork WallpointNetWork; //路径点类
//        public int CurrentIndex; //当前初始路径点
//        public NavMeshPathStatus PathStatus; //当前路径状态
//        public float FindRadius; //寻敌范围
//
//    #endregion
//
//
//        //-----------------------------------------------------------
//
//
//    #region Private_Attribute
//
//        private NavMeshAgent _agent; //代理组件
//        private ICharactorAI _charactorAi; //角色AI
//        private List<ICharactor> _targets; //捕捉到的目标列表
//        private bool _isFirstRun = false; //是否首次执行
//        private bool _isStop; //是否停止移动
//        [SerializeField] private bool _hasPath; //是否存在路径
//        [SerializeField] private bool _pathPending; //是否在计算路径，该路径未完成
//
//    #endregion
//
//
//        //-----------------------------------------------------------
//
//
//    #region Unity_Message
//
//        private void Awake()
//        {
//            _agent = GetComponent<NavMeshAgent>();
//
//            //获取人物模型
//            MyModel = this.transform.Find("ybot").gameObject;
//
//            //获取角色控制柄下的刚体组件
//            Rig = this.GetComponent<Rigidbody>();
//
//            //获取角色控制柄下的碰撞体组件
//            Col = this.transform.GetComponent<CapsuleCollider>();
//
//            _targets = new List<ICharactor>();
//
//            //如果模型存在，则获取模型下的动画状态机
//            if (MyModel)
//            {
//                MyAnimator = MyModel.GetComponent<Animator>();
//            }
//
//            Sensor = this.transform.Find("Sensor").gameObject;
//
//            SetAiTarget(new EnemyAI());
//        }
//
//        private void Start()
//        {
//            if (!WallpointNetWork)
//            {
//                Debug.Log("无法获取到路径列表");
//                return;
//            }
//            
//            EventMgr.Instance.AddListener(EventMgr.EVENT_TYPE.EnemyFsmEnter, EnterEvent); //进入状态
//
//            SetNextTarget(false);
//            CheckBOnGround();
//            MyAnimator.SetTrigger("ToEqip");
//            MyAnimator.SetFloat("Z", 1);
//            CharactorAi.ChangeAiState(new IdleAIState());
//            CharactorAi.SetAiCharactor(theTarget: this);
//        }
//
//        private void Update()
//        {
//            CheckBOnGround();
//            FindEnemy();
//            AiStateUpdate(_targets);
//
//            _hasPath = _agent.hasPath;
//            _pathPending = _agent.pathPending;
//            PathStatus = _agent.pathStatus;
//
//
//
//            if (CharactorAi.CurAiState == AiState.Idle)
//            {
//                StartCoroutine(WaitNextTarget());
//                _agent.speed = 2;
//            }
//            else
//            {
//                _agent.speed = 4f;
//                if (_targets.Count != 0)
//                {
//                    _targets.Clear();
//                }
//            }
//            
//            if (CharactorAi.CurAiState == AiState.Idle || CharactorAi.CurAiState == AiState.Chase)
//            {
//                SetMoveState(isStop: _isStop);
//            }
//            else
//            {
//                SetMoveState(isStop: true);
//            }
//
//            //Debug.Log(CharactorAi.CurAiState);
//        }
//
//    #endregion
//
//
//        //-----------------------------------------------------------
//
//
//    #region Public_Methods
//
//        //停止移动
//        public override void StopMove()
//        {
//            SetMoveState(isStop: true);
//        }
//
//        //前往某目标位置
//        public override void MoveTo(Vector3 theTargetPosition)
//        {
//            _agent.isStopped = _isStop;
//            _agent.destination = theTargetPosition;
//        }
//
//        //攻击
//        public override void Attack(ICharactor theTarget)
//        {
//            MyAnimator.SetTrigger("LAttack");
//        }
//
//        //锁定目标
//        public override void LookTarget(ICharactor theTarget)
//        {
//            var direction = theTarget.transform.position - transform.position;
//            var lookRotation = Quaternion.LookRotation(direction);
//            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 0.1f);
//        }
//
//    #endregion
//
//
//        //-----------------------------------------------------------
//
//
//    #region Private_Methods
//
//        //设置移动状态（移动 or 停止）
//        private void SetMoveState(bool isStop)
//        {
//            SetMoveAnim(isStop ? 0 : CharactorAi.CurAiState == AiState.Chase ? 2 : 1);
//            _agent.isStopped = isStop;
//        }
//
//        //导航至下个固定目标点
//        //由于要实现到达每个固定点后停留一段时间，所以使用协程
//        private IEnumerator WaitNextTarget()
//        {
//            if (_agent.remainingDistance < 1f)
//            {
//                _isStop = true;
//                SetNextTarget(true); //自动遍历true
//                yield return new WaitForSeconds(2f); //停留2秒 
//                _isStop = false;
//            }
//            
//        #region 旧的巡航解决方案
//
//            //路径计算完成的情况下 || 无效路径 || 无法到达的路径
////            if ((!_hasPath && !_pathPending) || PathStatus == NavMeshPathStatus.PathInvalid ||
////                PathStatus == NavMeshPathStatus.PathPartial)
////            {
////                _isStop = true;  
////                SetNextTarget(true); //自动遍历true
////                yield return new WaitForSeconds(2f); //停留2秒 
////                _isStop = false;
////            }
//
//        #endregion
//        }
//
//        //设置走路动画
//        private float SetMoveAnim(int num)
//        {
//            MyAnimator.SetFloat("Z", Mathf.Lerp(MyAnimator.GetFloat("Z"), num, 0.1f));
//            return MyAnimator.GetFloat("Z");
//        }
//
//        //寻找周围的对象
//        private void FindEnemy()
//        {
//            Collider[] hitColliders;
//            ExplosionDamage(this.transform.position, FindRadius, out hitColliders);
//            if (hitColliders.Length > 0)
//            {
//                if (_targets.Count < hitColliders.Length)
//                {
//                    for (int i = 0; i < hitColliders.Length; i++)
//                    {
//                        //将检测到的角色加入目标列表
//                        //注意，空对象也会加入。使用前要检查是否存在空对象
//                        _targets.Add(hitColliders[i].GetComponent<ICharactor>());
//                    }
//                }
//            }
//        }
//
//
//        //球形碰撞器用于检测范围内的目标
//        private void ExplosionDamage(Vector3 center, float radius, out Collider[] hitColliders)
//        {
//            hitColliders = Physics.OverlapSphere(center, radius, 1 << 11);
//        }
//
//        /// <summary>
//        /// 固定路径点导航
//        /// </summary>
//        /// <param name="isIncrease">到达导航点后是否自动前往下个导航点</param>
//        private void SetNextTarget(bool isIncrease)
//        {
//            //增长步数
//            int incStep = isIncrease ? 1 : 0;
//            //下一个路径点
//            Transform nextTargeTransform = null;
//
//            while (nextTargeTransform == null)
//            {
//                //解决第一个路径点为空时会出现死循环的Bug
//                if (WallpointNetWork.WallPoints[0] == null && CurrentIndex == WallpointNetWork.WallPoints.Count - 1)
//                {
//                    CurrentIndex = 0;
//                }
//
//                //下一个路径点下标值得计算
//                int nextIndex = (CurrentIndex + incStep >= WallpointNetWork.WallPoints.Count)
//                    ? 0
//                    : CurrentIndex + incStep;
//                nextTargeTransform = WallpointNetWork.WallPoints[nextIndex];
//
//                //不为空的话作为导航点，空的话遍历下个元素
//                if (nextTargeTransform != null)
//                {
//                    _agent.destination = nextTargeTransform.position;
//                    CurrentIndex = nextIndex;
//                    return;
//                }
//
//                CurrentIndex++;
//            }
//
//        }
//
//    #endregion
//        
//        public void EnterEvent(EventMgr.EVENT_TYPE Event_Type, Component Sender, object Param = null)
//        {
//            switch ((string) Param)
//            {
//                case "Normal":
//                    OnNormalEnter();
//                    Debug.Log("Enemynormal");
//                    break;
//                case "General":
//                    OnGeneralEnter();
//                    Debug.Log("EnemyGeneral");
//                    break;
//                default:
//                    Debug.Log("EnterEvent Error");
//                    break;
//            }
//        }
//
//        void OnNormalEnter()
//        {
//            _isStop = true;
//        }
//
//        void OnGeneralEnter()
//        {
//            _isStop = false;
//        }
//
    } //Class_End

} //NameSpace_End