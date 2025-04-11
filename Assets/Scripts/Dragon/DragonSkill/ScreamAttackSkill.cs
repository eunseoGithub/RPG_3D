using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamAttackSkill : MonoBehaviour
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
        if (other.CompareTag("Land"))
        {
            Destroy(gameObject); // 땅에 닿으면 파이어볼 제거
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
