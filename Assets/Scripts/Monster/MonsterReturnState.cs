using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterReturnState : IState<Monster>
{
    protected Monster _owner;
    public MonsterReturnState(Monster owner)
    {
        _owner = owner;
    }
    public void OperateEnter(Monster sender)
    {
        if (_owner._animator.GetBool("Return") == false)
            _owner._animator.SetBool("Return", true);
    }
    public void OperateUpdate(Monster sender)
    {
        _owner.MoveCreatePoint();
        float distance = Vector3.Distance(_owner.transform.position, _owner.createPoint);
        if (distance <= 0.3f)
        {
            _owner.returnCheck = true;
        }

    }
    public void OperateExit(Monster Sender)
    {
        if (_owner._animator.GetBool("Return") == true)
            _owner._animator.SetBool("Return", false);
    }
}
