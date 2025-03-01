using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChaseState : IState<Monster>
{
    protected Monster _owner;
    public MonsterChaseState(Monster owner)
    {
        _owner = owner;
    }
    public void OperateEnter(Monster sender)
    {
        if (_owner._animator.GetBool("Chase") == false)
            _owner._animator.SetBool("Chase", true);
    }
    public void OperateUpdate(Monster sender)
    {
        _owner.MoveChase();
    }
    public void OperateExit(Monster Sender)
    {
        if (_owner._animator.GetBool("Chase") == true)
            _owner._animator.SetBool("Chase", false);
    }
}
