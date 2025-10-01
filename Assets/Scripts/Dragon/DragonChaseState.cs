using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * DragonChaseState
 * 보스 추적 상태(FSM) 처리
 * OperateEnter() : 추적 상태 진입 시 chase 애니메이션 활성화
 * OperateUpdate() : 
 *      - 추적 범위 내인지 확인
 *      - 우너거리 공격 범위에 들어오면 근거리/원거리 공격 상태 랜덤 전환
 *      - 범위 밖이면 MoveChase()로 이동
 * OperateExit() : 추적 상태 종료 시 chase 애니메이션 비활성화
 */
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
    }

    public void OperateExit(Dragon Sender)
    {
        _owner._animator.SetBool("chase", false);
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

    }

}
