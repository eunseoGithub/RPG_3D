using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIdle : IState<CharacterController>
{
    private CharacterController charController;

    public void OperateEnter(CharacterController sender)
    {
        charController = sender;
        if (charController != null)
        {
            //_bikeController.CurrentSpeed = 0;
        }
    }

    public void OperateExit(CharacterController sender)
    {
    }

    public void OperateUpdate(CharacterController sender)
    {

    }
}
