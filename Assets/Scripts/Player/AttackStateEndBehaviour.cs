using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateEndBehaviour : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponentInParent<CharacterControl>().ChangeToIdleState();
    }
}
