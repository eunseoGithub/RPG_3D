using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOffAttack : MonoBehaviour
{
    public GameObject warningPrefab;         // 경고 원 프리팹
    public GameObject explosionPrefab;       // 폭발 파티클 프리팹
    public GameObject coliderPrefab;         // 충돌 오브젝트 프리팹
    public Transform bossTransform;          // 보스의 위치
    public float warningDuration = 2f;       // 경고 원 표시 시간
    public float spawnRange = 15f;           // 폭발이 발생할 범위
    public float minDistanceFromBoss = 5f;   // 보스와 너무 가까운 위치는 제외
    public int explosionCount = 10;          // 총 폭발 횟수
    public float delayBetweenExplosions = 0.5f; // 각 폭발 간 시간 지연

    private List<GameObject> spawnedObjects = new List<GameObject>(); // 생성된 오브젝트 추적

    private void OnEnable()
    {
        StartCoroutine(SpawnExplosions());
    }

    private void OnDisable()
    {
        // 비활성화될 때 모든 생성된 오브젝트 삭제
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        spawnedObjects.Clear();
    }

    private IEnumerator SpawnExplosions()
    {
        for (int i = 0; i < explosionCount; i++)
        {
            Vector3 randomPosition = GetValidRandomPosition();
            Vector3 warningPosition = new Vector3(randomPosition.x, -0.73f, randomPosition.z);
            Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

            GameObject warningCircle = Instantiate(warningPrefab, warningPosition, randomRotation);
            spawnedObjects.Add(warningCircle);
            yield return new WaitForSeconds(warningDuration);

            if (warningCircle != null)
            {
                Destroy(warningCircle);
                spawnedObjects.Remove(warningCircle);
            }

            Vector3 leftPosition = warningCircle.GetComponent<WarningArea>().GetRightPosition();
            GameObject explosion = Instantiate(explosionPrefab, leftPosition, randomRotation);
            GameObject colliderObj = Instantiate(coliderPrefab, warningPosition, randomRotation);

            spawnedObjects.Add(explosion);
            spawnedObjects.Add(colliderObj);

            yield return new WaitForSeconds(delayBetweenExplosions);
        }
    }

    private Vector3 GetValidRandomPosition()
    {
        Vector3 randomPosition;
        float distance;

        do
        {
            float randomX = Random.Range(-spawnRange, spawnRange);
            float randomZ = Random.Range(-spawnRange, spawnRange);

            randomPosition = new Vector3(
                bossTransform.position.x + randomX,
                bossTransform.position.y + 0.5f,
                bossTransform.position.z + randomZ
            );

            distance = Vector3.Distance(bossTransform.position, randomPosition);
        }
        while (distance < minDistanceFromBoss);

        return randomPosition;
    }
}