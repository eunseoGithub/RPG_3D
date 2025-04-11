using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    private GameObject rootObject;
    Animator rootAni;
    // Start is called before the first frame update
    void Start()
    {
        rootObject = gameObject.transform.root.gameObject;
        rootAni = rootObject.GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (rootAni.GetBool("Attack"))
        {
            if (other.CompareTag("Player"))
            {
                other.gameObject.GetComponent<Character>().GetDamage(10);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
