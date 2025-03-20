using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public static Character Instance { get; private set; }
    public Image hpBarImage;
    public Image mpBarImage;
    public Image expBarImage;
    public Text expText;
    public Text hpText;
    public Text mpText;
    public Text levelText;
    [SerializeField]
    private float hp;
    private float mp;
    [SerializeField]
    private float exp;
    private StatManger stat;
    [SerializeField]
    int level;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 유지
        }
        else
        {
            Destroy(gameObject); // 중복 생성 방지
        }
    }
    void Start()
    {
        stat = StatManger.Instance;
        hp = 100;
        mp = 100;
        exp = 0;
        level = 1;
    }
    public void SetHp(float _hp)
    {
        hp = _hp;
    }
    public float GetHp()
    {
        return hp;
    }
    public void SetMp(float _mp)
    {
        mp = _mp;
    }
    public float GetMp()
    {
        return mp;
    }
    public void SetExp(float _exp)
    {
        exp = _exp;
    }
    public float GetExp()
    {
        return exp;
    }
    public void GetDamage(float damage)
    {
        hp -= damage;
        UpdateHpBar();
    }
    public void UseMp(float _mp)
    {
        mp -= _mp;
        UpdateMpBar();
    }
    void UpdateHpBar()
    {
        if (hpBarImage != null && hpText != null)
        {
            hpBarImage.fillAmount = hp / 100.0f; // HP 비율 반영
            int currentMaxHp = stat.statData.stat[level - 1].hp;
            hpText.text = $"HP : ({hp} / {currentMaxHp})";
        }
    }
    void UpdateMpBar()
    {
        if (mpBarImage != null && mpText != null)
        {
            mpBarImage.fillAmount = mp / 100.0f; // HP 비율 반영
            int currentMaxMp = stat.statData.stat[level - 1].mp;
            mpText.text = $"MP : ({mp} / {currentMaxMp})";
        }
    }
    public void UpdateExp()
    {
        if (expBarImage != null && expText != null)
        {
            if(level <= 10)
            {
                int nextLevelExp = stat.statData.stat[level - 1].exp;
                expBarImage.fillAmount = Mathf.Clamp01(exp / nextLevelExp);
                expText.text = $"EXP : ({exp} / {nextLevelExp})";
            }
            else
            {
                int nextLevelExp = stat.statData.stat[9].exp;
                expBarImage.fillAmount = Mathf.Clamp01(exp / nextLevelExp);
                expText.text = $"EXP : ({exp} / {nextLevelExp})";
            }
        }
    }
    public void UpdateLevel()
    {
        levelText.text = "LV." + level;
    }
    void CheckLevelUp()
    {
        if(exp >= stat.statData.stat[level - 1].exp)
        {
            exp -= stat.statData.stat[level - 1].exp;
            level++;
            UpdateExp();
            UpdateHpBar();
            UpdateMpBar();
            UpdateLevel();
        }
    }
    // Update is called once per frame
    void Update()
    {
        CheckLevelUp();
    }
}
