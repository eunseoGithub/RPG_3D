using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTrigger : MonoBehaviour
{
    Character character;
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponentInParent<Character>();  
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("player trigger"+other.name);
        if (other.CompareTag("EnemyAttack"))
        {
            float damage = other.GetComponent<SkillDamage>().damage;
            character.GetDamage(damage);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
