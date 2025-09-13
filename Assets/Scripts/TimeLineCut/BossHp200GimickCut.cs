using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
/*
 * BossHp200GimickCut
 * 보스 Hp 200 도달 시 발동되는 컷신/애니메이션 관리
 * 기능 요약 : 
 * - PlayableDirector를 통해 컷신 실행
 * - 보스 더미 애니메이션 트리거 실행
 * - 컷신 종료 시 콜백 호출
 */
public class BossHp200GimickCut : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public Animator bossDummyAnim;

    public System.Action OnTimeLineFinished;
    // Start is called before the first frame update
    void Start()
    {
        playableDirector.stopped += OnTimeLineStopped;
    }
    public void bossAnimStart()
    {
        bossDummyAnim.SetTrigger("start");
    }
    public void OnTimeLineStopped(PlayableDirector obj)
    {
        OnTimeLineFinished?.Invoke();
    }
    // Update is called once per frame
    void Update()
    {

    }
}
