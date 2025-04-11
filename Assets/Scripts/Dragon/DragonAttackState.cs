using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAttackState : IState<Dragon>
{
    protected Dragon _owner;
    public DragonAttackState(Dragon owner)
    {
        _owner = owner;
    }
    public void OperateEnter(Dragon sender)
    {
        if (_owner._currentState == Dragon.SkillStates.Near)
        {
            _owner._animator.SetBool("nearAttack", true);
        }
        else if (_owner._currentState == Dragon.SkillStates.Far)
        {
            _owner._animator.SetBool("farAttack", true);
        }
        else
        {
            Debug.LogError("Current skill state 정해지지 않음");
        }
        _owner._animator.SetBool("chase", false);
    }

    public void OperateUpdate(Dragon sender)
    {

    }

    public void OperateExit(Dragon Sender)
    {

    }

}
