using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMoveState : IState<Dragon>
{
    protected Dragon _owner;
    public DragonMoveState(Dragon owner)
	{
        _owner = owner;
	}

    public void OperateEnter(Dragon sender) { }

    public void OperateUpdate(Dragon sender) { }

    public void OperateExit(Dragon Sender) { }
}
