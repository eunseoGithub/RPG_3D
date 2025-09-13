using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * SkillCooldownManager
 * 플레이어 스킬 쿨타임 관리 및 UI 업데이트 클래스
 * 기능 요약 : 
 * - 각 스킬의 쿨타임을 추적하고 UI Image에 적용
 * - E 스킬 스택형 처리 :  최대 스택 수와 회복 시간 관리
 * - 스킬 사용 시 스택 회복 타이머 초기화
 * - 쿨다운 진행률을 image.fillAmount로 실시간 표시
 */
public class SkillCooldownManager : MonoBehaviour
{
    public CharacterControl characterController;

    [System.Serializable]
    public class SkillUI
    {
        public KeyCode key;
        public Image cooldownImage;
    }

    public List<SkillUI> skillUIList = new List<SkillUI>();
    
    private Dictionary<KeyCode, float> skillCooldowns;//쿨타임 길이
    private Dictionary<KeyCode, float> lastSkillUseTime;//마지막 사용 시각
    private Dictionary<KeyCode, float> stackRecoveryTimers = new Dictionary<KeyCode, float>();//스택형 쿨타임
    public GameObject eStackText;
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
            if (skill.key == KeyCode.E)
                stackRecoveryTimers[KeyCode.E] = Time.time;
        }
    }
    public void ResetStackTimer(KeyCode key)
    {
        stackRecoveryTimers[key] = Time.time;
    }
    void Update()
    {
        foreach (SkillUI skill in skillUIList)
        {
            if (!skillCooldowns.ContainsKey(skill.key) || !lastSkillUseTime.ContainsKey(skill.key))
                continue;
            float cooldown = skillCooldowns[skill.key];// 그 스킬의 총 쿨타임
            float lastUsed = lastSkillUseTime[skill.key];//마지막으로 스킬을 사용한 시각
            float elapsedTime = Time.time - lastUsed;//마지막 사용 이후 경과 시간 

            if(skill.key != KeyCode.E || !StatManger.Instance.eStackEnabled)
            {
                skill.cooldownImage.fillAmount = Mathf.Clamp01(1 - (elapsedTime / cooldown));
            }
            else
            {
                if (eStackText != null && eStackText.activeSelf== false)
                    eStackText.SetActive(true);
                int currentStack = StatManger.Instance.eCurrentStack;
                int maxStack = StatManger.Instance.eMaxStack;

                float elapsedStackTime = Time.time - stackRecoveryTimers[KeyCode.E];
                float stackProgress = Mathf.Clamp01(elapsedStackTime / cooldown);

                skill.cooldownImage.fillAmount = stackProgress;

                if(currentStack < maxStack && elapsedStackTime >= cooldown)
                {
                    StatManger.Instance.eCurrentStack++;
                    stackRecoveryTimers[KeyCode.E] = Time.time;
                }
                if (eStackText != null)
                    eStackText.GetComponent<Text>().text = StatManger.Instance.eCurrentStack.ToString();
            }

            if (skillCooldowns.ContainsKey(skill.key) && lastSkillUseTime.ContainsKey(skill.key))
            {

                //UI 업데이트
                if (elapsedTime < cooldown)
                {
                    skill.cooldownImage.fillAmount = 1 - (elapsedTime / cooldown);
                }
                else
                {
                    skill.cooldownImage.fillAmount = 0; // 쿨타임 종료 시 숨김
                }

                if (skill.key == KeyCode.E && StatManger.Instance.eStackEnabled)
                {
                    if (StatManger.Instance.eCurrentStack < StatManger.Instance.eMaxStack)//e 스택형일때 스택 회복
                    {
                        float elapsedStackTime = Time.time - stackRecoveryTimers[KeyCode.E];
                        if (elapsedStackTime >= cooldown)
                        {
                            StatManger.Instance.eCurrentStack++;
                            stackRecoveryTimers[KeyCode.E] = Time.time;
                        }
                    }
                }
            }
        }

    }
}
