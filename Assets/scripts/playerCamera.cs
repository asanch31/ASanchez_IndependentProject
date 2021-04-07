using UnityEngine;

public class playerCamera : MonoBehaviour
{
    //allow object to connect with player object
    public GameObject player;
    // offset camera from player position (third person view)
    
    public int cameravar = 0;
    Vector3 offset = new Vector3(0, 1, 0);

    //third person script variables (taken from unity script third person camera
    private const float Y_ANGLE_MIN = 0.0f;
    private const float Y_ANGLE_MAX = 30.0f;

    public Transform lookAt;
    public Transform camTransform;
    public float distance = 5.0f;

    private float currentX = 0.0f;
    private float currentY = 29.0f;
    private float sensitivityX = 15.0f;
    private float sensitivityY = 15.0f;
    
    //

    //is third person camera on
    private bool thirdpov = true;


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



        //allow camera to follow object with offset for third person view

        //switch too third person camera
        if (thirdpov == true)
        {
            currentX += Input.GetAxis("Mouse X");
            currentY += Input.GetAxis("Mouse Y");

            currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
        }
        if (thirdpov == false)
        {
            float mxVal = Input.GetAxis("Mouse X");
            currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);


        }


        if (Input.GetKeyUp(KeyCode.C))
        {
            print (" print c");
            thirdpov = true;

        }

        //first person camera
        else if (Input.GetKeyUp(KeyCode.V))
        {
            thirdpov = false;

        }

    }
    private void LateUpdate()
    {
        
        if (thirdpov == true)
        {
            
            Vector3 dir = new Vector3(0, 0, -distance);
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            camTransform.position = lookAt.position + rotation * dir;
            camTransform.LookAt(lookAt.position);
        }
        if (thirdpov== false)
        {
            transform.position = player.transform.position + offset;
            transform.rotation = player.transform.rotation;
        }
        
    }

}
