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
        Debug.Log("SetState : " + state);

        if(this.sender == null)
        {
            Debug.LogError("this.sender ERROR");
            return;
        }

        if(curState == state)
        {
            Debug.LogWarningFormat("Same state : ", state);
            return;
        }

        if(curState != null)
        {
            Debug.LogWarningFormat("Same state : ", state);
            return;
        }

        if(curState != null)
        {
            curState.OperateExit(this.sender);
        }

        curState = state;

        if(curState != null)
        {
            curState.OperateEnter(this.sender);
        }

        Debug.Log("SetNextState : " + state);

    }

    public void DoOperateUpdate()
    {
        if(this.sender == null)
        {
            Debug.LogError("invalid this.sender");
            return;
        }
        curState.OperateUpdate(this.sender);
    }
}
