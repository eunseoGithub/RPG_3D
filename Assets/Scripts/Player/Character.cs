using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    float Hp;
    // Start is called before the first frame update
    void Start()
    {
        Hp = 100;
    }
    void SetHp(float hp)
    {
        Hp = hp;
    }
    float GetHp()
    {
        return Hp;
    }
    public void GetDamage(float damage)
    {
        Hp -= damage;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
