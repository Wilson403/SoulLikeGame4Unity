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
using GameAttr.WeaponAttr;
using UnityEngine;
using M_Factory;
using M_Factory.AssetFactory;

public class GameLoop : MonoBehaviour {
    
    //场景状态切换的控制器
    private readonly SceneStateController _controller = new SceneStateController();

    private bool _bStart = true;
    //private readonly GameManageHub _hub = new GameManageHub();
    
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        
        //设置初始场景
        GameManageHub.GetInstance().Awake();
        Cursor.lockState = CursorLockMode.Locked; 
        CreatePlayer();
        CreateEnemy();
       
      //  _controller.SetState("", new LogoMenuState(_controller));
    }

    private void Update()
    {
        //更新
       // _controller.StateUpdate();
       if (_bStart)
       {
           GameManageHub.GetInstance().Start();
           _bStart = false;
       }

       GameManageHub.GetInstance().Update();
    }

    private void FixedUpdate()
    {
        GameManageHub.GetInstance().FixedUpdate();
    }

    private void CreatePlayer()
    {
        var factory = MainFactory.GetCharactorFactory();
        factory.CreatePlayer(new Vector3(10,0,10), 10, WeaponType.Shield, WeaponType.Sword);
        
    }

    private void CreateEnemy()
    {
        var factory = MainFactory.GetCharactorFactory();
        factory.CreateEnemy(new Vector3(50, 0, 50), WeaponType.Shield, WeaponType.Sword);
    }


}
