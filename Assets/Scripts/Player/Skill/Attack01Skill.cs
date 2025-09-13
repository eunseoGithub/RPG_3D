using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Attack01Skill
 * 플레이어의 기본 발사체 스킬 관리
 * 기능 요약 : 
 * - Rigidbody를 사용하여 지정 속도로 발사체 이동
 * - 발사체는 5초 후 자동 삭제
 * - 적 충돌 시 발사체 삭제
 * - Launch() : 발사체 발사 방향 설정 및 이동
 * - OnTriggerEnter() : 충돌 감지 후 발사체 삭제
 */
public class Attack01Skill : MonoBehaviour
{
    public float speed = 10f; // 발사체 속도

    private Rigidbody rb;
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
            Destroy(this.gameObject);
        }
    }
    public void Launch(Vector3 direction)
    {
        Vector3 directionNoY = new Vector3(direction.x, 0, direction.z).normalized;
        // 발사 방향으로 속도 설정
        rb.velocity = directionNoY * speed;
    }

}
