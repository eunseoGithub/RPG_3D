using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationEvent : MonoBehaviour
{
    CharacterControl characterControl;
    public Transform firePoint;
    public GameObject Attack01Prefab;
    public GameObject Attack02Prefab;
    public GameObject Attack03Prefab;
    public GameObject Attack04Prefab;
    public GameObject Attack05Prefab;
    public GameObject Boss;
    Character character;
    public bool rSkillInvincibleOn = false;
    public void Start()
    {
        characterControl = GetComponent<CharacterControl>();
        character = Character.Instance;
    }
    public void StartAttack()
    {
        characterControl.isAttacking = true;
    }

    public void EndAttack()
    {
        characterControl.ChangeToIdleState();
    }

    public void Attack01_Start()
    {
        StartAttack();
    }
    public void Attack01_End()
    {
        EndAttack();
    }

    public void Attack02_Start()
    {
        StartAttack();
    }
    public void Attack02_End()
    {
        EndAttack();
    }

    public void Attack03_Start()
    {
        StartAttack();
    }
    public void Attack03_End()
    {
        EndAttack();
    }

    public void Attack04_Start()
    {
        StartAttack();
    }
    public void Attack04_End()
    {
        EndAttack();
    }

    public void Attack05_Start()
    {
        StartAttack();
        if (rSkillInvincibleOn)
            character.SetInvincible(0.5f);
    }
    public void Attack05_End()
    {
        EndAttack();
    }

    public void Attack01_Fire()
    {
        FireAtMousePosition_Attack01();
    }
    public void Attack02_Fire()
    {
        FireAtMousePosition_Attack02();
    }
    public void Attack03_Fire()
    {
        FireAtMousePosition_Attack03();
    }
    public void Attack04_Fire()
    {
        FireAtMousePosition_Attack04();
    }
    public void Attack05_Fire()
    {
        Attack05Prefab.SetActive(true);
        SFXManager.Instance.PlaySound(SFXManager.Instance.playerAttack_R);
    }
    void FireAtMousePosition_Attack01()
    {
        Vector3 direction = (characterControl.currentTargetPosition - firePoint.position).normalized;

        GameObject fireball = Instantiate(Attack01Prefab, firePoint.position, Quaternion.identity);
        fireball.GetComponent<SkillDamage>().damage = StatManger.Instance.AADamage;
        fireball.GetComponent<SkillDamage>().skill = Skill.AA;
        fireball.GetComponent<Attack01Skill>().Launch(direction);
        SFXManager.Instance.PlaySound(SFXManager.Instance.playerAttack_leftclick);
    }
    void FireAtMousePosition_Attack02()
    {
        Vector3 direction = (characterControl.currentTargetPosition - firePoint.position).normalized;

        GameObject fireball = Instantiate(Attack02Prefab, firePoint.position, Quaternion.LookRotation(-direction));
        fireball.GetComponent<SkillDamage>().damage = StatManger.Instance.QDamage;
        fireball.GetComponent<SkillDamage>().skill = Skill.Q;
        fireball.GetComponent<Attack02Skill>().Launch(direction);
        SFXManager.Instance.PlaySound(SFXManager.Instance.playerAttack_Q);
        if( StatManger.Instance.qDoubleFireEnabled)
        {
            StartCoroutine(DelayFireAttack02(direction, 0.1f));
        }
    }

    IEnumerator DelayFireAttack02(Vector3 direction, float delay)
    {
        yield return new WaitForSeconds(delay);

        GameObject fireball = Instantiate(Attack02Prefab, firePoint.position, Quaternion.LookRotation(-direction));
        fireball.GetComponent<SkillDamage>().damage = StatManger.Instance.QDamage;
        fireball.GetComponent<SkillDamage>().skill = Skill.Q;
        fireball.GetComponent<Attack02Skill>().Launch(direction);
        SFXManager.Instance.PlaySound(SFXManager.Instance.playerAttack_Q);
    }

    void FireAtMousePosition_Attack03()
    {
        if(StatManger.Instance.eStackEnabled)
        {
            if (StatManger.Instance.eCurrentStack <= 0)
                return;
            StatManger.Instance.eCurrentStack--;
            var lastUsed = characterControl.GetLastSkillUseTimes();
            if (lastUsed.ContainsKey(KeyCode.E))
                lastUsed[KeyCode.E] = Time.time;
        }
        Vector3 direction = new Vector3(characterControl.currentTargetPosition.x, 1.0f, characterControl.currentTargetPosition.z);
        GameObject fire = Instantiate(Attack03Prefab, direction, Quaternion.identity);
        fire.GetComponent<SkillDamage>().damage = StatManger.Instance.WDamage;
        fire.GetComponent<SkillDamage>().skill = Skill.W;
        SFXManager.Instance.PlaySound(SFXManager.Instance.playerAttack_W);

    }
    void FireAtMousePosition_Attack04()
    {
        Vector3 direction = new Vector3(characterControl.currentTargetPosition.x, 1.0f, characterControl.currentTargetPosition.z);
        GameObject fire = Instantiate(Attack04Prefab, direction, Quaternion.identity);
        fire.GetComponent<SkillDamage>().damage = StatManger.Instance.EDamage;
        fire.GetComponent<SkillDamage>().skill = Skill.E;
        //float distanceToBoss = Vector3.Distance(fire.transform.position, Boss.transform.position);
        //if (distanceToBoss < 5.0f) // 거리 1.5 이하이면 히트 판정
        //{
        //    Dragon bossComponent = Boss.GetComponent<Dragon>();
        //    if (bossComponent != null)
        //    {
        //        bossComponent.GetDamage(10); // 보스에게 10의 데미지
        //    }
        //}
        SFXManager.Instance.PlaySound(SFXManager.Instance.playerAttack_E);
    }
    void FireAtMousePosition_Attack05()
    {
        Vector3 direction = new Vector3(characterControl.currentTargetPosition.x, 1.0f, characterControl.currentTargetPosition.z);
        GameObject fire = Instantiate(Attack05Prefab, direction, Quaternion.identity);
        fire.GetComponent<SkillDamage>().damage = StatManger.Instance.EDamage;
        fire.GetComponent<SkillDamage>().skill = Skill.R;
        SFXManager.Instance.PlaySound(SFXManager.Instance.playerAttack_E);

        if (StatManger.Instance.rDoubleFireEnabled)
        {
            StartCoroutine(DelayFireAttack05(direction, 0.7f));
        }
    }
    IEnumerator DelayFireAttack05(Vector3 direction, float delay)
    {
        yield return new WaitForSeconds(delay);

        GameObject fire = Instantiate(Attack05Prefab, direction, Quaternion.identity);
        fire.GetComponent<SkillDamage>().damage = StatManger.Instance.EDamage;
        fire.GetComponent<SkillDamage>().skill = Skill.R;
        SFXManager.Instance.PlaySound(SFXManager.Instance.playerAttack_E);
    }
}
