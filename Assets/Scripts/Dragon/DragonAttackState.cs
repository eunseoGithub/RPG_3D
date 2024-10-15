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
        if (_owner._currentState == Dragon.SkillStates.Near)  // 근거리 공격 반경안에 있으면
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
        //else if (_owner.CheckChaseRange() == Dragon.BossState.Chase)    // 추적중에 아직 추적 반경에 있으면 
        //{
        //    _owner.ChangeChaseState(); // 추적상태로 변경
        //}
        //else
        //{
        //    if (_owner.CheckReturnBase()/* == Dragon.BossState.ReturnBase*/) // 귀환해야 하는지 체크
        //    {
        //        _owner.ChangeReturnBaseState(); // 귀환 상태로 변경
        //    }
        //    else
        //    {
        //        _owner.ChangeIdleState(); // 귀환하지 않으면 idle 상태
        //    }
        //}
    }

    public void OperateExit(Dragon Sender) 
    {
        //_owner._animator.SetBool("nearAttack", false);
        //_owner._animator.SetBool("farAttack", false);
    }

}
