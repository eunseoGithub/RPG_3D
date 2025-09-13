using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 
 * MonsterIdleState
 * 몬스터 대기 상태(FSM) 상태 관리
 * OperateEnter() : 상태 진입 시 추적 애니메이션 비활성화
 */
public class MonsterIdleState : IState<Monster>
{
    protected Monster _owner;
    public MonsterIdleState(Monster owner)
    {
        _owner = owner;
    }
    public void OperateEnter(Monster sender)
    {
        if (_owner._animator.GetBool("Chase") == true)
            _owner._animator.SetBool("Chase", false);
    }
    public void OperateUpdate(Monster sender)
    {

    }
    public void OperateExit(Monster Sender)
    {

    }
}
