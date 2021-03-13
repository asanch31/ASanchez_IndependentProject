using UnityEngine;


public class PlayerController : MonoBehaviour
{


    //public GameManger gameManager;

    //intialize float to allow user control of car(object) speed.
    public float speed = 15f;
    //intialize float to allow user control of car(object) turnspeed
    public float turnSpeed = 20f;

    //allow player to control object with userinput
    private float horizontalInput;
    private float verticalInput;

    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);

        }

        //allow player to control object with userinput
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");


        // move player based on speed variable and user input
        transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
        transform.Translate(Vector3.right * Time.deltaTime * turnSpeed * horizontalInput);

        // turn player based on speed variable and user input
        //transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);

        float mxVal = Input.GetAxis("Mouse X");

        if (mxVal > 0)
            transform.Rotate(0f, 1f, 0f);
        if (mxVal < 0)
            transform.Rotate(0f, -1f, 0f);


    }
}
