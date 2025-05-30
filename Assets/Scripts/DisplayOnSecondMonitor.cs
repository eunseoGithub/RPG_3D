using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayOnSecondMonitor : MonoBehaviour
{
    void Start()
    {
        // 원하는 해상도 설정 (예: 1920x1080), 창모드로 실행
        Screen.SetResolution(1920, 1080, false);

    }

}
