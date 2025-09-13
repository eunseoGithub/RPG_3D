using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * Character
 * 플레이어 캐릭터의 상태, 스탯, 경험치, 레벨, 인벤토리, 회복 및 UI 업데이트 관리
 * 기능 요약 : 
 * - Hp, Mp, Exp, 레벨 관리 & 관련 UI 업데이트
 * - 레벨업 체크 및 UI 표시
 * - 아이템 추가/삭제
 * - 무적 상태 적용
 * - 자동 체력/마나 회복 및 힐 아이템 회복 관리
 */
public enum ItemType
{
    Key,
    Posion
}
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
    public bool isInvincible;//무적 판정
    public LevelUpUI levelUpUI;
    public int posionCount;
    public Text posionCountText;
    public List<ItemType> inventory = new List<ItemType>();
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
        isInvincible = false;
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
    public void UpdateHpBar()
    {
        if (hpBarImage != null && hpText != null)
        {
            int currentMaxHp = stat.statData.stat[level - 1].hp;
            hpBarImage.fillAmount = hp / currentMaxHp; // HP 비율 반영
            hpText.text = $"HP : ({hp} / {currentMaxHp})";
        }
    }
    public void UpdateMpBar()
    {
        if (mpBarImage != null && mpText != null)
        {
            int currentMaxMp = stat.statData.stat[level - 1].mp;
            mpBarImage.fillAmount = mp / currentMaxMp; // MP 비율 반영
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
        if (level >= 20)
            return;
        if (exp >= stat.statData.stat[level - 1].exp)
        {
            exp -= stat.statData.stat[level - 1].exp;
            level++;
            UpdateExp();
            UpdateHpBar();
            UpdateMpBar();
            UpdateLevel();
            levelUpUI.ShowLevelUpUI();
        }
    }

    public void AddItem(ItemType item)
    {
        inventory.Add(item);
    }
    public void RemoveItem(ItemType item)
    {
        inventory.Remove(item);
    }

    public void SetInvincible(float duration)
    {
        StartCoroutine(InvincibleCoroutine(duration));
    }
    private IEnumerator InvincibleCoroutine(float duration)
    {
        isInvincible = true;
        yield return new WaitForSeconds(duration);
        isInvincible = false;
    }
    public float GetMaxHp()
    {
        float maxHp = stat.statData.stat[level - 1].hp;
        return maxHp;
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
