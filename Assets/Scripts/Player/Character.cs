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
    private int level;
    public bool healOn;
    private float healTime;
    public float regenerateTime = 1.0f;
    public int healCount = 0;
    public bool key;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
        healOn = false;
        healTime = 5.0f;
        regenerateTime = 1.0f;
        healCount = 0;
        key = false;
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
    public void SetLevel(int _level)
    {
        level = _level;
    }
    public int GetLevel()
    {
        return level;
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
            if (level <= 10)
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
        if (exp >= stat.statData.stat[level - 1].exp)
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

        if (healOn && healCount < 5)
        {
            healTime -= Time.deltaTime;
            if (healTime <= 0.0f)
            {
                float maxHp = stat.statData.stat[level - 1].hp;
                hp = Mathf.Min(hp + 10.0f, maxHp); // HP가 maxHp를 넘지 않도록 제한
                healCount++;
                healTime = 1.0f; // 1초마다 회복
                UpdateHpBar();
            }
        }

        if (healOn && healCount >= 5)
        {
            healOn = false;
        }

        if (!healOn)
        {
            healTime = 5.0f;
            healCount = 0;
        }

        regenerateTime -= Time.deltaTime;
        if (regenerateTime <= 0.0f)
        {
            float maxHp = stat.statData.stat[level - 1].hp;
            float maxMp = stat.statData.stat[level - 1].mp;

            hp = Mathf.Min(hp + 1.0f, maxHp);  // HP가 maxHp를 넘지 않도록 제한
            mp = Mathf.Min(mp + 2.0f, maxMp);  // MP가 maxMp를 넘지 않도록 제한

            regenerateTime = 1.0f; // 다시 1초로 초기화
            UpdateHpBar();
            UpdateMpBar();
        }

    }
}
