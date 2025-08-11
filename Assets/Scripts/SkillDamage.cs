using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SkillDamage : MonoBehaviour
{
    public float damage;
    public bool snare;
    public bool dot;
    public bool slow;
    public Skill skill;
    public string attackId;

    private void Awake()
    {
        attackId = Guid.NewGuid().ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        //damage = 10;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
