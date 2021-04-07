using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour
{
    private const float Y_ANGLE_MIN = 0.0f;
    private const float Y_ANGLE_MAX = 50.0f;

    public Transform lookAt;
    public Transform camTransform;
    public float distance = 5.0f;

    private float currentX = 0.0f;
    private float currentY = 45.0f;
    private float sensitivityX = 20.0f;
    private float sensitivityY = 20.0f;
    private bool thirdpov = false;

    // Start is called before the first frame update
    void Start()
    {
        if (thirdpov == true)
        {
            camTransform = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        
        float mxVal = Input.GetAxis("Mouse X");

        if (mxVal > 0)
            transform.Rotate(0f, 1f, 0f);
        if (mxVal < 0)
            transform.Rotate(0f, -1f, 0f);
        
        float myVal = Input.GetAxis("Mouse Y");

        if (myVal > 0)
            transform.Rotate(0f, 1f, 0f);
        if (myVal < 0)
            transform.Rotate(0f, -1f, 0f);

    }
}
