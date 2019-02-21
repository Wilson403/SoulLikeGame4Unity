using System.Collections;
using System.Collections.Generic;
using M_ControllerSystem;
using UnityEngine;

namespace M_CharactorSystem.M_Player
{   
    public abstract class IPlayer : ICharactor
    {
        public bool LeftIsShield = true; //是否左手持盾
        public bool BLockmoving = false;
        public bool BLock = false;

       
        public bool BArmedSword = false;


        protected float _currentValue; //动画权重的当前值
        protected Vector3 _jumpVec; //跳跃高度
        protected Vector3 _rollVec; //翻滚速度
        protected Vector3 _deltaPos; //根运动量
        protected Vector3 _stepVec;
        
		
        public IController Controller
        {
            get { return m_Controller; }
        }

        public override void SetCharactorModel(GameObject go)
        {
            base.SetCharactorModel(go);
            
            m_CameraPoint = m_CharactorHandle.transform.Find("CameraController").GetChild(0).gameObject;
			_animationeventMgr = m_Model.GetComponent<AnimationEventMgr>();
            _animationeventMgr.SetCharactor(this);
        }

        public override void Init()
        {
            //首帧就要检查是否位于地面
            CheckBOnGround();
            
            SetWeaponPoint();
            SetWeaponPos();
        }
        
        public override void Update()
        {
            ChangeController();
            Controller.Update(); //控制器内部更新逻辑
            BCanMove(); 
            BFollowObject();
            CheckBOnGround();
            
            //触发锁定，相机会锁定敌人
            if (Controller.BLock)
            {
                m_CameraControl.LockUnLock();
            }
        }

        public virtual void FixedUpdate()
        {
            
        }

        //被敌人攻击
        public override void UnderAttack(ICharactor theTarget)
        {
            var dir = Vector3.Normalize(theTarget.GetModel().transform.position - m_Model.transform.position);
            var value = UsefulView(theTarget); 

            //背面
            if (value >= 0 && value < 60)
            {
                //没有找到相应的动画，用正面被攻击的替代
                MyActionManager.General.GetDamageAnimation(0, 0); //触发被攻击动画
            }

            //侧面
            else if (value >= 60 && value < 120)
            {
                //右
                if (dir.x > 0)
                {
                    MyActionManager.General.GetDamageAnimation(-1, 0); //触发被攻击动画
                }
                //左
                else if (dir.x < 0)
                {
                    MyActionManager.General.GetDamageAnimation(1, 0); //触发被攻击动画
                }
            }
			
            //正面
            else if (value >= 120 && value <= 180)
            {
                MyActionManager.General.GetDamageAnimation(0, 0); //触发被攻击动画
            }
			
            //计算伤害值
            m_Attribute.GetRemainHp(theTarget);
            if (m_Attribute.GetNowHp() <= 0)
            {
                MyActionManager.General.GetDeadAnimation(); //触发死亡动画
            }
        }
        
        /// <summary>
        /// 切换控制器
        /// </summary>
        private void ChangeController()
        {
            var num = 0;
            num = CheckController.CheckCurController();

            if (num == 1 && m_ControllerState != ControllerState.KeyBoard)
            {
                m_Controller = new KeyBoardInput();
                m_ControllerState = ControllerState.KeyBoard;
            }
            else if (num == -1 && m_ControllerState != ControllerState.JoyStrick)
            {
                m_Controller = new JoyStrickInput();
                m_ControllerState = ControllerState.JoyStrick;
            }
            else
            {
                //nothing
            }
        }
        
        /// <summary>
        /// 让走路动画启用
        /// </summary>
        /// <returns></returns>
        private bool BCanMove()
        {
            if (Controller.DMag > 0f)
            {
                m_Animator.SetBool("Move", true);
                return true;
            }
            else
            {
                m_Animator.SetBool("Move", false);
                return false;
            }
        }
        
        /// <summary>
        /// 设置状态机的BFollowObject参数;相机是否处于聚焦跟踪状态
        /// </summary>
        private void BFollowObject()
        {
            m_Animator.SetBool("BFollowObject", m_CameraControl.LockState ? true : false);
        }
        
        //移动速度重置
        public void ReGetSpeed()
        {
            MovingVec = Vector3.zero;
        }
        
        public override int GetAtkValue()
        {
            return base.GetAtkValue() + GetWeaponAtkValue(1);
        }

        //使用动画自身的运动
        public void OnUpdateRm(Vector3 delta)
        {
            MyActionManager.General.RootMotionValue(delta, out _deltaPos);
        }

        public override void AddCamera(CameraControl theCamera)
        {
            m_CameraControl = theCamera;
        }

        
    }
} 
