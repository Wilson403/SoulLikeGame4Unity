/*
 * IHuman ：继承于角色类ICharactor ，用于人型角色
 * 程序员 ：Wilson
 * 日期 ：2018/12/16
 * 挂载对象 ：None
 * 更多描述 ：由于可能存在多种角色，人型或者非人型，持武器或者没有武器，如果
 *           所有共同属性都在ICharactor中定义，该类就会变得臃肿，维护困难，
 *           所以还是要进一步划分。
 * 修改记录 ：None
 */

using System.Collections;
using System.Collections.Generic;
using M_CharactorSystem;
using UnityEngine;

namespace M_CharactorSystem
{

    public class IHuman : ICharactor
    {

    #region Protected_Variable

        protected GameObject Sensor; //传感器,用于检测地面
        protected IWeapon Weapon; //武器对象（桥接模式）

    #endregion


        //----------------------------------------------------------------------------------


    #region Private_Variable

        private float _raylength = 2f; //射线长度

    #endregion


        //----------------------------------------------------------------------------------


    #region Public_Methods

        //取得武器攻击力
        public int GetWeaponValue(int index)
        {
            return Weapon.GetAtkValue(index);
        }
        
        //取得武器防御力
        public int GetDefenseValue(int index)
        {
            return Weapon.GetDefenseValue(index);
        }

    #endregion


        //----------------------------------------------------------------------------------


    #region Protected_Methods

        //检查角色是否位于地面
        protected void CheckBOnGround()
        {
            if (Sensor != null)
            {
                //定义射线打向地面，来检查角色是否位于平面
                RaycastHit hit;
                if (Physics.Raycast(Sensor.transform.position, Sensor.transform.TransformDirection(Vector3.up) * -1,
                    out hit, _raylength,
                    1 << 10))
                {
                    OnGround();
                }
                else
                {
                    NotGround();
                }
            }
        }

    #endregion


        //----------------------------------------------------------------------------------


    #region Private_Methods

        //角色位于地面上
        private void OnGround()
        {
            MyAnimator.SetBool("BGround", true);
            Col.radius = 0.5f;
        }

        //角色不在地面上
        private void NotGround()
        {
            MyAnimator.SetBool("BGround", false);
            Col.radius = 0f;
        }

    #endregion

    } //Class_End
}

