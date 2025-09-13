using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * LogManager
 * 게임 내 로그 메세지를 화면 UI에 출력하고 관리
 * 기능 요약 : 
 * - 싱글톤 패턴으로 인스턴스 관리
 * - ScrollRect와 연동
 * - 로그 추가 시 자동 스크롤
 */
public class LogManager : MonoBehaviour
{
    public static LogManager Instance { get; private set; }

    public ScrollRect scrollRect;
    public RectTransform content;
    public Text logTextPrefab;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void AddLog(string message)
    {
        Text newLog = Instantiate(logTextPrefab, content);
        newLog.text = message;

        Canvas.ForceUpdateCanvases();
        ScrollToBottom();
    }

    private void ScrollToBottom()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(content);
        scrollRect.verticalNormalizedPosition = 0f;
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
