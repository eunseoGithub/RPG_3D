using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;  // UI 텍스트
    public GameObject dialoguePanel; // 대화 패널
    private Queue<string> sentences; // 대사 저장 큐

    private bool isTyping = false; // 타이핑 중인지 확인
    public GameObject npc;
    public int currentId;
    public bool finishDialogue;
    void Start()
    {
        sentences = new Queue<string>();
        LoadDialogue(1);
        finishDialogue = false;
    }

    public void LoadDialogue(int dialogueSetId)
    {
        // JSON 파일 로드
        TextAsset jsonFile = Resources.Load<TextAsset>("NPCChat");
        currentId = dialogueSetId;
        if (jsonFile != null)
        {
            // JSON 파싱
            DialogueSetsData dialogueSetsData = JsonConvert.DeserializeObject<DialogueSetsData>(jsonFile.text);

            // 주어진 ID에 해당하는 대화 세트 찾기
            var selectedDialogueSet = dialogueSetsData.DialogueSets.Find(set => set.id == dialogueSetId);

            if (selectedDialogueSet != null)
            {
                // 기존 대사 큐 비우기
                sentences.Clear();

                // 선택된 대화 세트에서 대사 큐에 추가
                foreach (var item in selectedDialogueSet.Dialogue[0])
                {
                    sentences.Enqueue(item.Value);
                }
            }
            else
            {
                Debug.LogError("해당 ID에 해당하는 대화 세트를 찾을 수 없습니다!");
            }
        }
        else
        {
            Debug.LogError("NPCChat.json 파일을 찾을 수 없습니다!");
        }
    }

    void Update()
    {
        if (dialoguePanel.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        DisplayNextSentence();
    }

    void DisplayNextSentence()
    {
        if (isTyping) return; // 타이핑 중이면 입력 막기
        if (!dialoguePanel.activeSelf)
            dialoguePanel.SetActive(true);
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.01f); // 글자 타이핑 속도 조절
        }

        isTyping = false;
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        npc.SetActive(false);
        if (currentId == 2)
            finishDialogue = true;
    }
}
[System.Serializable]
public class DialogueSetsData
{
    public List<DialogueSet> DialogueSets; // 여러 대화 세트를 담은 리스트
}

[System.Serializable]
public class DialogueSet
{
    public int id;  // 대화 세트를 구분할 ID
    public List<Dictionary<string, string>> Dialogue; // 실제 대화 내용
}
