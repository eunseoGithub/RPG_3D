using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * DisplayUI
 * 화면 해상도 및 모드 설정 UI 관리
 * 기능 요약 : 
 * - Dropdown UI를 통해 해상도, 화면 모드 선택 가능
 */
public class DisplayUI : MonoBehaviour
{
    public Dropdown DisplayDropdown;
    public Dropdown ModeDropdown;

    // Start is called before the first frame update
    void Start()
    {
        DisplayDropdown.onValueChanged.AddListener(DisplayUpdate);
        ModeDropdown.onValueChanged.AddListener(ModeUpdate);
    }
    void DisplayUpdate(int value)
    {
        switch (value)
        {
            case 0:
                Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
                Debug.Log("1920, 1080 적용됨");
                break;
            case 1:
                Screen.SetResolution(1024, 768, FullScreenMode.Windowed);
                Debug.Log("1024, 768 적용됨");
                break;
            case 2:
                Screen.SetResolution(800, 600, FullScreenMode.Windowed);
                Debug.Log("800, 600 적용됨");
                break;
        }
    }
    void ModeUpdate(int value)
    {
        switch (value)
        {
            case 0: // 전체화면모드
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                Screen.fullScreen = true;
                Debug.Log("전체화면으로 변경됨");
                break;
            case 1: // 창모드
                Screen.fullScreenMode = FullScreenMode.Windowed;
                Screen.fullScreen = false;
                Debug.Log("창모드로 변경됨");
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
