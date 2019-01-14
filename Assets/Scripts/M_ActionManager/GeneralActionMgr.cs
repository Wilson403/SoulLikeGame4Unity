using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralActionMgr : IActionInterface
{
    public GeneralActionMgr(ActionManager am) : base(am)
    {

    }

    //无武装下的移动行为
    public void UnEqipMove()
    {
        //不处于聚焦状态下
        if (Am.MPlay.MCameraControl.LockState == false)
        {
            //移动的时候调节动画混合树
            if (Am.MPlay.Controller.DMag > 0)
            {
                Am.MPlay.MyAnimator.SetFloat("Z",
                    Mathf.Lerp(Am.MPlay.MyAnimator.GetFloat("Z"),
                        (Am.MPlay.Controller.DMag <= 0.8f ? 1f : 2f), 0.1f));

                Am.MPlay.MyAnimator.SetFloat("X", 0);
            }
            else//平滑动画，去掉else块动画切换没有过渡
            {
                //向0值平滑
                Am.MPlay.MyAnimator.SetFloat("Z", Mathf.Lerp(Am.MPlay.MyAnimator.GetFloat("Z"),
                    0, 0.1f));
            }

            //移动速度的计算
            if (!Am.MPlay._isLockmoving)
            {
                if (Am.MPlay.Controller.DMag > 0)
                {
// if (Am.CheckState("UneqipMove") || Am.CheckState("EqipMove")) //过渡会看起来自然点，不过会影响性能
// {
                    Am.MPlay.MovingVec =
                        (Am.MPlay.Controller.DMag <= 0.8f ? Am.MPlay.WalkSpeed : Am.MPlay.RunSpeed) *
                        Am.MPlay.MyModel.transform.forward;
// }
                }
                else
                {
                    ReGetSpeed();
                }
            }

            //只有摇杆值不为0时才进行角色方向变更
            if (Am.MPlay.Controller.DMag > 0f)
            {
                //利用线性插值使角色旋转变得平滑
                Am.MPlay.MyModel.transform.forward =
                    Vector3.Slerp(Am.MPlay.MyModel.transform.forward, Am.MPlay.Controller.GetRVec(Am.MPlay.transform),
                        0.3f);
            }

        } //UnEqipMove_End

        else //相机锁住物体时，即聚焦模式，移动动作变更
        {
            //将旋转量从世界空间转变到局部空间
            Vector3 localDvec =
                Am.MPlay.transform.InverseTransformVector(Am.MPlay.Controller.GetRVec(Am.MPlay.transform));
            
            Am.MPlay.MyAnimator.SetFloat("Z", localDvec.z);
            Am.MPlay.MyAnimator.SetFloat("X", localDvec.x);

            Am.MPlay.MyModel.transform.forward = Am.MPlay.transform.forward;
            if (!Am.MPlay._isLockmoving)
                Am.MPlay.MovingVec = Am.MPlay.Controller.GetRVec(Am.MPlay.transform) * Am.MPlay.WalkSpeed;
        }
    }

    //跳跃及闪避行为
    public void JumpAndDodge()
    {
        //触发跳跃动作
        if (Am.MPlay.MyAnimator.GetBool("Move"))
        {
            if (Am.MPlay.Controller.BJump)
            {
                if (!Am.MPlay.MCameraControl.LockState)
                    Am.MPlay.MyAnimator.SetTrigger("Jump");
                else
                {
                    if (Am.CheckState("EqipMove"))
                    {
                        Am.MPlay.MyAnimator.SetTrigger("Dodge_Roll");
                    }
                }
            }

            else
            if(Am.MPlay.Controller.BStep)
            {
                if (Am.MPlay.MCameraControl.LockState)
                {
                    if (Am.CheckState("EqipMove"))
                        Am.MPlay.MyAnimator.SetTrigger("Dodge_Step");
                }
            }
        }
        
    }//JumpAndDodge_End

    //武装与非武装之间的切换
    public void ActionStateChange()
    {
        if (Am.MPlay.Controller.GetSelect1() && !Am.MPlay.BArmedSword)
        {
            if (Am.CheckState("UneqipMove"))
            {
                Am.MPlay.MyAnimator.SetTrigger("ToEqip");
                Am.MPlay.BArmedSword = true;
            }
        }

        if (Am.MPlay.Controller.GetSelect1() && Am.MPlay.BArmedSword)
        {
            if (Am.CheckState("EqipMove"))
            {
                Am.MPlay.MyAnimator.SetTrigger("ToUnEqip");
                Am.MPlay.BArmedSword = false;
            }
        }
    }
}


