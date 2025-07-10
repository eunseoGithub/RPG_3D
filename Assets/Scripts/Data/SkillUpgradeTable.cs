using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

