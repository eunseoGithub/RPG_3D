using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : IState<CharacterControl>
{
    private CharacterControl charController;

    public void OperateEnter(CharacterControl sender)
    {
        charController = sender;
        charController.charAnimator.SetTrigger("Attack");
    }

    public void OperateExit(CharacterControl sender)
    {

    }

    public void OperateUpdate(CharacterControl sender)
    {
        if (charController != null)
        {

        }
    }
}
