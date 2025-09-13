using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * PauseManger
 * 게임 일시정지 및 재개 관리 클래스
 */
public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void GamePause()
    {
        Time.timeScale = 0f;
    }
    public void GameResume()
    {
        Time.timeScale = 1f;
    }
    public bool IsPasued()
    {
        return Time.timeScale == 0f;
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
