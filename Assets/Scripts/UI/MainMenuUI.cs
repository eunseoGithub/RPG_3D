using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * MainMenuUI
 * 게임의 메인 메뉴 UI를 관리
 */
public class MainMenuUI : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject InventoryPanel;
    public GameObject SkillPanel;


    public void MenuButtonClick()
    {
        PauseManager.Instance.GamePause();
        MenuPanel.SetActive(true);
    }
    public void InventoryButtonClick()
    {
        PauseManager.Instance.GamePause();
        InventoryPanel.SetActive(true);
    }
    public void SkillButtonClick()
    {
        PauseManager.Instance.GamePause();
        SkillPanel.SetActive(true);
    }
    public void skillButtonExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
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
