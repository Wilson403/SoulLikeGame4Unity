/*
 * ICharactor_New ：
 * 程序员 ：Wilson
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：None
 */
 
using System.Collections;
using System.Collections.Generic;
using AIMgr.CharactorAIMgr;
using GameAttr.CharactorAttr;
using UnityEngine;
using UnityEngine.UI;

namespace M_CharactorSystem
{
    public class ICharactor_New : MonoBehaviour
    {
        protected GameObject Model;
        protected string AssetName;
        protected ICharactorAttr CharactorAttr = null;
        protected ICharactorAI CharactorAI = null;
        
        protected CapsuleCollider Col; 
        protected Rigidbody Rig;
        protected Animator MyAnimator;

        //设置模型
        public void SetModel(GameObject theGameObject)
        {
            Model = theGameObject;
            Col = Model.GetComponent<CapsuleCollider>();
            Rig = Model.GetComponent<Rigidbody>();
            MyAnimator = Model.GetComponent<Animator>();
        }

        //取得模型
        public GameObject GetModel()
        {
            return Model;
        }

        //销毁或隐藏模型
        public void Release(bool IsDestroy)
        {
            if (IsDestroy)
            {
                GameObject.Destroy(Model);
            }
            else
            {
                Model.SetActive(false);
            }
        }
        
        //设置AI
        public void SetAITarget(ICharactorAI theAITarget)
        {
            CharactorAI = theAITarget;
        }

        //更新AI
        public void AIUpdate(List<ICharactor> targets)
        {
            CharactorAI.Update(targets);
        }

        //属性设置
        public virtual void SetCharactorAttr(ICharactorAttr theCharactorAttr)
        {
            CharactorAttr = theCharactorAttr;
        }

        public string GetAssetName()
        {
            return AssetName;
        }


    } 
} 
