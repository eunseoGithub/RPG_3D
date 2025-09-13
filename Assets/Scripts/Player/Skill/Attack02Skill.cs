using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Attack02Skill
 * 플레이어 관통형 발사체 스킬 관리
 * 기능 요약 : 
 * - Rigidbody를 사용하여 지정 속도로 발사체 이동
 * - 발사체는 5초 후 자동 삭제
 * - 적 충돌 시 hitCount 증가
 * - hitCount가 최대 관통횟수 이상이면 발사체 삭제
 * - Launch() : 발사체 발사 방향 설정 및 이동
 * - OnTriggerEnter() : 충동 감지 후 관통 처리 및 삭제
 */
public class Attack02Skill : MonoBehaviour
{
    public float speed = 10f; // 발사체 속도

    private Rigidbody rb;
    private int hitCount = 0;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    private void Start()
    {
        Destroy(this.gameObject, 5.0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            hitCount++;

            int maxPenetration = StatManger.Instance.qMaxPenetration;

            if (hitCount >= maxPenetration)
            {
                Destroy(this.gameObject);
            }
        }
    }
    public void Launch(Vector3 direction)
    {
        Vector3 directionNoY = new Vector3(direction.x, 0, direction.z).normalized;
        // 발사 방향으로 속도 설정
        rb.velocity = directionNoY * speed;
    }
}
