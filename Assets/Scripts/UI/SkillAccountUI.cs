using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillAccountUI : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform content;
    public Text skillAccountTextPrefeb;
    public Image skillImage;
    public Text skillName;
    public SkillUpgradeTable skillUpgradeTable;
    
    public Sprite spriteAA, spriteQ, spriteW, spriteE, spriteR;
    public GameObject currentskillAccountPanel;
    void AddSkillAccount(string account)
    {
        Text newAccount = Instantiate(skillAccountTextPrefeb, content);
        newAccount.text = account;

        Canvas.ForceUpdateCanvases();
        ScrollToBottom();
    }
    private void ScrollToBottom()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(content);
        scrollRect.verticalNormalizedPosition = 0f;
    }
    public void SkillAccountClick()
    {
        ClearSkillAccount();
        Skill currentSkill = EventSystem.current.currentSelectedGameObject.GetComponent<SkillTag>().skill;
       
        skillName.text = currentSkill.ToString();
        skillImage.sprite = GetSKillSprite(currentSkill);
        int count = StatManger.Instance.playerChoices.Count;
        int accountNum = 1;
        for (int i = 0; i< count; i++)
        {
            if(StatManger.Instance.playerChoices[i].skill == currentSkill)
            {
                AddSkillAccount($"[{accountNum}] {StatManger.Instance.playerChoices[i].description}");
                accountNum++;
            }
        }
        //List<LevelUpgrade> levels = skillUpgradeTable.levels;
        //for (int i = 0; i< levels.Count; i++)
        //{
        //    LevelUpgrade levelUpgrade = levels[i];
        //    for(int j = 0; j < levelUpgrade.choices.Count; j++)
        //    {
        //        SkillUpgradeChoice choice = levelUpgrade.choices[j];
        //        if(choice.skill == currentSkill)
        //        {
        //            AddSkillAccount($"[Lv.{levelUpgrade.level}] {choice.description}");
        //        }
        //    }
        //}
        currentskillAccountPanel.SetActive(true);
    }
    private void ClearSkillAccount()
    {
        for(int i = content.childCount - 1; i>=0; i--)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }
    private Sprite GetSKillSprite(Skill skill)
    {
        switch (skill)
        {
            case Skill.AA:
                return spriteAA;
            case Skill.Q:
                return spriteQ;
            case Skill.W:
                return spriteW;
            case Skill.E:
                return spriteE;
            case Skill.R:
                return spriteR;
            default:
                return null;
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
