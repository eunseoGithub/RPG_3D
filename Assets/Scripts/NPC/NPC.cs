using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * NPC
 * 플레이어와 상호작용 가능한 NPC를 관리
 * 기능 요약 : 
 * - 플레이어가 NPC 근처에 들어오면 상호작용 버튼(UI) 활성화
 * - 플레이어가 NPC를 떠나면 UI 비활성화
 * - 플레이어가 지정 키를 입력 시 대화 시작
 * - NPC 위치에 따라 UI 위치 실시간 업데이트
 */
public class NPC : MonoBehaviour
{
    public Transform npcTransform; // NPC 위치
    public RectTransform pButtonUI; // UI 위치
    public Camera mainCamera;
    private bool isPlayerNear = false;
    public GameObject NPC_Panel;
    public DialogueManager dialogueManager;

    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pButtonUI.gameObject.SetActive(true);
            isPlayerNear = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pButtonUI.gameObject.SetActive(false);
            isPlayerNear = false;
        }
    }
    void OnDisable()
    {
        if(pButtonUI!=null)
            pButtonUI.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (isPlayerNear)
        {
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(npcTransform.position + Vector3.up * 2);
            pButtonUI.position = screenPosition;

            if (Input.GetKeyDown(KeyCode.G))
            {
                dialogueManager.StartDialogue(); // 대화 시작
            }
        }
    }
}
