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
        if(_owner._currentState==Dragon.SkillStates.Near)
        {
            _owner._animator.SetBool("nearAttack", true);
        }
        else if(_owner._currentState == Dragon.SkillStates.Far)
        {
            _owner._animator.SetBool("farAttack", true);
        }
        else
        {
            Debug.LogError("Current skill state 정해지지 않음");
        }
        _owner._animator.SetBool("chase", false);
    }
   
    public  void OperateUpdate(Dragon sender) 
    {
        /*if (_owner._currentState == Dragon.SkillStates.Near)  // 근거리 공격 반경안에 있으면
        {
            // 근거리 공격애니메이션 플레이
        }
        else if (_owner._currentState == Dragon.SkillStates.Far)   // 원거리 반경안에 있으면
        {
            // 원거리 공격 애니메이션 플레이
        }
        else
        {
            _owner.ChangeIdleState(); // 귀환하지 않으면 idle 상태
        }
        */
    }

    public void OperateExit(Dragon Sender) 
    {

    }

}
