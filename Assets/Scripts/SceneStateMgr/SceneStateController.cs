/*
 * 这是一个场景状态的控制器，它保存了当前的场景状态。提供了切换场景状态的方法
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStateController
{
    //场景状态
    private ISceneState _mstate = null;
    
    //是否已经运行过一次
    private bool _mRunBegin = false;
  
    private AsyncOperation _async;
    

    //------------------------------------------------------
    
    
    //场景设置
    public void SetState(string statename,ISceneState state)
    {
        _mRunBegin = false;
        LoadScene(statename);

        //如果m_state不为空，说明存在上一个场景
        if (_mstate != null)
        {
            //释放上一个场景的资源
            _mstate.StateEnd();
        }

        _mstate = state;
    }
    
    
    //------------------------------------------------------

    
    //加载场景
    private void LoadScene(string statename)
    {
        if (string.IsNullOrEmpty(statename))
        {
            return;
        }

        _async = SceneManager.LoadSceneAsync(statename);
    }
    
    
    //------------------------------------------------------
    

    //状态的更新
    public void StateUpdate()
    {
        //如果场景未加载完毕则返回
        if (_async != null && !_async.isDone)
        {
            return;
        }

        //首次运行
        if (_mstate != null && _mRunBegin == false)
        {
            _mRunBegin = true;
            _mstate.StateStart();
        }

        //不断更新
        if (_mstate != null)
        {
            _mstate.StateUpdate();
        }
    }
    
}//Class_End
