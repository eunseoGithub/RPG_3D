using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManger : MonoBehaviour
{
    public static StatManger Instance { get; private set; }
    
    public float AADamage;
    public float QDamage;
    public float WDamage;
    public float EDamage;
    public float RDamage;

    public float wSnareDuration = 2f;
    public float qSnareDuration = 0.3f;
    
    public float eDotDamage = 2f;
    public float eDotDuration = 5f;
    public float eDotInterval = 1f;
    
    public float qSlowAmount = 0.0f;
    public float qSlowDuration = 0.0f;
    
    public float wSlowAmount = 0.0f;
    public float wSlowDuration = 0.0f;
    
    public int qMaxPenetration = 1;
    
    public bool qDoubleFireEnabled = false;
    
    public float wDotDamage = 2f;
    public float wDotDuration = 3f;
    public float wDotInterval = 1f;

    public bool eStackEnabled = false;
    public int eMaxStack = 2;
    public int eCurrentStack = 2;

    public bool rDoubleFireEnabled = false;
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
        AADamage = 10.0f;
        QDamage = 15.0f;
        WDamage = 15.0f;
        EDamage = 15.0f;
        RDamage = 50.0f;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
