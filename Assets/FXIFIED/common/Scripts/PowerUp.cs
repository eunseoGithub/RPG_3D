using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public GameObject pickupEffect;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup();
            Debug.Log("11");
        }
    }

    void Pickup()
    {
        Instantiate(pickupEffect, transform.position, transform.rotation);


        Destroy(gameObject);
    }
}
