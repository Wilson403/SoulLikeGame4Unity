using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IActionInterface
{
    public ActionManager Am;

    public IActionInterface(ActionManager am)
    {
        Am = am;
    }
    
    protected void ReGetSpeed()
    {
        Am.MPlay.MovingVec = Vector3.zero;
    }
    
}
