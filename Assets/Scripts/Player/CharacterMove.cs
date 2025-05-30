using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : IState<CharacterControl>
{
    private CharacterControl charController;

    public void OperateEnter(CharacterControl sender)
    {
        charController = sender;
        charController.charAnimator.SetBool("Walk", true);
    }

    public void OperateUpdate(CharacterControl sender)
    {

    }

    public void OperateExit(CharacterControl sender)
    {
        if (charController != null)
        {
            charController.charAnimator.SetBool("Walk", false);
        }
    }
}
