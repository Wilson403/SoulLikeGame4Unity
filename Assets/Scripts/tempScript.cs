/*
 * TempScript ：
 * 程序员 ：Wilson
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：None
 */
 
using System.Collections;
using System.Collections.Generic;
using M_Factory;
using M_Factory.AssetFactory;
using UnityEngine;

namespace MyNamespace
{
    public class TempScript : MonoBehaviour 
    {

        private void Start()
        {
            MainFactory.GetCharactorFactory().CreatePlayer("Cube", 4);
        }
    } 
} 
