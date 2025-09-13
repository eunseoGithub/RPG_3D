using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * CharacterIdle
 * 플레이어 Idle 상태(FSM) 관리
 * OperateEnter() : 공격 중 상태면 isAttack false
 * OperateExit() : 걷기 애니메이션 해제
 */
public class CharacterIdle : IState<CharacterControl>
{
    private CharacterControl charController;

    public void OperateEnter(CharacterControl sender)
    {
        charController = sender;
        if (charController.isAttacking)
            charController.isAttacking = false;
    }

    public void OperateExit(CharacterControl sender)
    {
        charController.charAnimator.SetBool("Walk", false);
    }

    public void OperateUpdate(CharacterControl sender)
    {

    }
}
