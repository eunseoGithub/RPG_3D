using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamAttack : MonoBehaviour
{
    public GameObject warningCirclePrefab;  // 경고 원 프리팹
    public GameObject explosionPrefab;      // 폭발 파티클 프리팹
    public Transform bossTransform;         // 보스의 위치
    public float warningDuration = 2f;      // 경고 원 표시 시간
    public float spawnRange = 15f;          // 폭발이 발생할 범위
    public float minDistanceFromBoss = 5f;  // 보스와 너무 가까운 위치는 제외
    public int explosionCount = 10;         // 총 폭발 횟수
    public float delayBetweenExplosions = 0.5f; // 각 폭발 간 시간 지연
    private List<GameObject> spawnObjects = new List<GameObject>();
    private void OnEnable()
    {
        StartCoroutine(SpawnExplosions());
    }

    private void OnDisable()
    {
        foreach(GameObject obj in spawnObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        spawnObjects.Clear();
    }
    private IEnumerator SpawnExplosions()
    {
        for (int i = 0; i < explosionCount; i++)
        {
            Vector3 randomPosition = GetValidRandomPosition();

            GameObject warningCircle = Instantiate(warningCirclePrefab, randomPosition, Quaternion.Euler(90f, 0f, 0f));
            spawnObjects.Add(warningCircle);

            yield return new WaitForSeconds(warningDuration);
            
            if(warningCircle != null)
            {
                Destroy(warningCircle);
                spawnObjects.Remove(warningCircle);
            }

            GameObject explosion = Instantiate(explosionPrefab, randomPosition, Quaternion.identity);
            spawnObjects.Add(explosion);

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
                bossTransform.position.y+0.5f, // Y축은 보스와 동일한 높이로
                bossTransform.position.z + randomZ
            );

            distance = Vector3.Distance(bossTransform.position, randomPosition);
        }
        while (distance < minDistanceFromBoss); 

        return randomPosition;
    }
}
