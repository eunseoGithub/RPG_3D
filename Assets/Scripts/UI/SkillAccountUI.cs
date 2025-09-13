using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
/*
 * SkillAccountUI
 * 플레이어가 선택한 스킬 업그레이드 내역을 Ui에 표시
 * 기능 요약 : 
 * - ScrollRect를 이용한 스킬 선택 내역 표시
 * - 스킬 선택 시 관련 업그레이드 내역 목록 생성
 * - 스킬 이미지 및 이름 표시
 * - 패널 열기/닫기 처리
 */
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
