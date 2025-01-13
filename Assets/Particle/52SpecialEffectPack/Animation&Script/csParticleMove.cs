using UnityEngine;
using System.Collections;

public class csParticleMove : MonoBehaviour
{
    public float speed = 10f; // 발사체 속도

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    public void Launch(Vector3 direction)
    {
        Vector3 directionNoY = new Vector3(direction.x, 0, direction.z).normalized;
        // 발사 방향으로 속도 설정
        rb.velocity = directionNoY * speed;
    }
    /* public float speed = 0.1f;

     void Update () 
     {
         transform.Translate(Vector3.forward * speed * Time.deltaTime);
     }*/
}
