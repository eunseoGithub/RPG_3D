using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour, IState<CharacterController>
{
    private CharacterController charController;

    public void OperateEnter(CharacterController sender)
    {

    }

    public void OperateUpdate(CharacterController sender)
    {
        if (charController != null)
        {
            charController.gameObject.transform.Translate(Vector3.up * Time.deltaTime);
            //charController.transform.Translate(Vector3.up * (/*charController.CurrentSpeed*/1 * Time.deltaTime));
        }
    }

    public void OperateExit(CharacterController sender)
    {

    }
}
