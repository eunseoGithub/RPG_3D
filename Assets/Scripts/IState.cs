using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * IState
 * 제너릭 상태 인터페이스
 */
public interface IState<T>
{
    void OperateEnter(T sender);
    void OperateUpdate(T sender);
    void OperateExit(T Sender);
}
