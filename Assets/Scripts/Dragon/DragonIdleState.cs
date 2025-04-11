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

    public void OperateEnter(Dragon sender)
    {

    }


    public void OperateExit(Dragon Sender)
    {

    }

    public void OperateUpdate(Dragon sender)
    {
        // idle 상태인 동안 매 프레임마다 타겟을 Detecting 체크한다.(.0.02초)        
        if (_owner.DetectingTarget()) // 타겟이 감지 반경에 들어왔는지 체크
        {
            // 반경에 들어왔으면 제일 먼저 Chase 반경에 들어온다.
            // chase상태가 우선이다.
            if (_owner.CheckChaseRange())
            {
                _owner.ChangeChaseState();
            }

        }
    }
}
