using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * CharacterAttack
 * 플레이어 공격 상태(FSM) 관리
 * OperateUpdate() : 공격 상태 진입 시 공격 애니메이션 트리거 발동
 */
public class CharacterAttack : IState<CharacterControl>
{
    private CharacterControl charController;

    public void OperateEnter(CharacterControl sender)
    {
        charController = sender;
        charController.charAnimator.SetTrigger("Attack");
    }

    public void OperateExit(CharacterControl sender)
    {

    }

    public void OperateUpdate(CharacterControl sender)
    {

    }
}
