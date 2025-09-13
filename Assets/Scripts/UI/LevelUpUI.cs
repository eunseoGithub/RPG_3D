using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * LevelupUI
 * 플레이어 레벨업 시 스킬 선택 UI 관리
 * 기능 요약 : 
 * - 레벨업 시 레벨업 패널 표시
 * - 스킬 선택 UI에 아이콘과 설명 표시
 * - 선택 가능한 스킬 수에 따라 UI 활성/비활성 처리
 * - 선택한 스킬에 따라 해당 메서드 실행
 * - 선택 기록은 StaManager에 저장
 * - 레벨 20 달성 시 GameManger를 통해 문 열기 컷신 실행
 */
public class LevelUpUI : MonoBehaviour
{
    public GameObject levelUpSkillPanel;
    public Animator otherLevelUpAnimator;
    public Animator levelUpTitleAnimator;
    private Character character;
    public GameObject skill1;
    public GameObject skill2;
    public GameObject skill3;
    public SkillUpgradeTable skillUpgradeTable;
    public Sprite spriteAA;
    public Sprite spriteQ;
    public Sprite spriteW;
    public Sprite spriteE;
    public Sprite spriteR;
    private int playerLevel;
    private int choiceCount;
    private LevelUpgrade upgrade;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        character = Character.Instance;
    }

    public void ShowLevelUpUI()
    {
        levelUpSkillPanel.SetActive(true);

        PauseManager.Instance.GamePause();

        playerLevel = character.GetLevel();
        choiceCount = 0;
        upgrade = skillUpgradeTable.levels.Find(x => x.level == playerLevel);
        if(upgrade == null)
        {
            Debug.LogWarning("해당 레벨에 대한 업그레이드 정보가 없습니다.");
            return;
        }
        choiceCount = skillUpgradeTable.levels[playerLevel - 2].choices.Count;
        if (choiceCount > 2)
        {
            skill3.SetActive(true);
        }
        else
        {
            skill3.SetActive(false);
        }
        SetSkill(skill1,0);
        SetSkill(skill2, 1);
        if(choiceCount>2)
            SetSkill(skill3, 2);
        otherLevelUpAnimator.SetTrigger("Play");
        levelUpTitleAnimator.SetTrigger("Play");
        
    }
    Sprite SetSkillImage(Skill skilltype)
    {
        switch (skilltype)
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
        }
        return null;
    }
    void SetSkill(GameObject skillUI,int choiceNum)
    {
        Transform imageTransform = skillUI.transform.Find("SkillImage");
        if(imageTransform != null)
        {
            Image skillImage = imageTransform.GetComponent<Image>();
            skillImage.sprite = SetSkillImage(upgrade.choices[choiceNum].skill);
        }
        skillUI.GetComponentInChildren<Text>().text = upgrade.choices[choiceNum].description;
    }
    public void LevelUpClick(int selectedIndex)
    {
        
        playerLevel = character.GetLevel();
        Debug.Log(playerLevel);
        upgrade = skillUpgradeTable.levels.Find(x => x.level == playerLevel);

        if(upgrade == null)
        {
            Debug.LogWarning("해당 레벨의 스킬 업그레이드 정보를 찾을 수 없습니다.");
            return;
        }
        if(selectedIndex <0 || selectedIndex >= upgrade.choices.Count)
        {
            Debug.LogWarning("선택 인덱스가 올바르지 않습니다.");
            return;
        }
        SkillUpgradeChoice selectedChoice = upgrade.choices[selectedIndex];
        StatManger.Instance.playerChoices.Add(selectedChoice);

        string methodName = $"Choice{playerLevel}_{selectedIndex + 1}";
        var method = typeof(UpgradeSkillFunction).GetMethod(methodName);
        if(method!= null)
        {
            method.Invoke(UpgradeSkillFunction.Instance, null);
        }
        else
        {
            Debug.LogError("메서드를 찾을수 없습니다.");
        }
        levelUpSkillPanel.SetActive(false);
        if (Character.Instance.GetLevel()==20)
        {
            gameManager.PlayDoorOpenTimeline();
        }
        PauseManager.Instance.GameResume();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
