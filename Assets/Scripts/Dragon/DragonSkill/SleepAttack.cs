using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepAttack : MonoBehaviour
{
    public GameObject warningCirclePrefab;  // 경고 원 프리팹
    public GameObject explosionPrefab;      // 폭발 파티클 프리팹
    public Transform bossTransform;         // 보스의 위치
    public float warningDuration = 2f;      // 경고 원 표시 시간
    public float spawnRange = 15f;          // 폭발이 발생할 범위
    public float minDistanceFromBoss = 5f;  // 보스와 너무 가까운 위치는 제외
    public int explosionCount = 10;         // 총 폭발 횟수
    public float delayBetweenExplosions = 0.5f; // 각 폭발 간 시간 지연

    private void OnEnable()
    {
        StartCoroutine(SpawnExplosions());
    }

    private IEnumerator SpawnExplosions()
    {
        for (int i = 0; i < explosionCount; i++)
        {
            // 1. 보스 근처의 랜덤 위치를 얻음 (보스와 최소 거리 유지)
            Vector3 randomPosition = GetValidRandomPosition();

            // 2. 경고 원 생성
            GameObject warningCircle = Instantiate(warningCirclePrefab, randomPosition, Quaternion.Euler(90f, 0f, 0f));

            // 3. 경고 시간이 지난 후 원 제거 및 폭발 파티클 생성
            yield return new WaitForSeconds(warningDuration);
            Destroy(warningCircle);
            Instantiate(explosionPrefab, randomPosition, Quaternion.identity);

            // 4. 다음 폭발까지 대기
            yield return new WaitForSeconds(delayBetweenExplosions);
        }
    }

    private Vector3 GetValidRandomPosition()
    {
        Vector3 randomPosition;
        float distance;

        do
        {
            // X, Z 축에서 랜덤 좌표 생성 (보스 위치를 중심으로)
            float randomX = Random.Range(-spawnRange, spawnRange);
            float randomZ = Random.Range(-spawnRange, spawnRange);

            randomPosition = new Vector3(
                bossTransform.position.x + randomX,
                bossTransform.position.y + 0.1f, // Y축은 보스와 동일한 높이로
                bossTransform.position.z + randomZ
            );

            // 보스와의 거리 계산
            distance = Vector3.Distance(bossTransform.position, randomPosition);
        }
        while (distance < minDistanceFromBoss); // 보스와 너무 가까운 위치는 제외

        return randomPosition;
    }
}
