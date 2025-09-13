using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * TimelineOnOff
 * 컷신 실행 시 UI 관리
 * 기능 요약 : 
 * - 컷신 시작/종료 시 UI 숨기고 복원
 */
public class TimelineOnOff : MonoBehaviour
{
    public GameObject skillUI, buttonUI, playerUI, expUI, textUI,bossHpUI;
    private bool bossUIActive;
    private void Awake()
    {
        skillUI.SetActive(false);
        buttonUI.SetActive(false);
        playerUI.SetActive(false);
        expUI.SetActive(false);
        textUI.SetActive(false);
        if (bossHpUI.activeSelf == true)
        {
            bossUIActive = true;
            bossHpUI.SetActive(false);
        }
            
    }
    private void OnDisable()
    {
        skillUI.SetActive(true);
        buttonUI.SetActive(true);
        playerUI.SetActive(true);
        expUI.SetActive(true);
        textUI.SetActive(true);
        if (bossUIActive)
            bossHpUI.SetActive(true);
    }
}
