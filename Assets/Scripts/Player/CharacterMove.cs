using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : IState<CharacterController>
{
    private CharacterController charController;

    public void OperateEnter(CharacterController sender)
    {
        charController = sender;
        charController.charAnimator.SetBool("Walk", true);
    }

    public void OperateUpdate(CharacterController sender)
    {

    }

    public void OperateExit(CharacterController sender)
    {
        if (charController != null)
        {
            charController.charAnimator.SetBool("Walk", false);
        }
    }
}
