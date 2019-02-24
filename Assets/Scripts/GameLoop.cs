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
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour
{
    
    //场景状态切换的控制器
    private readonly SceneStateController _controller = new SceneStateController();

    private bool _bStart = true;

    public Text PlayerWarnText;
    public Text EnemyWarnText;
    
    private void Awake()
    {
     
    }

    private void Start()
    {
        
        //设置初始场景
        GameManageHub.GetInstance().Awake();
        //Cursor.lockState = CursorLockMode.Locked; 
       
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

    public void CreatePlayer(GameObject theTarget)
    {
        PlayerWarnText.enabled = true;
        if (MainFactory.Instance.GetCharactorFactory().GetPlayerCount() != 0)
        {
            PlayerWarnText.text = "别闹！只能存在一个我方角色";
            PlayerWarnText.color = Color.red;
            return;
        }

        PlayerWarnText.text = "我方角色已产生";
        PlayerWarnText.color = Color.green;
        
        var factory = MainFactory.Instance.GetCharactorFactory();
        factory.CreatePlayer(theTarget.transform.position, 10, WeaponType.Shield, WeaponType.Sword);
        
    }

    public void CreateEnemy(GameObject theTarget)
    {
        EnemyWarnText.enabled = true;
        
        if (MainFactory.Instance.GetCharactorFactory().GetEnemyCount() != 0)
        {
            EnemyWarnText.text = "暂时只能产生一个敌人，多个敌人有BUG,待解决";
            EnemyWarnText.color = Color.red;
            return;
        }

        EnemyWarnText.text = "敌人已产生";
        EnemyWarnText.color = Color.green;
        
        var factory = MainFactory.Instance.GetCharactorFactory();
        factory.CreateEnemy(theTarget.transform.position, WeaponType.Shield, WeaponType.Sword);
    }

    public void ReLoad()
    {
        SceneManager.LoadScene("Battle");
        MainFactory.Instance.GetCharactorFactory().Release();
    }


}
