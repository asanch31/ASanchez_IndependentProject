using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private GameConditions playerCtrl;


    private bool onGround = true;
    public bool gameOver = false;
    public float jumpForce = 10;

    private Rigidbody rbPlayer;
    private Animator animPlayer;
    private CharacterController controller;

    
    public ParticleSystem dirtSystem;
    public ParticleSystem jetsSystem;

    public AudioClip jumpSound;
    

    private AudioSource asPlayer;

    float verticalInput;
    public float speed = 600.0f;
    public float turnSpeed = 400.0f;
    
    public float gravity = 20.0f;

    void Start()
    {
        playerCtrl = GameObject.Find("Player").GetComponent<GameConditions>();
        asPlayer = GetComponent<AudioSource>();

        animPlayer = gameObject.GetComponentInChildren<Animator>();
        rbPlayer = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && onGround ==true && playerCtrl.gameOver == false)
        {
            
            animPlayer.SetInteger("jump", 1);
            rbPlayer.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            onGround = false;
            jetsSystem.Play();
            asPlayer.PlayOneShot(jumpSound, 1.0f);

            animPlayer.SetBool("ground_b", false);
            
        }
         verticalInput = Input.GetAxis("Vertical");
        if (verticalInput!=0 && playerCtrl.gameOver == false)
        {
            animPlayer.SetInteger("walk", 1);
            



        }
        else if (verticalInput == 0 && playerCtrl.gameOver == false)
        {
            animPlayer.SetInteger("walk", 0);

            //play dirt particle when running
            dirtSystem.Play();
        }
        else if (playerCtrl.gameOver == true)
        {
            animPlayer.SetBool("death", true);
            dirtSystem.Stop();

        }


        if (onGround == true && playerCtrl.gameOver ==false)
        {
            
            transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
            animPlayer.SetInteger("jump", 0);
            
        }

        if (playerCtrl.gameOver == false)
        {
            float turn = Input.GetAxis("Horizontal");
            transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            animPlayer.SetBool("ground_b", true);
            asPlayer.Stop();
            onGround = true;
            
        }
        /*else if (collision.gameObject.CompareTag("obstacle"))
        {
            Debug.Log("Game Over!");
            gameOver = true;
            animPlayer.SetBool("Death_b", true);
            animPlayer.SetInteger("DeathType_int", 1);
            explosionSystem.Play();
            dirtSystem.Stop();
            asPlayer.PlayOneShot(crashSound, 1.0f);
        }
        */
    }


}
