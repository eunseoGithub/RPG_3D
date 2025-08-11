using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        //Debug.Log("player trigger"+other.name);
        if (character.isInvincible)
            return;
        if (other.CompareTag("EnemyAttack"))
        {
            float damage = other.GetComponent<SkillDamage>().damage;
            character.GetDamage(damage);
        }
        if(other.CompareTag("item"))
        {
            Destroy(other.gameObject);
            character.posionCount++;
            character.posionCountText.text = character.posionCount.ToString();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
