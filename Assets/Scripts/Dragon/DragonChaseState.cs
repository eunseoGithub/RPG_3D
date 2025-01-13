using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonChaseState : IState<Dragon>
{
    protected Dragon _owner;
    public DragonChaseState(Dragon owner)
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
        _owner._animator.SetBool("chase", false);
        // 이동 애니메이션 종료
    }

    public void OperateUpdate(Dragon sender)
    {
        // chase Range(추적반경안에 있는지 체크한다.)
        // 반경은 chaseRange > FarAttack > NearAttack)
        if (_owner.CheckChaseRange())
        {
            if (_owner.CheckFarAttackRange())
            {
                // 원거리 반경에 들어왔을 경우
                // 난수를 발생하여 원거리 공격을 할 것인지 근거리 공격
                // 을 할 것인지를 결정한다.
                int randValue = UnityEngine.Random.Range(0, 1000);
                int attackType = randValue % 2;

                switch (attackType)
                {
                    case 0: // 근거리 공격 상태로 변경
                        _owner.ChangeNearAttackState();
                        break;

                    case 1:
                        _owner.ChangeFarAttackState();
                        break;
                }


            }
            else
            {
                _owner.MoveChase();
            }

        }

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
