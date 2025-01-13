using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOffAttack : MonoBehaviour
{
    public GameObject warningPrefab;         // 경고 원 프리팹
    public GameObject explosionPrefab;       // 폭발 파티클 프리팹
    public GameObject coliderPrefab;
    public Transform bossTransform;          // 보스의 위치
    public float warningDuration = 2f;       // 경고 원 표시 시간
    public float spawnRange = 15f;           // 폭발이 발생할 범위
    public float minDistanceFromBoss = 5f;   // 보스와 너무 가까운 위치는 제외
    public int explosionCount = 10;          // 총 폭발 횟수
    public float delayBetweenExplosions = 0.5f; // 각 폭발 간 시간 지연

    private void OnEnable()
    {
        StartCoroutine(SpawnExplosions());
    }

    private IEnumerator SpawnExplosions()
    {
        for (int i = 0; i < explosionCount; i++)
        {
            //보스 근처의 랜덤 위치를 얻음 (보스와 최소 거리 유지)
            Vector3 randomPosition = GetValidRandomPosition();

            //warningPrefab의 y 위치 값은 -0.73f로 고정
            Vector3 warningPosition = new Vector3(randomPosition.x, -0.73f, randomPosition.z);

            //랜덤 y축 회전 값 생성
            Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

            Debug.Log("Random y rotation: " + randomRotation.eulerAngles.y);

            //경고 prefab과 폭발 prefab을 동일한 회전과 위치에서 생성
            GameObject warningCircle = Instantiate(warningPrefab, warningPosition, randomRotation);
            yield return new WaitForSeconds(warningDuration);
            Destroy(warningCircle);

            // 폭발 파티클 생성 (위치와 회전 동일)
            Vector3 leftPosition = warningCircle.GetComponent<WarningArea>().GetRightPosition();
            Instantiate(explosionPrefab, leftPosition, randomRotation);
            Instantiate(coliderPrefab, warningPosition, randomRotation);
            //다음 폭발까지 대기
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