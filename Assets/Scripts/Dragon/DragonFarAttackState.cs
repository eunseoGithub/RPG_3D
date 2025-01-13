using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFarAttackState : IState<Dragon>
{
    protected Dragon _owner;
    public DragonFarAttackState(Dragon owner)
    {
        _owner = owner;
    }
    public void OperateEnter(Dragon sender)
    {
        //_owner._animator.SetBool("nearAttack", true);
    }

    public void OperateUpdate(Dragon sender)
    {
        if (_owner.CheckFarAttackRange())
        {
            // 공격
            if(!(_owner._animator.GetBool("farAttack")))
            {
                _owner._animator.SetInteger("farAttackSkill", Random.Range(0, 4));
                _owner._animator.SetBool("farAttack", true);
            }
        }
        else  // 원거리 공격 상태인데 아직 원거리 공격 반경에 있지 않으면 
        {
            // 원거리 공격상태인데 원거리 공격 반경안에 없는경우
            // chase 반경을 벗어나면 ReturnBase 처리.
            if (!(_owner._animator.GetBool("farAttack")))
            {
                if (_owner.CheckChaseRange())
                {
                    _owner.ChangeChaseState();
                }
            }
               

        }

    }

    public void OperateExit(Dragon Sender)
    {
        //_owner._animator.SetBool("nearAttack", false);        
    }
}
