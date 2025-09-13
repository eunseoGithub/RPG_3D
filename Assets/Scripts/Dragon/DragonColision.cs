using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * DragonColision
 * 보스 피격 처리 및 스킬 효과 적용
 * 기능 요약
 * - OnTriggerEnter() : 플레이어 공격 충돌 시 처리
 *      - 공격 중복 처리 방지(AttackIds 사용)
 *      - 데미지 적용
 *      - 스킬 효과 적용
 * - 피격된 공격 오브젝트 파괴
 */
public class DragonColision : MonoBehaviour
{
    GameObject DragonObj;
    Dragon dragon;
    MonsterDamageable damageable;
    // Start is called before the first frame update
    void Start()
    {
        DragonObj = transform.root.gameObject;
        dragon = DragonObj.GetComponent<Dragon>();
        damageable = DragonObj.GetComponent<MonsterDamageable>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerAttack"))
        {
            Debug.Log("trigger " + other.name);
            SkillDamage playerAttack = other.GetComponent<SkillDamage>();
            if(playerAttack != null)
            {
                if (dragon.AttackIds.Contains(playerAttack.attackId))
                    return;

                dragon.AttackIds.Add(playerAttack.attackId);

                dragon.GetDamage(playerAttack.damage);
                Skill skilltype;
                skilltype = playerAttack.skill;
                if (playerAttack.snare)
                {
                    switch (skilltype)
                    {
                        case Skill.Q:
                            int result = UnityEngine.Random.Range(0, 10);
                            if (result > 0)
                            {
                                damageable.ApplySnare(StatManger.Instance.qSnareDuration);
                            }
                            break;
                        case Skill.W:
                            damageable.ApplySnare(StatManger.Instance.wSnareDuration);
                            break;
                    }

                }
                if (playerAttack.dot)
                {
                    switch (skilltype)
                    {
                        case Skill.W:
                            damageable.ApplyDot(StatManger.Instance.wDotDamage, StatManger.Instance.wDotInterval,
                        StatManger.Instance.wDotDuration);
                            break;
                        case Skill.E:
                            damageable.ApplyDot(StatManger.Instance.eDotDamage, StatManger.Instance.eDotInterval,
                        StatManger.Instance.eDotDuration);
                            break;
                    }
                }

                if (playerAttack.slow)
                {
                    switch (skilltype)
                    {
                        case Skill.Q:
                            damageable.ApplySlow(StatManger.Instance.qSlowAmount, StatManger.Instance.qSlowDuration);
                            break;
                        case Skill.W:
                            damageable.ApplySlow(StatManger.Instance.wSlowAmount, StatManger.Instance.wSlowDuration);
                            break;
                    }
                }
            }
            Destroy(other.gameObject);
        }
        else
        {

        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
