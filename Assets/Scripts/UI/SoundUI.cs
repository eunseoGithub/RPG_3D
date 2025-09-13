using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * SoundUI
 * 게임 내 사운드 설정 UI 관리
 * 기능 요약 : 
 * - 전체, 배경음, 스킬 볼륨 조절
 * - 슬라이더 값 변경 시 텍스트 UI 업데이트
 * - AudioSource 볼륨 실시간 적용
 */
public class SoundUI : MonoBehaviour
{
    public Slider totalSlider;
    public Slider backgroundSlider;
    public Slider skillSlider;
    public Text totalText;
    public Text backgroundText;
    public Text skillText;
    public AudioSource BGMManager;
    public AudioSource SoundManager;
    // Start is called before the first frame update
    void Start()
    {
        totalSlider.onValueChanged.AddListener(totalSoundUpdate);
        backgroundSlider.onValueChanged.AddListener(backgroundSoundUpdate);
        skillSlider.onValueChanged.AddListener(skillSoundUpdate);
    }
    void totalSoundUpdate(float value)
    {
        totalText.text = $"{(int)(value * 100)}";
        backgroundText.text = $"{(int)(value * 100)}";
        skillText.text = $"{(int)(value * 100)}";
        skillSlider.value = value;
        backgroundSlider.value = value;
        BGMManager.volume = value;
        SoundManager.volume = value;
    }
    void backgroundSoundUpdate(float value)
    {
        backgroundText.text = $"{(int)(value * 100)}";
        BGMManager.volume = value;
    }
    void skillSoundUpdate(float value)
    {
        skillText.text = $"{(int)(value * 100)}";
        SoundManager.volume = value;
    }
    // Update is called once per frame
    void Update()
    {
    }
}
