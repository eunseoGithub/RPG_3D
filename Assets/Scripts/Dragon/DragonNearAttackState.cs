using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonNearAttackState : IState<Dragon>
{
    protected Dragon _owner;
    public DragonNearAttackState(Dragon owner)
    {
        _owner = owner;
    }
    public void OperateEnter(Dragon sender)
    {
        //_owner._animator.SetBool("nearAttack", true);
    }

    public void OperateUpdate(Dragon sender)
    {
		if (_owner.CheckNearAttackRange())
		{
            // 공격
            if(!(_owner._animator.GetBool("nearAttack")))
            {
                _owner._animator.SetInteger("nearAttackSkill", Random.Range(0, 3));
                _owner._animator.SetBool("nearAttack", true);
            }
		}
        else  // 근거리 공격 상태인데 아직 근거리 공격 반경에 있지 않으면 
        {
            // 근거리 공격상태인데 근거리 공격 반경안에 없는경우
            // chase 반경을 벗어나면 ReturnBase 처리.
            if (!(_owner._animator.GetBool("nearAttack")))
            {
                if (_owner.CheckChaseRange())
                {
                    _owner.MoveChase();
                }
                else
                {
                    _owner.ChangeReturnBaseState();
                }
            }
                
            
        }

    }

    public void OperateExit(Dragon Sender)
    {
        //_owner._animator.SetBool("nearAttack", false);        
    }

}
