using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * CharacterTrigger
 * 플레이어 트리거 충돌 관리
 * OnTriggerEnter() : 
 * - 캐릭터가 무적이거나 기믹 중일 때 피해 무시
 * - 'EnemyAttack' 태그가 추돌 시
 *      - 데미지를 받아와 플레이어 Hp 감소
 *      - DamageVignette를 통해 피격 화면 효과 적용
 * - 'item' 태그 충돌 시 :
 *      - 아이템 삭제
 *      캐릭터 포션 개수 증가 및 UI 업데이트
 *      인벤토리에 포션 추가
 */
public class CharacterTrigger : MonoBehaviour
{
    Character character;
    DamageVignette damageVignette;
    public DragonGimickController dragonGimick;
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponentInParent<Character>();
        damageVignette = GetComponent<DamageVignette>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("player trigger"+other.name);
        if (character.isInvincible||dragonGimick.isGimick)
            return;
        if (other.CompareTag("EnemyAttack"))
        {
            float damage = other.GetComponent<SkillDamage>().damage;
            character.GetDamage(damage);
            damageVignette.TakeDamage();
        }
        if(other.CompareTag("item"))
        {
            Destroy(other.gameObject);
            character.posionCount++;
            character.posionCountText.text = character.posionCount.ToString();
            character.AddItem(ItemType.Posion);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
