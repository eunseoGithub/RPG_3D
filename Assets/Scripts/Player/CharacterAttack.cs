using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : IState<CharacterController>
{
    private CharacterController charController;

    public void OperateEnter(CharacterController sender)
    {
        charController = sender;
        charController.charAnimator.SetTrigger("Attack");
    }

    public void OperateExit(CharacterController sender)
    {
        
    }

    public void OperateUpdate(CharacterController sender)
    {
        if (charController != null)
        {
            
        }
    }
}
