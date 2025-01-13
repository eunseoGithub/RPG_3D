using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackSkill : MonoBehaviour
{
    public float speed = 10f;  // 이동 속도
    public float lifeTime = 5f; // Fireball의 수명(초)
    [SerializeField]
    float damage = 10;
    private Vector3 direction; // 이동 방향
    public void Initialize(Vector3 fireDirection)
    {
        direction = fireDirection.normalized;  // 방향 벡터를 정규화합니다.
        Destroy(gameObject, lifeTime);         // 일정 시간이 지나면 Fireball 파괴
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("player attack <Basic Attack>");
            other.GetComponent<Character>().GetDamage(damage);
        }
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
