using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * MonsterReturnState
 * 몬스터가 생성 위치로 복귀하는 상태(FSM) 관리
 * OperateEnter() : 상태 진입 시 복귀 애니메이션 활성화
 * OperateUpdate() : 생성 위치까지 이동, 도착 시 returnCheck를 true 설정
 * OperateExit() : 상태 종료 시 복귀 애니메이션 비활성화
 */
public class MonsterReturnState : IState<Monster>
{
    protected Monster _owner;
    public MonsterReturnState(Monster owner)
    {
        _owner = owner;
    }
    public void OperateEnter(Monster sender)
    {
        if (_owner._animator.GetBool("Return") == false)
            _owner._animator.SetBool("Return", true);
    }
    public void OperateUpdate(Monster sender)
    {
        if(!_owner.GetDie())
        {
            _owner.MoveCreatePoint();
            float distance = Vector3.Distance(_owner.transform.position, _owner.createPoint);
            if (distance <= 0.3f)
            {
                _owner.returnCheck = true;
            }
        }
        

    }
    public void OperateExit(Monster Sender)
    {
        if (_owner._animator.GetBool("Return") == true)
            _owner._animator.SetBool("Return", false);
    }
}
