using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * MonsterPool
 * 몬스터 풀 관리
 * 기능 요약 : 
 * - 지정된 개수의 몬스터를 미리 생성하고 풀에 저장
 * - active 수만큼 초기 몬스터를 활성화
 * - 몬스터 사망 시 비활성화 후 풀에서 다음 몬스터 활성화
 * - 몬스터 스폰 위치를 랜덤으로 생성하며, 최소 거리를 유지
 * - 모든 몬스터 제거 기능 지원
 */
public class MonsterPool : MonoBehaviour
{
    public GameObject monsterPrefab;
    private List<GameObject> pools = new List<GameObject>();
    public int poolSize;//pool에 저장될 max 몬스터 개수
    public int active;//pool에서 active 상태가 될 몬스터 개수
    public int currentActive;//현재 active 상태인 몬스터 개수
    public float spawnRadius = 5.0f; // 스폰 반경
    public float minDistance = 1.5f; // 몬스터 간 최소 거리
    private List<bool> poolLive = new List<bool>();
    // Start is called before the first frame update
    void Start()
    {
        poolSize = 10;
        active = 3;
        currentActive = 10;
        if (monsterPrefab != null)
        {
            for (int i = 0; i < poolSize; i++)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition();
                GameObject mon = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
                mon.gameObject.SetActive(false);
                currentActive--;
                pools.Add(mon);
                poolLive.Add(true);
            }
        }
        for (int i = 0; i < active; i++)
        {
            pools[i].SetActive(true);
            currentActive++;
        }
        //StartCoroutine(WatchingLoop());
    }
    void OnEnable()
    {
        Monster.OnMonsterDeath += OnMonsterDie;
    }

    void OnDisable()
    {
        Monster.OnMonsterDeath -= OnMonsterDie;
    }
    public void OnMonsterDie(Monster monster)
    {
        int index = pools.IndexOf(monster.gameObject);
        if (index != -1)
        {
            poolLive[index] = true;
            currentActive--;

            for (int k = 0; k < poolSize; k++)
            {
                if (!pools[k].activeSelf)
                {
                    pools[k].SetActive(true);
                    currentActive++;
                    break;
                }
            }
        }
    }
    public void RemoveMonster()
    {
        for (int i = 0; i < pools.Count; i++)
        {
            Destroy(pools[i]);
        }
    }
    Vector3 GetRandomSpawnPosition()
    {
        Vector3 spawnPosition;
        int attempts = 0;
        bool validPosition;

        do
        {
            // 랜덤한 위치를 MonsterPool 오브젝트 기준으로 생성
            Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
            spawnPosition = new Vector3(transform.position.x + randomCircle.x, transform.position.y, transform.position.z + randomCircle.y);

            validPosition = true;

            // 기존 몬스터들과 거리 검사
            foreach (GameObject mon in pools)
            {
                if (mon.activeSelf) // 현재 활성화된 몬스터만 검사
                {
                    float distance = Vector3.Distance(spawnPosition, mon.transform.position);
                    if (distance < minDistance)
                    {
                        validPosition = false;
                        break;
                    }
                }
            }

            attempts++;
            if (attempts > 20) // 무한 루프 방지 (20번 시도 후 그냥 배치)
            {
                Debug.LogWarning("무한 루프 방지입니다.");
                break;
            }

        } while (!validPosition);

        return spawnPosition;
    }
    
    // Update is called once per frame
    void Update()
    {

    }
}
