using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * CharacterMove
 * 플레이어 Move 상태(FSM) 관리
 * OperateEnter() : 걷기 애니메이션 활성화
 * OperateExit() : 걷기 애니메이션 비활성화
 */
public class CharacterMove : IState<CharacterControl>
{
    private CharacterControl charController;

    public void OperateEnter(CharacterControl sender)
    {
        charController = sender;
        charController.charAnimator.SetBool("Walk", true);
    }

    public void OperateUpdate(CharacterControl sender)
    {

    }

    public void OperateExit(CharacterControl sender)
    {
        if (charController != null)
        {
            charController.charAnimator.SetBool("Walk", false);
        }
    }
}
