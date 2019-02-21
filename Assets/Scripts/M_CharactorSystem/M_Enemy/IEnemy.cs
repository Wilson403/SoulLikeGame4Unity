using System.Collections;
using System.Collections.Generic;
using AIMgr;
using AIMgr.CharactorAIMgr;
using M_Factory;
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

        protected List<ICharactor> Targets; //捕捉到的目标列表

       
       
        protected bool IsAttach = false;

        protected bool _isFirstRun = false; //是否首次执行
        protected bool _isStop; //是否停止移动
        protected bool _hasPath; //是否存在路径
        protected bool _pathPending; //是否在计算路径，该路径未完成

        public override void Init()
        {
            CheckBOnGround();
            WallpointNetWork = GameObject.FindWithTag("WallPoints").GetComponent<AIWallPointNetWork>();
            if (!WallpointNetWork) Debug.Log("路径网格获取失败");
        }

        

        public override void Update()
        {

        }

        public override void SetCharactorModel(GameObject go)
        {
            base.SetCharactorModel(go);
            _agent = m_CharactorHandle.GetComponent<NavMeshAgent>();
        }
    }
} 
