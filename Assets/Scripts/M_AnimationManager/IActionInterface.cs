using System.Collections;
using System.Collections.Generic;
using M_CharactorSystem;
using UnityEngine;

namespace M_AnimationManager
{
    public abstract class IActionInterface
    {
        protected ActionManager Am;
        protected readonly Animator CurAnimator;
        protected readonly ICharactor Charactor;
        protected readonly CameraControl M_CameraControl;
        
        protected IActionInterface(ActionManager am)
        {
            Am = am;
            CurAnimator = am.Charactor.GetAnimator();
            Charactor = am.Charactor;
            M_CameraControl = am.M_CameraControl;
        }


        /// <summary>
        /// 调整动画浮点数，可以使用插值平滑。如果不使用插值，time设置为0即可
        /// </summary>
        /// <param name="isLerp">是否进行插值</param>
        /// <param name="animVar">动画参数</param>
        /// <param name="target">目标值</param>
        /// <param name="time">平滑时间</param>
        protected void AnimSetfloat(bool isLerp, string animVar, float target, float time)
        {
            CurAnimator.SetFloat(animVar, isLerp
                ? Mathf.Lerp(CurAnimator.GetFloat(animVar),
                    target, time)
                : target);
        }

        /// <summary>
        /// 状态机层次权重的调整
        /// </summary>
        /// <param name="layerName"></param>
        /// <param name="targetvalue"></param>
        /// <param name="time"></param>
        protected void SetAnimLayerWeight(string layerName, float targetvalue, float time)
        {
            CurAnimator.SetLayerWeight(CurAnimator.GetLayerIndex(layerName),
                Mathf.Lerp(CurAnimator.GetLayerWeight(CurAnimator.GetLayerIndex(layerName)),
                    targetvalue, time));
        }

        /// <summary>
        /// 检查当前是否处于所指定的动画状态，以状态名为检索依据
        /// </summary>
        /// <param name="stateName">状态名</param>
        /// <param name="layerName">动画层</param>
        /// <returns></returns>
        protected bool CheckState(string stateName, string layerName = "Base Layer")
        {
            var layerindex = CurAnimator.GetLayerIndex(layerName);
            return CurAnimator.GetCurrentAnimatorStateInfo(layerindex).IsName(stateName);
        }
	
        /// <summary>
        /// 检查当前是否处于所指定的动画状态，以标签名（tag）为检索依据
        /// </summary>
        /// <param name="tagName">动画状态tag</param>
        /// <param name="layerName">动画层</param>
        /// <returns></returns>
        protected bool CheckStateTag(string tagName, string layerName = "Base Layer")
        {
            var layerindex = CurAnimator.GetLayerIndex(layerName);
            return CurAnimator.GetCurrentAnimatorStateInfo(layerindex).IsTag(tagName);
        }

    }
}

