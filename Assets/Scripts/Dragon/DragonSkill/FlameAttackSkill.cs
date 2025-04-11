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
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Character>().GetDamage(damage);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
