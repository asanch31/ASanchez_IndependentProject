﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossKey : MonoBehaviour
{

    public GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = boss.transform.position;
    }
}