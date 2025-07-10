using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuUI : MonoBehaviour
{
    public GameObject SoundPanel;
    public GameObject DisplayPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Button_Sound()
    {
        SoundPanel.SetActive(true);
    }
    public void Button_Display()
    {
        DisplayPanel.SetActive(true);
    }
    public void Button_Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void Button_Back()
    {
        this.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
