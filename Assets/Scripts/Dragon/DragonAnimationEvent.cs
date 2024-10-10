using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAnimationEvent : MonoBehaviour
{
    public Dragon _owner;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void FlameAttackEnd()
    {
        _owner.ChangeIdleState();
        _owner._animator.SetBool("nearAttack", false);
    }
    void ClawAttackEnd()
    {
        _owner.ChangeIdleState();
        _owner._animator.SetBool("nearAttack", false);
    }
    void DefendEnd()
    {
        _owner.ChangeIdleState();
        _owner._animator.SetBool("nearAttack", false);
    }

    void BasicAttackEnd()
    {
        _owner.ChangeIdleState();
        _owner._animator.SetBool("farAttack", false);
    }
    void ScreamEnd()
    {
        _owner.ChangeIdleState();
        _owner._animator.SetBool("farAttack", false);
    }
    void SleepEnd()
    {
        _owner.ChangeIdleState();
        _owner._animator.SetBool("farAttack", false);
    }
    void LandEnd()
    {
        _owner.ChangeIdleState();
        _owner._animator.SetBool("farAttack", false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
