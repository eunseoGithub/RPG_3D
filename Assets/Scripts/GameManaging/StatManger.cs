using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManger : MonoBehaviour
{
    public static StatManger Instance { get; private set; }
    
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
