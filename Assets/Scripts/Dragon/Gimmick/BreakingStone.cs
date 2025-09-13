using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * BreakingStone
 * 부서지는 돌 오브젝트 및 관련 보스 기믹 처리
 * OnTriggerEnter() : 플레이어 공격에 맞으면 countTrigger 감소, 0이 되면 오브젝트 파괴
 * Start() : 초기화
 * Update : 애니메이션 상태 확인 후 SpinFireBar 회전 활성화
 */
public class BreakingStone : MonoBehaviour
{
    [SerializeField]
    int countTrigger;
    private SpinFireBar spinFireBar;
    Animator anim;
    AnimatorStateInfo stateInfo;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerAttack"))
        {
            Destroy(other.gameObject);
            countTrigger--;
            if(countTrigger<=0)
            {
                Destroy(this.gameObject);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        countTrigger = 3;
        spinFireBar = transform.parent.GetComponentInChildren<SpinFireBar>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (spinFireBar != null)
        {
            if (stateInfo.IsName("BossGimickRock") && stateInfo.normalizedTime >= 1f)
            {
                if(!spinFireBar.rotateOn)
                {
                    spinFireBar.rotateOn = true;
                }
            }
        }

    }
}
