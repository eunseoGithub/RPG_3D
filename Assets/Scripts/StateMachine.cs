using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T>
{
    private T sender;

    //현재 상태를 담는 프로퍼티
    public IState<T> curState { get; set; }

    //기본 상태를 생성시에 설정하게 생성자 선언
    public StateMachine(T sender, IState<T> state)
    {
        this.sender = sender;
        SetState(state);
    }

    public void SetState(IState<T> state)
    {
        if (this.sender == null)
        {
            return;
        }

        if (curState == state)
        {
            return;
        }

        if (curState != null)
        {
            curState.OperateExit(this.sender);
        }

        curState = state;

        if (curState != null)
        {
            curState.OperateEnter(this.sender);
        }

    }

    public void DoOperateUpdate()
    {
        if (this.sender == null)
        {
            Debug.LogError("invalid this.sender");
            return;
        }
        curState.OperateUpdate(this.sender);
    }
}
