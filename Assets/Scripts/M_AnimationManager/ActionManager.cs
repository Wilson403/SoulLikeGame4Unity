using System.Collections;
using System.Collections.Generic;
using M_AnimationManager;
using M_CharactorSystem;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class ActionManager
{
    public ICharactor Charactor { get; private set; }
    public IGeneral General { get; private set; }
    public IEqip Eqip { get; private set; }
    public IUnEqip UnEqip { get; private set; }

    public ActionManager(ICharactor charactor)
    {
        Charactor = charactor;
        
    }

    public void SetAnimation(IGeneral general, IEqip eqip, IUnEqip unEqip)
    {
        General = general;
        Eqip = eqip;
        UnEqip = unEqip;
    }
}
