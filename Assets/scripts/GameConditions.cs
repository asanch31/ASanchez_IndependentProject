using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameConditions : MonoBehaviour

    //script where game is managed and controlled
    // player has to find keys(rock samples) and safely leave
    // player has health and can be damaged by enemies and asteroids
{
    //collect items and finish and health variables
    private int key;
    private int finish;
    public int health;
    int rockSamples;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI FindText;
    public GameObject findKey;
    public GameObject foundKey;
    public GameObject Lose;
    public GameObject Win;

    public bool winCondition=false;

    public GameObject ship;


    //add finishing element not implemented yet
   // public GameObject finishKey;

    public bool gameOver = false;

    public ParticleSystem damagePSystem;
    public ParticleSystem deathPSystem;
    public ParticleSystem shipjets;


    private AudioSource asPlayer;
    public AudioClip damageSound;
    public AudioClip deathSound;

    // Start is called before the first frame update
    void Start()
    {
        // Set the objectives 
        key = 0;
        rockSamples = 4;
        finish = 0;

        

        FindKeyText();
        Health();
        

        asPlayer = GetComponent<AudioSource>();

        // Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank

        //findKey.SetActive(true);
        Lose.SetActive(false);
        foundKey.SetActive(false);
        Win.SetActive(false);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //player interacts with key/rocks
        if (other.gameObject.CompareTag("key"))
        {
            other.gameObject.SetActive(false);
            // Add one to the score variable 'rocksamples'
            rockSamples--;
            key = key + 1;
            

            // Run the 'SetCountText()' function (see below)
            FindKeyText();
        }

        //player interaction with ememy or hazards
        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("hazard"))
        {
            //random damage occurs when player touches enemy/hazard
            damagePSystem.Play();
            int randomDMG = Random.Range(2, 8);
            health = health - randomDMG; 
            Health();
            
            //player jumps(moves) backwards to avoid constant damage (damage sound plays)
            if (other.gameObject.CompareTag("enemy"))
            {
                transform.Translate(Vector3.forward * -3);
                asPlayer.PlayOneShot(damageSound, 1.0f);
            }
            //gameManager.PositionPlayer();   respawn player not implemented
        }
        if (other.gameObject.CompareTag("ship"))
        {
            FoundFinish();
        }
    }

    private void OnTriggerExit(Collider other)
    {

        //finishKey.SetActive(false);  not implemented


    }
    void FindKeyText()
    {

        if (key == 4)
        {
            // if found all objective play text message
            FindText.text = "";
            foundKey.SetActive(true);
            
            finish = finish + 1;

        }

        //else display objective (find rock samples)
        else
        {
            foundKey.SetActive(false);
            FindText.text = "Samples Left: " + rockSamples.ToString();
        }

    }
    void FoundFinish()
    {
        // if found objectives (keys) and health >0 toggle win trigger
        if (key > 3 && health > 0)
        {

            Win.SetActive(true);
            foundKey.SetActive(false);
            shipjets.Play();
            winCondition = true;
            

            //
            //gameManager.Level1();
            //SceneManager.LoadScene(1);
        }

        //else display objectives and health status
        else
        {
            
            FindText.text = "Samples Left: " + health.ToString();

        }
    }

    //health: does player have health
    //Yes: Game continues
    //No: player is Dead (play sound effect)
    void Health()
    {
        healthText.text = "Health: " + health.ToString();
        if (health <= 0)
        {
            deathPSystem.Play();
            asPlayer.PlayOneShot(deathSound, 1.0f);
            Lose.SetActive(true);
            gameOver = true;
          
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
