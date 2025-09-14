using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * DragonGimickController
 * 드래곤 보스 Hp 기믹 및 컷신 처리
 * 기능 요약 :
 * - Hp 체크 후 특정 수치에서 기믹 발동
 * - 기믹 시작 시 :
 *      - 보스 위치 이동
 *      - 보스 animator 비활성화(컷신 중 보스가 패턴 발동 방지)
 *      - 기믹 활성화 상태(isGimick = true)
 * - 기믹 종료 시 :
 *      - 보스 위치 원래 위치로 복귀
 *      - 보스 animator 활성화
 *      - 기믹 상태 해제(isGimick = false)
 * - Hp300/200 컷신 완료 이벤트 처리
 *      - Timeline 종료 후 보스 상태 복귀
 *      - Hp200 기믹 후 FireballSpawner 시작
 */
public class DragonGimickController : MonoBehaviour
{
    private float hp;
    private Dragon dragon;
    private bool hp300GimickTriggered;
    private bool hp200GimickTriggered;
    private Animator anim;
    private Vector3 bossPos;
    private Vector3 newPos;
    public GameObject bossHp300CutController;
    public GameObject bossHp200CutController;
    private BossHp300GimickCut bossHp300Cut;
    private BossHp200GimickCut bossHp200Cut;
    public GameObject uiCanvas;
    public bool isGimick;
    private bool bossHp300CutFinish;
    // Start is called before the first frame update
    void Start()
    {
        dragon = this.GetComponent<Dragon>();
        hp300GimickTriggered = false;
        hp200GimickTriggered = false;
        bossPos = this.transform.position;
        anim = this.GetComponent<Animator>();
        newPos = new Vector3(300f, 0f, 100f);
        bossHp300Cut = bossHp300CutController.GetComponent<BossHp300GimickCut>();
        bossHp300Cut.OnTimeLineFinished += OnHp300TimeLineFinished;
        bossHp200Cut = bossHp200CutController.GetComponent<BossHp200GimickCut>();
        bossHp200Cut.OnTimeLineFinished += OnHp200TImeLineFinished;
        isGimick = false;
        bossHp300CutFinish = false;
    }
    void CheckHp300()
    {
        if (hp300GimickTriggered)
            return;
        else
        {
            if(hp < 300)
            {
                hp300GimickTriggered = true;
                Hp300GimickStart();
            }
        }
    }
    void CheckHp200()
    {
        if (hp200GimickTriggered||!bossHp300CutFinish)
            return;
        else
        {
            if(hp<200)
            {
                hp200GimickTriggered = true;
                Hp200GimickStart();
            }
        }
    }
    
    void GimickStart()
    {
        bossPos = this.transform.position;
        this.transform.position = newPos;
        anim.enabled = false;
        isGimick = true;
    }
    void GimickEnd()
    {
        this.transform.position = bossPos;
        anim.enabled = true;
        isGimick = false;
    }
    void Hp300GimickStart()
    {
        GimickStart();
        bossHp300CutController.SetActive(true);
    }
    void OnHp300TimeLineFinished()
    {
        Debug.Log("OnHp300TimeLineFinished");
        GimickEnd();
        bossHp300CutController.transform.GetChild(0).gameObject.SetActive(false);
        bossHp300CutController.transform.GetChild(2).gameObject.SetActive(false);
        bossHp300CutFinish = true;
    }
    void Hp200GimickStart()
    {
        GimickStart();
        bossHp200CutController.SetActive(true);
    }
    void OnHp200TImeLineFinished()
    {
        Debug.Log("OnHp200TImeLineFinished");
        GimickEnd();
        bossHp200CutController.transform.GetChild(0).gameObject.SetActive(false);
        bossHp200CutController.transform.GetChild(2).gameObject.SetActive(false);
        bossHp200CutController.transform.GetChild(1).GetComponent<FireballSpawner>().GimickStart();
    }
    // Update is called once per frame
    void Update()
    {
        hp = dragon.GetHp();
        CheckHp300();
        CheckHp200();
    }
}
