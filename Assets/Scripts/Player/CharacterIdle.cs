using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIdle : IState<CharacterController>
{
    private CharacterController charController;

    public void OperateEnter(CharacterController sender)
    {
        charController = sender;
    }

    public void OperateExit(CharacterController sender)
    {
        charController.charAnimator.SetBool("Walk", false);
    }

    public void OperateUpdate(CharacterController sender)
    {

    }
}
