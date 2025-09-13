using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * CloseUI
 * UI 닫기 버튼 처리
 */
public class CloseUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void XButton_CloseUI()
    {
        this.gameObject.transform.parent.gameObject.SetActive(false);
        if (transform.parent.name == "SkillAccountUI"|| transform.parent.name == "Display_Panel" || transform.parent.name == "Sound_Panel")
            return;
        else
            PauseManager.Instance.GameResume();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
