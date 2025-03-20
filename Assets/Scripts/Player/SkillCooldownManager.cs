using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooldownManager : MonoBehaviour
{
    public CharacterController characterController;

    [System.Serializable]
    public class SkillUI
    {
        public KeyCode key;
        public Image cooldownImage;
    }

    public List<SkillUI> skillUIList = new List<SkillUI>();

    private Dictionary<KeyCode, float> skillCooldowns;
    private Dictionary<KeyCode, float> lastSkillUseTime;

    void Start()
    {
        if (characterController == null)
        {
            Debug.LogError("CharacterController가 연결되지 않았습니다!");
            return;
        }

        skillCooldowns = characterController.GetSkillCooldowns();
        lastSkillUseTime = characterController.GetLastSkillUseTimes();

        // 초기화 - 쿨다운 이미지 숨기기
        foreach (SkillUI skill in skillUIList)
        {
            skill.cooldownImage.fillAmount = 0;
        }
    }

    void Update()
    {
        foreach (SkillUI skill in skillUIList)
        {
            if (skillCooldowns.ContainsKey(skill.key) && lastSkillUseTime.ContainsKey(skill.key))
            {
                float cooldown = skillCooldowns[skill.key];
                float lastUsed = lastSkillUseTime[skill.key];
                float elapsedTime = Time.time - lastUsed;

                if (elapsedTime < cooldown)
                {
                    skill.cooldownImage.fillAmount = 1 - (elapsedTime / cooldown);
                }
                else
                {
                    skill.cooldownImage.fillAmount = 0; // 쿨타임 종료 시 숨김
                }
            }
        }
    }
}
