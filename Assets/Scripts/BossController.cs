using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Transform player;
    public int speed = 3;
    public bool acting = false;
    void LookAtPlayer()
    {
        if(player !=null && !acting)
        {
            Vector3 dir = player.transform.position - transform.position;
            this.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * speed);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(player);
        LookAtPlayer();
    }
}
