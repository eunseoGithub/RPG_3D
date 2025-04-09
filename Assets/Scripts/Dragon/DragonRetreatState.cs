using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonRetreatState : IState<Dragon>
{
    protected Dragon _owner;
    public DragonRetreatState(Dragon owner)
    {
        _owner = owner;
    }
    public void OperateEnter(Dragon sender)
    {
        _owner._animator.SetBool("chase", true);
    }

    public void OperateExit(Dragon Sender)
    {
    }

    public void OperateUpdate(Dragon sender)
    {
        _owner.MoveRetreat();
    }
}
