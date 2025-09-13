using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * SkillUpgradeTable
 * 스킬 업그레이드 데이터를 저장하는 ScriptableObject
 * LevelUpgrade : 특정 레벨에서 가능한 업그레이드 선택지 목록
 * SkillUpgradeChoice : 업그레이드 가능한 스킬, 선택지 번호, 설명 포함
 * Skill enum : 스킬 식별용
 */
[CreateAssetMenu(fileName = "SkillUpgradeTable", menuName = "RPG/Skill Upgrade Table")]
public class SkillUpgradeTable : ScriptableObject
{
    public List<LevelUpgrade> levels;
}

[System.Serializable]
public class LevelUpgrade
{
    public int level;
    public List<SkillUpgradeChoice> choices;
}

[System.Serializable]
public class SkillUpgradeChoice
{
    public Skill skill; // "AA", "Q", "W", "E", "R"
    public int number; // UI나 식별 용도 (선택지 0 or 1 등)
    [TextArea] public string description;
}
public enum Skill
{
    AA = 0,
    Q,
    W,
    E,
    R,
}

