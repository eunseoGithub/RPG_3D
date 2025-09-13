using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * CharacterAnimationEvent
 * 플레이어 애니메이션 이벤트와 스킬 발동 관리
 * 기능 요약 : 
 * - 각 스킬 발사 처리
 * - R 스킬 시전 시 무적 적용
 * - 스킬별 발사 위치와 방향 계산
 * - Q,R 스킬 연속 발사 기능
 */
public class CharacterAnimationEvent : MonoBehaviour
{
    CharacterControl characterControl;
    public Transform firePoint;
    public GameObject Attack01Prefab;
    public GameObject Attack02Prefab;
    public GameObject Attack03Prefab;
    public GameObject Attack04Prefab;
    public GameObject Attack05Prefab;
    public GameObject potionHealPrefab;
    public GameObject Boss;
    Character character;
    SkillCooldownManager skillcooldownManager;
    public bool rSkillInvincibleOn = false;
    public void Start()
    {
        characterControl = GetComponent<CharacterControl>();
        character = Character.Instance;
        skillcooldownManager = GetComponent<SkillCooldownManager>();
    }
    public void StartAttack()
    {
        if(!characterControl.isAttacking)
            characterControl.isAttacking = true;
    }

    public void EndAttack()
    {
        characterControl.ChangeToIdleState();
    }

    public void Attack05_Start()
    {
        if (rSkillInvincibleOn)
            character.SetInvincible(0.5f);
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
        FireAtMousePosition_Attack05();
    }
    public void PotionHeal()
    {
        potionHealPrefab.SetActive(true);
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
        Vector3 direction = new Vector3(characterControl.currentTargetPosition.x, 1.0f, characterControl.currentTargetPosition.z);
        GameObject fire = Instantiate(Attack03Prefab, direction, Quaternion.identity);
        fire.GetComponentInChildren<SkillDamage>().damage = StatManger.Instance.WDamage;
        fire.GetComponentInChildren<SkillDamage>().skill = Skill.W;
        SFXManager.Instance.PlaySound(SFXManager.Instance.playerAttack_W);

    }
    void FireAtMousePosition_Attack04()
    {
        Vector3 direction = new Vector3(characterControl.currentTargetPosition.x, 1.0f, characterControl.currentTargetPosition.z);
        GameObject fire = Instantiate(Attack04Prefab, direction, Quaternion.identity);
        fire.GetComponentInChildren<SkillDamage>().damage = StatManger.Instance.EDamage;
        fire.GetComponentInChildren<SkillDamage>().skill = Skill.E;
        SFXManager.Instance.PlaySound(SFXManager.Instance.playerAttack_E);
    }
    void FireAtMousePosition_Attack05()
    {
        Vector3 direction = new Vector3(characterControl.currentTargetPosition.x, 1.0f, characterControl.currentTargetPosition.z);
        GameObject fire = Instantiate(Attack05Prefab, direction, Quaternion.identity);
        fire.GetComponentInChildren<SkillDamage>().damage = StatManger.Instance.RDamage;
        fire.GetComponentInChildren<SkillDamage>().skill = Skill.R;
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
        fire.GetComponentInChildren<SkillDamage>().damage = StatManger.Instance.RDamage;
        fire.GetComponentInChildren<SkillDamage>().skill = Skill.R;
        SFXManager.Instance.PlaySound(SFXManager.Instance.playerAttack_E);
    }
}
