using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
