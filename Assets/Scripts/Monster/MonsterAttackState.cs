using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackState : IState<Monster>
{
    protected Monster _owner;
    public MonsterAttackState(Monster owner)
    {
        _owner = owner;
    }
    public void OperateEnter(Monster sender)
    {
        if (_owner._animator.GetBool("Attack") == false)
            _owner._animator.SetBool("Attack", true);
        if (_owner._animator.GetBool("Chase") == true)
            _owner._animator.SetBool("Chase", false);
    }
    public void OperateUpdate(Monster sender)
    {

    }
    public void OperateExit(Monster Sender)
    {
        if (_owner._animator.GetBool("Attack") == true)
            _owner._animator.SetBool("Attack", false);
    }
}
