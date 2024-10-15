using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonIdleState : IState<Dragon>
{
    protected Dragon _owner;
    public DragonIdleState(Dragon owner)
    {
        _owner = owner;
    }

    /// <summary>
    ///  상태가 변경되어 들어올때 한번 호출
    /// </summary>
    /// <param name="sender"></param>
    public void OperateEnter(Dragon sender) 
    { 
        // idle 애니메이션 플레이
    }    


    /// <summary>
    /// 상태가 다른 상태로 변경되어 나갈때 한번 호출
    /// </summary>
    /// <param name="Sender"></param>
    public void OperateExit(Dragon Sender) 
    { 
        // idle 애니메이션 종료
    }


    /// <summary>
    /// 해당 상태가 지속되는 동안 반복 호출
    /// </summary>
    /// <param name="sender"></param>
    public void OperateUpdate(Dragon sender) 
    {
        // idle 상태인 동안 매 프레임마다 타겟을 체크한다.(.0.02초)
        
		if (_owner.DetectingTarget()) // 타겟이 감지 반경에 들어왔는지 체크
		{
            if(!_owner.CheckReturnBase())
            {
                _owner._currentState = _owner.RandomSkiilState();
                switch (_owner._currentState)
                {
                    case Dragon.SkillStates.Near:
                        _owner._currentDistance = _owner._attackNearRange;
                        if (_owner.CheckNearAttackRange() == Dragon.BossState.Attack)
                        {
                            _owner.ChangeAttackState();
                        }
                        else
                        {
                            _owner.ChangeChaseState();
                        }
                        break;

                    case Dragon.SkillStates.Far:
                        _owner._currentDistance = _owner._attackFarRange;
                        
                        Dragon.BossState state = _owner.CheckFarAttackRange();
                        if (state == Dragon.BossState.Attack)
                        {
                            Debug.Log("Far Attack");
                            _owner.ChangeAttackState();
                        }
                        else if(state == Dragon.BossState.Chase)
                        {
                            Debug.Log("Chase");
                            _owner.ChangeChaseState();
                        }
                        else if(state == Dragon.BossState.Retreat)
                        {
                            Debug.Log("Retreat");
                            _owner.ChangeRetreatState();
                        }
                        else
                        {
                            Debug.LogError("Far Attack Check Boss state is none. (" + state + ")");
                        }
                        break;
                }
            }
            else
            {
                _owner.ChangeReturnBaseState();
            }
            

            /*if (_owner.CheckNearAttackRange() || _owner.CheckFarAttackRange())  // 원거리 근거리 공격 반경에 있는지 체크
			{
                _owner.ChangeAttackState(); // 공격 상태로 변경한다.
			} else if (_owner.CheckChaseRange())    // 추적 반경에 있는지 체크
			{
                _owner.ChangeChaseState();  // 추적 상태로 변경한다.
			}*/

        }
    }
}
