using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossKey : MonoBehaviour
{
    // script to have last key follow the boss
    public GameObject boss;
   
    void Update()
    {
        
        transform.position = boss.transform.position;
    }
}
