using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdleState : IState<Monster>
{
    protected Monster _owner;
    public MonsterIdleState(Monster owner)
    {
        _owner = owner;
    }
    public void OperateEnter(Monster sender)
    {
        if (_owner._animator.GetBool("Chase") == true)
            _owner._animator.SetBool("Chase", false);
    }
    public void OperateUpdate(Monster sender)
    {

    }
    public void OperateExit(Monster Sender)
    {

    }
}
