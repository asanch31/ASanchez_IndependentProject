using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private int minionCount;

    private GameConditions playerCtrl;


    private bool onGround = true;
    public bool gameOver = false;
    public float jumpForce = 10;

    private Rigidbody rbPlayer;
    private Animator animPlayer;
    //private CharacterController controller;


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

        minionCount = FindObjectsOfType<MinionStats>().Length;
        print(minionCount);
        playerCtrl = GameObject.Find("Player").GetComponent<GameConditions>();
        asPlayer = GetComponent<AudioSource>();

        animPlayer = gameObject.GetComponentInChildren<Animator>();
        rbPlayer = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //if space is pressed player jumps including sounds and animations
        if (Input.GetKeyUp(KeyCode.Space) && onGround == true && playerCtrl.gameOver == false)
        {

            animPlayer.SetInteger("jump", 1);
            rbPlayer.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            onGround = false;
            dirtSystem.Stop();
            jetsSystem.Play();
            asPlayer.PlayOneShot(jumpSound, 1.0f);

            animPlayer.SetBool("ground_b", false);

        }
        verticalInput = Input.GetAxis("Vertical");


        //if player is pressing forward than character will move and play walk animation
        if (verticalInput != 0 && playerCtrl.gameOver == false)
        {
            animPlayer.SetInteger("walk", 1);

        }

        //if player is standing still character will stay in idle animation
        else if (verticalInput == 0 && playerCtrl.gameOver == false)
        {
            animPlayer.SetInteger("walk", 0);

            //play dirt particle when running
            if (onGround == true)
            {
                dirtSystem.Play();
            }
            else
            {
                dirtSystem.Stop();
            }
        }

        //if player dies or game ends particle system("dirt") stops and so does player
        else if (playerCtrl.gameOver == true)
        {
            animPlayer.SetBool("death", true);
            dirtSystem.Stop();

        }

        //player can move if game hasn't ended
        if (playerCtrl.gameOver == false)
        {
            //allow for movement in air (jump forward)
            transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);


            //when player touces ground signals to stop jumping animation
            if (onGround == true)
            {
                animPlayer.SetInteger("jump", 0);
            }
        }
        //player can rotate when alive
        if (playerCtrl.gameOver == false)
        {
            float turn = Input.GetAxis("Horizontal");
            transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);

        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        //when player touches ground 
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
