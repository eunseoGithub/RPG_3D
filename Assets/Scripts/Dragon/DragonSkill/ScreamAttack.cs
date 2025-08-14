using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamAttack : MonoBehaviour
{
    public GameObject warningCirclePrefab;  // 경고 원 프리팹
    public GameObject explosionPrefab;      // 폭발 파티클 프리팹
    public Transform playerTransform;         // 플레이어 위치
    public float warningDuration = 2f;      // 경고 원 표시 시간
    public float detectInterval = 0.1f;           // 플레이어 위치 감지 간격
    private List<GameObject> spawnObjects = new List<GameObject>();
    public LayerMask groundLayer;
    private void OnEnable()
    {
        StartCoroutine(AttackPattern());
    }

    private void OnDisable()
    {
        foreach (GameObject obj in spawnObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        spawnObjects.Clear();
    }

    private IEnumerator AttackPattern()
    {
        yield return StartCoroutine(SpawnExplosionsOnPlayer(3, detectInterval));

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(SpawnExplosionsOnPlayer(3, detectInterval));
    }

    private IEnumerator SpawnExplosionsOnPlayer(int count, float interval)
    {
        for(int i = 0; i<count; i++)
        {
            Vector3 targetPos = playerTransform.position;
            targetPos.y = GetGroundY(targetPos);

            GameObject warningCircle = Instantiate(warningCirclePrefab, targetPos, Quaternion.Euler(90f, 0f, 0f));
            spawnObjects.Add(warningCircle);

            yield return new WaitForSeconds(warningDuration);

            if(warningCircle != null)
            {
                Destroy(warningCircle);
                spawnObjects.Remove(warningCircle);
            }

            GameObject explosion = Instantiate(explosionPrefab, targetPos, Quaternion.identity);
            spawnObjects.Add(explosion);

            yield return new WaitForSeconds(interval);
        }
    }

    private float GetGroundY(Vector3 position)
    {
        Ray ray = new Ray(position + Vector3.up * 50f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
        {
            return hit.point.y;
        }
        return position.y;
    }
}
