using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * MonsterChaseState
 * 몬스터의 추적 상태(FSM) 관리
 * OperateEnter() : 추적 애니메이션 활성화
 * OperateUpdate() : 애니메이션 상태 재확인 및 플레이어를 향해 이동
 * OperateExit() : 추적 애니메이션 비활성화
 */
public class MonsterChaseState : IState<Monster>
{
    protected Monster _owner;
    public MonsterChaseState(Monster owner)
    {
        _owner = owner;
    }
    public void OperateEnter(Monster sender)
    {
        if (_owner._animator.GetBool("Chase") == false)
            _owner._animator.SetBool("Chase", true);
    }
    public void OperateUpdate(Monster sender)
    {
        //동기화 문제로 간헐적으로 animation의 chase가 true가 되지 않아 idle 상태로 움직이는 상황이 발생하여,
        //OperateUpdate 함수에서 animaition값을 재 확인하고 값 변경
        if (_owner._animator.GetBool("Chase") == false)
            _owner._animator.SetBool("Chase", true);
        if (!_owner.GetDie()&& _owner._animator.GetBool("Chase")==true)
            _owner.MoveChase();
    }
    public void OperateExit(Monster Sender)
    {
        if (_owner._animator.GetBool("Chase") == true)
            _owner._animator.SetBool("Chase", false);
    }
}
