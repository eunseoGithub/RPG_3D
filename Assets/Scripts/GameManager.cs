using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Character character;
    public DialogueManager dialogue;
    public List<GameObject> monsterPool;
    public GameObject npc;
    public GameObject door;
    Vector3 npcPos;
    bool nextStep = false;
    public GameObject skill2, skill3, skill4;
    public GameObject Boss;
    public GameObject GameOverPanel;
    public GameObject GameClearPanel;
    // Start is called before the first frame update
    void Start()
    {
        character = Character.Instance;
        npcPos.x = 137.0f; npcPos.y = -0.03922876f; npcPos.z = -75.3f;

    }
    void ActiveSkill()
    {
        skill2.SetActive(true);
        skill3.SetActive(true);
        skill4.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (!nextStep && character.GetLevel() >= 10 && character.key == true)
        {
            for (int i = 0; i < monsterPool.Count; i++)
            {
                monsterPool[i].GetComponent<MonsterPool>().RemoveMonster();
                monsterPool[i].SetActive(false);
            }
            dialogue.LoadDialogue(2);
            npc.transform.position = npcPos;
            npc.SetActive(true);
            door.SetActive(false);
            nextStep = true;
        }
        if (dialogue.finishDialogue)
        {
            if (!skill2.activeSelf)
            {
                ActiveSkill();
            }
        }
        if (character.GetHp() <= 0.0f)
        {
            GameOverPanel.SetActive(true);
        }
        if (Boss.GetComponent<Dragon>().GetHp() <= 0.0f)
        {
            GameClearPanel.SetActive(true);
        }
    }
}
