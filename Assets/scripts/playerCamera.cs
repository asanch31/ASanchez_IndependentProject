using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCamera : MonoBehaviour
{
    //allow object to connect with player object
    public GameObject player;
    // offset camera from player position (third person view)
    private Vector3 offset = new Vector3(0, 1, 4);
    public int cameravar = 0;
    // Start is called before the first frame update
    
    void Start()
    {

      
    }

    // Update is called once per frame
    void Update()
    {
        //allow camera to follow object with offset for third person view
        transform.position = player.transform.position + offset;

        if (Input.GetKeyUp(KeyCode.C))
        {
            offset = new Vector3(0, 1, 4);
        }

        if (Input.GetKeyUp(KeyCode.V))
        {
            offset = new Vector3(0, 0, 0);
        }
       




    }
}
