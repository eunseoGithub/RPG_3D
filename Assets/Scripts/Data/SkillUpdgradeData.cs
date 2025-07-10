using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
