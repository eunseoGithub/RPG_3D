using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove :  IState<CharacterController>
{
    private CharacterController charController;

    public void OperateEnter(CharacterController sender)
    {
        charController = sender;
        charController.CurrentSpeed = charController.maxSpeed;
    }

    public void OperateUpdate(CharacterController sender)
    {
        if (charController != null)
        {
            if (charController.CurrentSpeed > 0)
            {
                charController.transform.Translate(Vector3.forward * (charController.CurrentSpeed * Time.deltaTime));
            }
        }
    }

    public void OperateExit(CharacterController sender)
    {

    }
}
