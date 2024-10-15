using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonReturnBaseState : IState<Dragon>
{
    protected Dragon _owner;
    public DragonReturnBaseState(Dragon owner)
    {
        _owner = owner;
    }

    public void OperateEnter(Dragon sender) 
    {
        _owner._animator.SetBool("chase", true);
        // 이동 애니메이션
    }


    public void OperateExit(Dragon Sender) 
    {
        _owner._animator.SetBool("chase", false);
        // 이동 애니메이션 종료
    }

    public void OperateUpdate(Dragon sender)
    {
        //if (_owner.CheckFarAttackRange() || _owner.CheckNearAttackRange())   // 추적중에 공격 반경으로 변경되면
        //{
        //    _owner.ChangeAttackState();
        //}
        //else if (_owner.CheckChaseRange() == Dragon.BossState.Chase)    // 귀환중에 타겟이 추적반경안에 있으면
        //{
        //    _owner.ChangeChaseState();      // 추적 상태로 변경
        //}
        //else
        //{
        //    if (_owner.CheckReturnBase()) // 귀환해야 하는지 체크
        //    {
        //        _owner.MoveReturnBase(); // 귀환이동.
        //    }
        //    else
        //    {
        //        _owner.ChangeIdleState(); // 귀환하지 않으면 idle 상태
        //    }
        //}
    }

}
