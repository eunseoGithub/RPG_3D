﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBossHP : MonoBehaviour
{
    public GameObject bossHP;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bossHP.SetActive(true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
