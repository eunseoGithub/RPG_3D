using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public void GameStart()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    // Update is called once per frame
    void Update()
    {

    }
}
