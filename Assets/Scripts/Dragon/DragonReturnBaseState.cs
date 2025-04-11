using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonReturnBaseState : IState<Dragon>
{
    protected Dragon _owner;
    public DragonReturnBaseState(Dragon owner)
    {
        _owner = owner;
    }

    public void OperateEnter(Dragon sender)
    {
        _owner._animator.SetBool("chase", true);
    }


    public void OperateExit(Dragon Sender)
    {
        _owner._animator.SetBool("chase", false);
    }

    public void OperateUpdate(Dragon sender)
    {

    }

}
