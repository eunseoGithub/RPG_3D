using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * DragonReturnBaseState
 * 보스가 자신의 초기 위치로 돌아가는 상태(FSM) 관리
 * OperateEnter() : chase 애니메이션 활성화
 * OperateExit() : chase 애니메이션 비활성화
 * 현재는 단순히 애니메이션 처리만 수행(실제 이동은 Dragon 클래스에서 처리 가능)
 */
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
    }


    public void OperateExit(Dragon Sender)
    {
        _owner._animator.SetBool("chase", false);
    }

    public void OperateUpdate(Dragon sender)
    {

    }

}
