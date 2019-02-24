using System.Collections;
using System.Collections.Generic;
using AIMgr;
using AIMgr.CharactorAIMgr;
using M_Factory;
using M_Factory.AttriableFactory;
using UnityEngine;
using UnityEngine.AI;

namespace M_CharactorSystem.M_Enemy
{
    public class IEnemy : ICharactor
    {
        protected AIWallPointNetWork WallpointNetWork; //路径点类
        protected int CurrentIndex; //当前初始路径点
        protected NavMeshPathStatus PathStatus; //当前路径状态
        protected float FindRadius = 10; //寻敌范围
        protected float StayTime = 2f;
        protected float Timer = 0.0f;
        private EnemyAnimEvent _enemyAnimEvent;

        protected List<ICharactor> Targets; //捕捉到的目标列表

       
       
        protected bool IsAttach = false;

        protected bool _isFirstRun = false; //是否首次执行
        protected bool _isStop; //是否停止移动
        protected bool _hasPath; //是否存在路径
        protected bool _pathPending; //是否在计算路径，该路径未完成

        public override void Init()
        {
            CheckBOnGround();
            SetWeaponPoint();
            SetWeaponPos();
            
            WallpointNetWork = GameObject.FindWithTag("WallPoints").GetComponent<AIWallPointNetWork>();
            if (!WallpointNetWork) Debug.Log("路径网格获取失败");
        }

        

        public override void Update()
        {
            if (isStartRun)
            {
                Init();
                isStartRun = false;
            }
        }
        
        public override int GetAtkValue()
        {
           return base.GetAtkValue() + GetWeaponAtkValue(1);
   
        }

        protected override void SetWeaponPoint()
        {
            m_Lweapontrans = UnityTool.DeepFind(m_Model.transform, "weaponHandleL").transform;
            m_Rweapontrans = UnityTool.DeepFind(m_Model.transform, "weaponHandleR").transform;
        }
        
        protected override void SetWeaponPos()
        {
            var lweapon = GetLWeaponModel();
            UnityTool.SetParent(m_Lweapontrans, lweapon);

            var rweapon = GetRWeaponModel();
            UnityTool.SetParent(m_Rweapontrans, rweapon);
        }
        
        public override void SetCharactorModel(GameObject go)
        {
            base.SetCharactorModel(go);
            _agent = m_CharactorHandle.GetComponent<NavMeshAgent>();
            
            _enemyAnimEvent = m_Model.GetComponent<EnemyAnimEvent>();
            _enemyAnimEvent.SetCharactor(this);
        }
        
        public override void UnderAttack(ICharactor theTarget)
        {
           
            m_Animator.SetTrigger("UnderAttack");
            //计算伤害值
            m_Attribute.GetRemainHp(theTarget);
            if (m_Attribute.GetNowHp() <= 0)
            {
                m_Animator.SetBool("BDead", true);
                BKilled = true;
                
            }
        }
    }
} 
