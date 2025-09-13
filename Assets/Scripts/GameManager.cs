using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
/*
 * GameManager
 * 게임 내 전반적인 진행 관리
 * 기능 요약 : 
 * - 플레이어 및 NPC, 몬스터 풀 관리
 * - 보스 및 게임 종료 조건 체크
 * - 문 열림 컷신 실행
 * - 게임 클리어/오버 UI 활성화
 */
public class GameManager : MonoBehaviour
{
    Character character;
    public DialogueManager dialogue;
    public List<GameObject> monsterPool;
    public GameObject npc;
    Vector3 npcPos;
    bool nextStep = false;
    public GameObject Boss;
    public GameObject GameOverPanel;
    public GameObject GameClearPanel;
    public GameObject DoorOpenCutController;
    public PlayableDirector doorCutTimeline;
    public GameObject GameEndPanel;
    // Start is called before the first frame update
    void Start()
    {
        character = Character.Instance;
        npcPos.x = 137.0f; npcPos.y = -0.03922876f; npcPos.z = -75.3f;
        doorCutTimeline.stopped += OnDoorCutTimelineFinished;
    }
    private void OnDoorCutTimelineFinished(PlayableDirector director)
    {
        if(director == doorCutTimeline)
        {
            Debug.Log("TimeLine 끝남");
            DoorOpenCutController.SetActive(false);
        }
    }
    public void PlayDoorOpenTimeline()
    {
        if (!nextStep && character.GetLevel() >= 20 && character.key == true)
        {
            for (int i = 0; i < monsterPool.Count; i++)
            {
                monsterPool[i].GetComponent<MonsterPool>().RemoveMonster();
                monsterPool[i].SetActive(false);
            }
            nextStep = true;
            DoorOpenCutController.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (character.GetHp() <= 0.0f)
        {
            GameEndPanel.SetActive(true);
            GameOverPanel.SetActive(true);
            PauseManager.Instance.GamePause();
        }
        if (Boss.GetComponent<Dragon>().GetHp() <= 0.0f)
        {
            GameEndPanel.SetActive(true);
            GameClearPanel.SetActive(true);
            PauseManager.Instance.GamePause();
        }

    }

}
