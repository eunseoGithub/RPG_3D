using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * BasicAttackSkill
 * 보스의 기본 공격 스킬 동작 처리
 * Initialize() : 발사 방향 설정 및 수명 타이머 시작
 * Update() : 설정된 방향으로 이동
 * OnTriggerEnter() : 땅에 닿으면 오브젝트 파괴
 */
public class BasicAttackSkill : MonoBehaviour
{
    public float speed = 10f;  // 이동 속도
    public float lifeTime = 5f; // Fireball의 수명(초)
    private Vector3 direction; // 이동 방향
    public void Initialize(Vector3 fireDirection)
    {
        direction = fireDirection.normalized;  // 방향 벡터를 정규화합니다.
        Destroy(gameObject, lifeTime);         // 일정 시간이 지나면 Fireball 파괴
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Land"))
        {
            Destroy(gameObject); // 땅에 닿으면 파이어볼 제거
        }
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
