using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSkillFunction : MonoBehaviour
{
    public static UpgradeSkillFunction Instance { get; private set; }

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
    private Character character;
    private CharacterControl characterControl;
    private SkillCooldownManager skillCoolDownManager;
    private Animator charAni;
    private CharacterAnimationEvent characterAnimationEvent;
    private void Start()
    {
        character = this.GetComponent<Character>();
        characterControl = this.GetComponent<CharacterControl>();
        skillCoolDownManager = this.GetComponent<SkillCooldownManager>();
        charAni = this.GetComponent<Animator>();
        characterAnimationEvent = this.GetComponent<CharacterAnimationEvent>();
    }
    public void Choice2_1()
    {
        float damage = StatManger.Instance.QDamage;
        StatManger.Instance.QDamage = damage + damage * 0.1f;
    }
    public void Choice2_2()
    {
        StatManger.Instance.eDotDuration += 1f;
    }
    public void Choice3_1()
    {
        float speed = charAni.GetFloat("Attack01Speed");
        speed = speed * 1.15f;
        charAni.SetFloat("Attack01Speed", speed);
        float fireballSpeed = characterAnimationEvent.Attack01Prefab.GetComponent<Attack01Skill>().speed;
        characterAnimationEvent.Attack01Prefab.GetComponent<Attack01Skill>().speed = fireballSpeed * 1.15f;
    }
    public void Choice3_2()
    {
        Vector3 scale = characterAnimationEvent.Attack03Prefab.transform.localScale;
        scale.x = scale.x * 1.1f;
        scale.y = scale.y * 1.1f;
        scale.z = scale.z * 1.1f;
        characterAnimationEvent.Attack03Prefab.transform.localScale = scale;
    }
    public void Choice4_1()
    {
        //Q 스킬 쿨타임 -1초
        characterControl.Attack02CoolDown -= 1.0f;
    }
    public void Choice4_2()
    {
        //R 범위 +10%
        Vector3 scale = characterAnimationEvent.Attack05Prefab.transform.localScale;
        scale.x = scale.x * 1.1f;
        scale.y = scale.y * 1.1f;
        scale.z = scale.z * 1.1f;
        characterAnimationEvent.Attack05Prefab.transform.localScale = scale;
    }
    public void Choice5_1()
    {
        StatManger.Instance.eDotDamage = StatManger.Instance.eDotDamage * 1.2f;
    }
    public void Choice5_2()
    {
        StatManger.Instance.wSnareDuration += 1f;
    }
    public void Choice6_1()
    {
        //Q 관통 가능(1명까지)
        StatManger.Instance.qMaxPenetration = 2;
    }
    public void Choice6_2()
    {
        //R 시전 중 무적 0.5초
        characterAnimationEvent.rSkillInvincibleOn = true;
    }
    public void Choice7_1()
    {
        //평타 데미지 +15%
        StatManger.Instance.QDamage = StatManger.Instance.QDamage * 1.15f;
    }
    public void Choice7_2()
    {
        //Q 스킬 발사속도 +20%
        float speed = charAni.GetFloat("Attack02Speed");
        speed = speed * 1.2f;
        charAni.SetFloat("Attack02Speed", speed);
        float fireballSpeed = characterAnimationEvent.Attack01Prefab.GetComponent<Attack01Skill>().speed;
        characterAnimationEvent.Attack01Prefab.GetComponent<Attack01Skill>().speed = fireballSpeed * 1.2f;
    }
    public void Choice8_1()
    {
        //E 범위 +15 %
        Vector3 scale = characterAnimationEvent.Attack04Prefab.transform.localScale;
        scale.x = scale.x * 1.15f;
        scale.y = scale.y * 1.15f;
        scale.z = scale.z * 1.15f;
        characterAnimationEvent.Attack04Prefab.transform.localScale = scale;

    }
    public void Choice8_2()
    {
        //W 적중 시 이동속도 -20% 디버프 추가 2초
        StatManger.Instance.wSlowDuration += 2.0f;
        StatManger.Instance.wSlowAmount += 0.2f;
        characterAnimationEvent.Attack03Prefab.GetComponent<SkillDamage>().slow = true;
    }
    public void Choice9_1()
    {
        StatManger.Instance.QDamage = StatManger.Instance.QDamage * 1.1f;
    }
    public void Choice9_2()
    {
        //R 쿨타임 -3초
        characterControl.Attack05CoolDown -= 3.0f;
    }
    public void Choice10_1()
    {
        StatManger.Instance.RDamage = StatManger.Instance.RDamage * 1.3f;
    }
    public void Choice10_2()
    {
        //②범위 +30%
        Vector3 scale = characterAnimationEvent.Attack05Prefab.transform.localScale;
        scale.x = scale.x * 1.3f;
        scale.y = scale.y * 1.3f;
        scale.z = scale.z * 1.3f;
        characterAnimationEvent.Attack05Prefab.transform.localScale = scale;
    }
    public void Choice10_3()
    {
        //③R 데미지 + 15 %, 범위 + 15%
        StatManger.Instance.RDamage = StatManger.Instance.RDamage * 1.15f;

        Vector3 scale = characterAnimationEvent.Attack05Prefab.transform.localScale;
        scale.x = scale.x * 1.15f;
        scale.y = scale.y * 1.15f;
        scale.z = scale.z * 1.15f;
        characterAnimationEvent.Attack05Prefab.transform.localScale = scale;
    }
    public void Choice11_1()
    {
        //평타 마나 회복 추가
        float maxMp = StatManger.Instance.statData.stat[character.GetLevel() - 1].mp;

        float mp = character.GetMp();
        Mathf.Min(mp + 2.0f, maxMp);

        character.SetMp(mp);
        character.UpdateMpBar();
    }
    public void Choice11_2()
    {
        //Q 스킬 슬로우 20% (1초) 효과 추가
        StatManger.Instance.qSlowDuration += 1.0f;
        StatManger.Instance.qSlowAmount += 0.2f;
        characterAnimationEvent.Attack02Prefab.GetComponent<SkillDamage>().slow = true;
    }
    public void Choice12_1()
    {
        //W 속박 범위 +20%
        Vector3 scale = characterAnimationEvent.Attack03Prefab.transform.localScale;
        scale.x = scale.x * 1.2f;
        scale.y = scale.y * 1.2f;
        scale.z = scale.z * 1.2f;
        characterAnimationEvent.Attack03Prefab.transform.localScale = scale;
    }
    public void Choice12_2()
    {
        //E 틱 간격 단축 (더 빠른 데미지)
        StatManger.Instance.eDotInterval -= 0.25f;
    }
    public void Choice13_1()
    {
        //Q 데미지 +15%
        StatManger.Instance.QDamage = StatManger.Instance.QDamage * 1.15f;
    }
    public void Choice13_2()
    {
        //R이 적을 맞출 때 마나를 10 회복
        float maxMp = StatManger.Instance.statData.stat[character.GetLevel() - 1].mp;

        float mp = character.GetMp();
        Mathf.Min(mp + 10.0f, maxMp);

        character.SetMp(mp);
        character.UpdateMpBar();
    }
    public void Choice14_1()
    {
        //Q 스킬 슬로우 20% (1초) 효과 추가
        StatManger.Instance.qSlowDuration += 1.0f;
        StatManger.Instance.qSlowAmount += 0.2f;
        characterAnimationEvent.Attack02Prefab.GetComponent<SkillDamage>().slow = true;
    }
    public void Choice14_2()
    {
        //W 쿨타임 -2초
        characterControl.Attack03CoolDown -= 2.0f;
    }
    public void Choice15_1()
    {
        //Q 적중 시 속박 0.3초 확률 (10%)
        characterAnimationEvent.Attack02Prefab.GetComponent<SkillDamage>().snare = true;
    }
    public void Choice15_2()
    {
        //Q 데미지 + 15%
        StatManger.Instance.QDamage = StatManger.Instance.QDamage * 1.15f;
    }
    public void Choice16_1()
    {
        //Q 스킬 쿨타임 초기화(50%)
        if (Random.value > 0.5f)
            return;
        
        Dictionary<KeyCode, float> cooldowns = characterControl.GetSkillCooldowns();
        Dictionary<KeyCode, float> lastUsedTimes = characterControl.GetLastSkillUseTimes();

        if (!cooldowns.ContainsKey(KeyCode.Q) || !lastUsedTimes.ContainsKey(KeyCode.Q))
            return;

        lastUsedTimes[KeyCode.Q] = Time.time - cooldowns[KeyCode.Q];
    }
    public void Choice16_2()
    {
        //Q 스킬 2연속 발사 가능 (쿨 2배)
        StatManger.Instance.qDoubleFireEnabled = true;
        characterControl.Attack02CoolDown *= 2f;
    }
    public void Choice17_1()
    {
        //평타 데미지 +30%
        StatManger.Instance.AADamage = StatManger.Instance.AADamage * 1.3f;
    }
    public void Choice17_2()
    {
        //W 장판 남은 적에게 도트 데미지
        characterAnimationEvent.Attack03Prefab.GetComponent<SkillDamage>().dot = true;
    }
    public void Choice18_1()
    {
        //E 스택형으로 변경 가능 (최대 2회)
        StatManger.Instance.eStackEnabled = true;
        StatManger.Instance.eCurrentStack = StatManger.Instance.eMaxStack;
    }
    public void Choice18_2()
    {
        //Q 스킬 관통 최대 3명까지
        StatManger.Instance.qMaxPenetration = 4;
    }
    public void Choice19_1()
    {
        //Q 데미지+20%
        StatManger.Instance.QDamage = StatManger.Instance.QDamage * 1.2f;
    }
    public void Choice19_2()
    {
        //R 사용 시 체력 회복(현재 체력의 50%)
        float hp = character.GetHp();
        float maxHp = character.GetMaxHp();
        hp = Mathf.Min(hp*1.5f, maxHp);
        character.SetHp(hp);
    }
    public void Choice20_1()
    {
        //①R 전체 맵 범위 (쿨 60초)
        Vector3 scale = characterAnimationEvent.Attack05Prefab.transform.localScale;
        scale.x = scale.x * 5f;
        scale.y = scale.y * 5f;
        scale.z = scale.z * 5f;
        characterAnimationEvent.Attack05Prefab.transform.localScale = scale;
        characterControl.Attack05CoolDown = 60.0f;
    }
    public void Choice20_2()
    {
        //②모든 스킬 쿨타임 초기화(1회 사용)
        Dictionary<KeyCode, float> cooldowns = characterControl.GetSkillCooldowns();
        Dictionary<KeyCode, float> lastUsedTimes = characterControl.GetLastSkillUseTimes();

        if (!cooldowns.ContainsKey(KeyCode.Q) || !lastUsedTimes.ContainsKey(KeyCode.Q))
            return;
        if (!cooldowns.ContainsKey(KeyCode.W) || !lastUsedTimes.ContainsKey(KeyCode.W))
            return;
        if (!cooldowns.ContainsKey(KeyCode.E) || !lastUsedTimes.ContainsKey(KeyCode.E))
            return;
        if (!cooldowns.ContainsKey(KeyCode.R) || !lastUsedTimes.ContainsKey(KeyCode.R))
            return;

        lastUsedTimes[KeyCode.Q] = Time.time - cooldowns[KeyCode.Q];
        lastUsedTimes[KeyCode.W] = Time.time - cooldowns[KeyCode.W];
        lastUsedTimes[KeyCode.E] = Time.time - cooldowns[KeyCode.E];
        lastUsedTimes[KeyCode.R] = Time.time - cooldowns[KeyCode.R];
    }

    public void Choice20_3()
    {
        //③R 2회 연속 사용 가능
        StatManger.Instance.rDoubleFireEnabled = true;
    }

}