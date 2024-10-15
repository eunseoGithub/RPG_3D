using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonRetreatState : IState<Dragon>
{
    protected Dragon _owner;
    public DragonRetreatState(Dragon owner)
    {
        _owner = owner;
    }
    public void OperateEnter(Dragon sender)
    {
        _owner._animator.SetBool("chase", true);
        // 이동 애니메이션 플레이
    }

    public void OperateExit(Dragon Sender)
    {
        //_owner._animator.SetBool("chase", false);
        // 이동 애니메이션 종료
    }

    public void OperateUpdate(Dragon sender)
    {
        _owner.MoveRetreat();
        /*if(_owner.CheckFarAttackRange() || _owner.CheckNearAttackRange())   // 추적중에 공격 반경으로 변경되면
		{
            _owner.ChangeAttackState();
		} else if (_owner.CheckChaseRange())    // 추적중에 아직 추적 반경에 있으면 계속 추적           
		{
            _owner.MoveChase();
		}
		else
		{
			if (_owner.CheckReturnBase()) // 귀환해야 하는지 체크
			{
                _owner.ChangeReturnBaseState(); // 귀환 상태로 변경
			}else
			{
                _owner.ChangeIdleState(); // 귀환하지 않으면 idle 상태
			}
		}*/

    }
}
