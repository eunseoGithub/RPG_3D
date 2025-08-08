using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateStartBehaviour : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponentInParent<CharacterControl>().isAttacking = true;
    }
}
