﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAnimationEvent : MonoBehaviour
{
    //보스 애니메이션 실행시 호출되는 함수
    public Dragon _owner;
    private int _flameAttackCount;
    private int _basicAttackCount;
    private int _screamCount;
    private int _sleepCount;
    private int _takeoffCount;
    public GameObject _flameAttack_FirePoint;
    public GameObject _basicAttack_FirePoint;
    public GameObject _basicAttack_firballlPrefab;
    public GameObject _screamAttack_Obj;
    public GameObject _defendAttack_Obj;
    public GameObject _sleepAttack_Obj;
    public GameObject _defendAttackCol;
    public GameObject _clawAttackCol;
    public GameObject _takeoffAttack_Obj;
    // Start is called before the first frame update
    void Start()
    {
        _flameAttackCount = 2;
        _basicAttackCount = 3;
        _screamCount = 3;
        _sleepCount = 2;
        _takeoffCount = 3;
    }
    public void BasicAttack_Fireballl_Attack()
    {
        GameObject fireball = Instantiate(_basicAttack_firballlPrefab, _basicAttack_FirePoint.transform.position, Quaternion.identity);
        Vector3 fireDirection = transform.forward;
        fireball.GetComponent<BasicAttackSkill>().Initialize(fireDirection);
        SFXManager.Instance.PlaySound(SFXManager.Instance.BossSkill2);
    }
    void FlameAttackStart()
    {
        if (_owner._animator.GetBool("nearAttack") == true)
        {
            if (_flameAttackCount >= 2)
            {
                if (_flameAttack_FirePoint.activeSelf == false)
                    _flameAttack_FirePoint.SetActive(true);
                SFXManager.Instance.PlaySound(SFXManager.Instance.BossSkill4);
            }
            _flameAttackCount--;
        }

    }
    void FlameAttackEnd()
    {
        if (_flameAttackCount <= 0)
        {
            _owner.ChangeIdleState();
            _owner._animator.SetBool("nearAttack", false);
            if (_flameAttack_FirePoint.activeSelf == true)
                _flameAttack_FirePoint.SetActive(false);
            _flameAttackCount = 2;
        }

    }
    void ClawAttackEnd()
    {
        _owner.ChangeIdleState();
        _owner._animator.SetBool("nearAttack", false);
        if (_clawAttackCol.activeSelf == true)
        {
            _clawAttackCol.SetActive(false);
        }
    }
    void DefendEnd()
    {
        _owner.ChangeIdleState();
        _owner._animator.SetBool("nearAttack", false);
        if (_defendAttack_Obj.activeSelf == true)
            _defendAttack_Obj.SetActive(false);
        if (_defendAttackCol.activeSelf == true)
            _defendAttackCol.SetActive(false);
    }
    void BasicAttackStart()
    {
        if (_owner._animator.GetBool("farAttack") == true)
        {
            if (_basicAttackCount >= 3)
            {
                if (_basicAttack_FirePoint.activeSelf == false)
                    _basicAttack_FirePoint.SetActive(true);
                BasicAttack_Fireballl_Attack();
            }
            _basicAttackCount--;

        }
    }
    void BasicAttackEnd()
    {
        if (_basicAttackCount <= 0)
        {
            _owner.ChangeIdleState();
            _owner._animator.SetBool("farAttack", false);
            _basicAttackCount = 3;
            if (_basicAttack_FirePoint.activeSelf == true)
                _basicAttack_FirePoint.SetActive(false);
        }
    }
    void ScreamStart()
    {
        if (_owner._animator.GetBool("farAttack") == true)
        {
            if (_screamAttack_Obj.activeSelf == false)
                _screamAttack_Obj.SetActive(true);
            _screamCount--;
            SFXManager.Instance.PlaySound(SFXManager.Instance.BossSkill1);
        }
    }
    void ScreamEnd()
    {
        if (_screamCount <= 0)
        {
            _owner.ChangeIdleState();
            _owner._animator.SetBool("farAttack", false);
            _screamCount = 3;
            if (_screamAttack_Obj.activeSelf == true)
                _screamAttack_Obj.SetActive(false);
        }
    }
    void SleepStart()
    {
        if (_owner._animator.GetBool("farAttack") == true)
        {
            if (_sleepAttack_Obj.activeSelf == false)
                _sleepAttack_Obj.SetActive(true);
            _sleepCount--;
        }
    }
    void SleepEnd()
    {
        if (_sleepCount <= 0)
        {
            _owner.ChangeIdleState();
            _owner._animator.SetBool("farAttack", false);
            if (_sleepAttack_Obj.activeSelf == true)
                _sleepAttack_Obj.SetActive(false);
            _sleepCount = 2;
        }
    }
    void LandEnd()
    {
        _owner.ChangeIdleState();
        _owner._animator.SetBool("farAttack", false);
    }

    void DefendExplosion()
    {
        if (_defendAttack_Obj.activeSelf == false)
        {
            _defendAttack_Obj.SetActive(true);
        }
    }

    void DefendAttack()
    {
        if (_defendAttackCol.activeSelf == false)
        {
            _defendAttackCol.SetActive(true);
        }
    }

    void ClawAttack()
    {
        if (_clawAttackCol.activeSelf == false)
        {
            _clawAttackCol.SetActive(true);
        }
    }
    void TakeOffStart()
    {
        if (_owner._animator.GetBool("farAttack") == true)
        {
            _owner._animator.SetBool("Land", false);
            if (_takeoffAttack_Obj.activeSelf == false)
                _takeoffAttack_Obj.SetActive(true);
            _takeoffCount--;
            SFXManager.Instance.PlaySound(SFXManager.Instance.BossSkill3);
        }
    }
    void TakeOffEnd()
    {
        if (_takeoffCount <= 0)
        {
            _owner.ChangeIdleState();
            _owner._animator.SetBool("farAttack", false);
            _owner._animator.SetBool("Land", true);
            if (_takeoffAttack_Obj.activeSelf == true)
                _takeoffAttack_Obj.SetActive(false);
            _takeoffCount = 3;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
