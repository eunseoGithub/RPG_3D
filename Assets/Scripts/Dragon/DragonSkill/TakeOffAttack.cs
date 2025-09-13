using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * TakeOffAttack
 * 보스의 공격 패턴(격자 형태의 폭발 생성)
 * OnEnable() : 격자 폭발 패턴 시작(코루틴 실행)
 * OnDiable() : 생성된 오브젝트 정리
 * SpawnGridExplosions() : 보스 위치 기준으로 가로/세로 격자 패턴을 순서대로 생성
 * SpawnWarningAndExplosions() : 경고 이펙트 ->폭발 & colider 순서대로 생성
 * GetGround() : 레이케스트로 지면 높이 계산
 */
public class TakeOffAttack : MonoBehaviour
{
    public GameObject warningPrefeb;
    public GameObject explosionPrefeb;
    public GameObject coliderPrefeb;
    public Transform bossTransform;
    public float warningDuration = 0.5f;
    public float spacing = 4f;
    public int gridCount = 5;
    public float delayBetWeenSpawns = 0.3f;

    private List<GameObject> spawnedObjects = new List<GameObject>();
    public LayerMask groundLayer;
    private void OnEnable()
    {
        StartCoroutine(SpawnGridExplosions());
    }

    private void OnDisable()
    {
        foreach(GameObject obj in spawnedObjects)
        {
            if (obj != null)
                Destroy(obj);
        }
        spawnedObjects.Clear();
    }
    
    private IEnumerator SpawnGridExplosions()
    {
        for(int i = 0; i< gridCount; i++)
        {
            Vector3 spawnPos = bossTransform.position + new Vector3(0, 0, i * spacing - 10f);
            Quaternion rotation = Quaternion.Euler(0f, 90f, 0f);
            yield return StartCoroutine(SpawnWarningAndExplosion(spawnPos, rotation));
        }

        for(int i = 0; i < gridCount; i++)
        {
            Vector3 spawnPos = bossTransform.position + new Vector3(i * spacing - 10f, 0, 0);
            Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);
            yield return StartCoroutine(SpawnWarningAndExplosion(spawnPos, rotation));
        }
    }

    private IEnumerator SpawnWarningAndExplosion(Vector3 basePos, Quaternion rotation)
    {
        float yPos = GetGroundY(basePos);
        Vector3 warningPosition = new Vector3(basePos.x, yPos, basePos.z);

        GameObject warning = Instantiate(warningPrefeb, warningPosition, rotation);
        spawnedObjects.Add(warning);

        yield return new WaitForSeconds(warningDuration);

        Transform rightTransform = warning.transform.Find("Area/Right");
        Vector3 explosionPos = rightTransform != null ? rightTransform.position : warningPosition;

        GameObject explosion = Instantiate(explosionPrefeb, explosionPos, rotation);
        GameObject coliderObj = Instantiate(coliderPrefeb, explosionPos, rotation);

        spawnedObjects.Add(explosion);
        spawnedObjects.Add(coliderObj);

        spawnedObjects.Remove(warning);
        Destroy(warning);

        yield return new WaitForSeconds(delayBetWeenSpawns);
    }

    private float GetGroundY(Vector3 position)
    {
        Ray ray = new Ray(position + Vector3.up * 50f, Vector3.down);
        if(Physics.Raycast(ray, out RaycastHit hit,100f, groundLayer))
        {
            return hit.point.y - 0.8f;
        }
        return position.y;
    }
}

