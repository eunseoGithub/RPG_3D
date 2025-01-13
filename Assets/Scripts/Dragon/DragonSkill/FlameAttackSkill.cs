using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameAttackSkill : MonoBehaviour
{
    [SerializeField]
    float damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("other : " + other);
        if (other.CompareTag("Player"))
        {
            Debug.Log("player attack <Flame Attack>");
            other.GetComponent<Character>().GetDamage(damage);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
