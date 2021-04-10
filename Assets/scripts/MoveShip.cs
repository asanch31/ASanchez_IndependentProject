using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShip : MonoBehaviour
{
    private GameConditions Win;

    
    // Start is called before the first frame update
    void Start()
    {
        Win = GameObject.Find("Player").GetComponent<GameConditions>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Win.winCondition == true)
        {
            
            transform.Translate(Vector3.forward * Time.deltaTime);
            
        }
    }
}
