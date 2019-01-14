/*
 * GameLoop ：负责部分系统的逻辑更新
 * 程序员 ：Wilson
 * 日期 ：2018/11/27
 * 挂载对象 ：(Scene)Logo/(Empty GameObject)GameLoop
 * 更多描述 ：当场景切换时不销毁目标物件
 * 修改记录 ：None
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour {
    
    //场景状态切换的控制器
    private SceneStateController _controller = new SceneStateController();
    
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        //设置初始场景
        _controller.SetState("", new LogoMenuState(_controller));
    }

    private void Update()
    {
        //更新
        _controller.StateUpdate();
    }
}
