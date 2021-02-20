using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCamera : MonoBehaviour
{
    //allow object to connect with player object
    public GameObject player;
    // offset camera from player position (third person view)
    private Vector3 offset = new Vector3(0, 4, -12);
    // Start is called before the first frame update
    private float horizontalInput;
    void Start()
    {

        horizontalInput = Input.GetAxis("Horizontal");
    }

    // Update is called once per frame
    void Update()
    {
        //allow camera to follow object with offset for third person view
        transform.position = player.transform.position;
        transform.Rotate(Vector3.up * Time.deltaTime * 20 * horizontalInput);
    }
}
