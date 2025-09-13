using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * SkillUpgradeData
 * 싱글턴으로 스킬 업그레이드 관련 데이터(ScriptableObject 테이블)를 관리하는 클래스
 * SkillUpgradeTable에 업그레이드 데이터 참조
 */
public class SkillUpdgradeData : MonoBehaviour
{
    public static SkillUpdgradeData Instance { get; private set; }

    public ScriptableObject SkillUpgradeTable;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
