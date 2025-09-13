using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * StatManager
 * 게임 내 플레이어 및 스킬 관련 스탯을 관리하는 싱글톤
 * 기능 요약 : 
 * - 플레이어 스탯과 스킬 데미지, 효과 수치 관리
 * - 스킬별 prefab 참조 및 초기 스케일 설정
 * - 스킬 업그레이드 선택(playerChoices) 저장
 * - 스킬 관련 버프/쿨타임 상태 플래그 관리
 */
public class StatManger : MonoBehaviour
{
    public static StatManger Instance { get; private set; }
    public GameObject aaPrefab;
    public GameObject qPrefab;
    public GameObject wPrefab;
    public GameObject ePrefab;
    public GameObject rPrefab;

    public float AADamage;
    public float QDamage;
    public float WDamage;
    public float EDamage;
    public float RDamage;

    public float wSnareDuration = 2f;
    public float qSnareDuration = 0.3f;
    
    public float eDotDamage = 5f;
    public float eDotDuration = 5f;
    public float eDotInterval = 1f;
    
    public float qSlowAmount = 0.0f;
    public float qSlowDuration = 0.0f;
    
    public float wSlowAmount = 0.0f;
    public float wSlowDuration = 0.0f;
    
    public int qMaxPenetration = 1;
    
    public bool qDoubleFireEnabled = false;
    
    public float wDotDamage = 5f;
    public float wDotDuration = 3f;
    public float wDotInterval = 1f;

    public bool eStackEnabled = false;
    public int eMaxStack = 2;
    public int eCurrentStack = 2;

    public bool rDoubleFireEnabled = false;

    public bool qCoolReset = false;
    public bool aaMpUp = false;
    public bool rMpUp = false;
    [System.Serializable]
    public class Stats
    {
        public int level;
        public int hp;
        public int mp;
        public int exp;
    }

    [System.Serializable]
    public class StatData
    {
        public List<Stats> stat = new List<Stats>();
    }
    public StatData statData;
    public List<SkillUpgradeChoice> playerChoices;
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
    // Start is called before the first frame update
    void Start()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("StatData");
        if (textAsset != null)
        {
            statData = JsonUtility.FromJson<StatData>(textAsset.text);

        }
        else
        {
            Debug.LogError("StatData 파일을 찾을 수 없습니다.");
        }
        //AADamage = 50.0f;
        //QDamage = 15.0f;
        //WDamage = 15.0f;
        //EDamage = 15.0f;
        //RDamage = 50.0f;
        aaPrefab.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        qPrefab.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        wPrefab.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        ePrefab.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        rPrefab.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
