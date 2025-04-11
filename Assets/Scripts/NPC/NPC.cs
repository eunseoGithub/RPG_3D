using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
