using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * ScreamAttackSkill
 * 공격 스킬의 충돌 처리
 * OnTriggerEnter() : 땅에 닿으면 오브젝트 제거
 */
public class ScreamAttackSkill : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Land"))
        {
            Destroy(gameObject); // 땅에 닿으면 파이어볼 제거
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
