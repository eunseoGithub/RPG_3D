using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * ActiveBossHP
 * 플레이어가 특정 트리거 영역에 들어갈 때 보스 체력 UI 및 보스 애니메이션 활성화
 */
public class ActiveBossHP : MonoBehaviour
{
    public GameObject bossHP;
    public Animator bossAnim;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bossHP.SetActive(true);
            bossAnim.enabled = true;
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
