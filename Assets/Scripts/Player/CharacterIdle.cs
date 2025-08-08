using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIdle : IState<CharacterControl>
{
    private CharacterControl charController;

    public void OperateEnter(CharacterControl sender)
    {
        charController = sender;
        if (charController.isAttacking)
            charController.isAttacking = false;
    }

    public void OperateExit(CharacterControl sender)
    {
        charController.charAnimator.SetBool("Walk", false);
    }

    public void OperateUpdate(CharacterControl sender)
    {

    }
}
