using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * MonsterAttackState
 * 몬스터의 공격 상태(FSM) 관리
 * OperateEnter() : 공격 애니메이션 활성화 및 추적 애니메이션 비활성화
 * OperateExit() : 공격 애니메이션 비활성화
 */
public class MonsterAttackState : IState<Monster>
{
    protected Monster _owner;
    public MonsterAttackState(Monster owner)
    {
        _owner = owner;
    }
    public void OperateEnter(Monster sender)
    {
        if (_owner._animator.GetBool("Attack") == false)
            _owner._animator.SetBool("Attack", true);
        if (_owner._animator.GetBool("Chase") == true)
            _owner._animator.SetBool("Chase", false);
    }
    public void OperateUpdate(Monster sender)
    {

    }
    public void OperateExit(Monster Sender)
    {
        if (_owner._animator.GetBool("Attack") == true)
            _owner._animator.SetBool("Attack", false);
    }
}
