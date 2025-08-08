using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpawner : MonoBehaviour
{
    public GameObject fireballPrefab;
    public int fireballCount = 15;
    public float spreadAngle = 180f;
    public float fireballSpeed = 5f;

    public Vector3 topLeft = new Vector3(35.69f, 0.8f, 2.4f);
    public Vector3 topRight = new Vector3(54f, 0.8f, 44.9f);
    
    public int repeatCount = 5;
    public float interval = 5f;
    
    private void Start()
    {
        StartCoroutine(SpawnFireballsRepeat());
    }

    void SpawnFireballs(Vector3 origin, Vector3 baseDirection)
    {
        float halfSpread = spreadAngle / 2f;

        for(int i = 0;i<fireballCount;i++)
        {
            float angleStep = spreadAngle / (fireballCount - 1);
            float angleOffset = -halfSpread + (angleStep * i);

            Quaternion rotation = Quaternion.AngleAxis(angleOffset, Vector3.up);
            Vector3 direction = rotation * baseDirection.normalized;

            GameObject fireball = Instantiate(fireballPrefab, origin, Quaternion.LookRotation(direction));

            Rigidbody rb = fireball.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.velocity = direction * fireballSpeed;
            }
        }
    }

    IEnumerator SpawnFireballsRepeat()
    {
        Vector3 centerTarget = (topLeft + topRight) / 2f;

        for(int i = 0; i<repeatCount; i++)
        {
            SpawnFireballs(topLeft, centerTarget - topLeft);
            SpawnFireballs(topRight, centerTarget - topRight);

            yield return new WaitForSeconds(interval);
        }
    }
}
