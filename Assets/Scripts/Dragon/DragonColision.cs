using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonColision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerAttack"))
        {
            //데미지 처리
            Debug.Log($"[{gameObject.name}] player attack");
            Destroy(other.gameObject);
        }
        else
        {
            //Debug.Log("!1111111111");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
