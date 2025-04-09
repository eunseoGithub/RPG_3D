using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonColision : MonoBehaviour
{
    GameObject DragonObj;
    Dragon dragon;
    // Start is called before the first frame update
    void Start()
    {
        DragonObj = transform.root.gameObject;
        dragon = DragonObj.GetComponent<Dragon>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerAttack"))
        {
            //데미지 처리
            dragon.GetDamage(10);
            Destroy(other.gameObject);
        }
        else
        {

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
