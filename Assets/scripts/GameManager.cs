using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour

//script where game is managed and controlled
// player has to find keys(rock samples) and safely leave
// player has health and can be damaged by enemies and asteroids

    //controls player health
{
    //collect items and finish and health variables
    private int key;
    private int finish;
    public int health;
    private int maxHealth =30;
    private int mxHealthBoost = 10;
    public int rockSamples = 4;

    //display player health
    public Image healthBar;


    //Ui elements and objectives
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI FindText;
    public GameObject findKey;
    public GameObject foundKey;
    public GameObject Lose;
    public GameObject Win;

    public bool winCondition = false;
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

    //powerup var
    public GameObject powerIndicator;
    public GameObject healthBuffUI;
    

    // Start is called before the first frame update
    void Start()
    {
        
        // Set the objectives 

        rockSamples = 4;
        finish = 0;


        //GUI
        FindKeyText();
        Health();


        asPlayer = GetComponent<AudioSource>();

        // Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank

        //findKey.SetActive(true);
        Lose.SetActive(false);
        foundKey.SetActive(false);
        Win.SetActive(false);

    }

    //buff activates for 60 seconds
    IEnumerator PowerUpCountdown()
    {
        yield return new WaitForSeconds(60);
        
       //health inc for 60 secs if potion buff is acquired
       //after 60 secs stats return to maxHealth if health has more than maxhealth
        healthBuffUI.SetActive(false);
        powerIndicator.SetActive(false);
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        Health();
    }
    private void OnTriggerEnter(Collider other)
    {
        //if player interacts with health potion
        if (other.CompareTag("health"))
        {
            
            Destroy(other.gameObject);

            powerIndicator.SetActive(true);
            //increase health 
            health = health + maxHealth/2;
            //if health goes above maxhealth turn on Buff icon
            if (health > maxHealth)
            {
                healthBuffUI.SetActive(true);

            }
            maxHealth = maxHealth + mxHealthBoost;
            Health();

            StartCoroutine(PowerUpCountdown());

        }
        //player interacts with key/rocks
        if (other.gameObject.CompareTag("key"))
        {
            other.gameObject.SetActive(false);
            // Add one to the score variable 'rocksamples'
            rockSamples--;



            // Run the 'SetCountText()' function (see below)
            FindKeyText();
        }
        if (other.gameObject.CompareTag("Death"))
        {

            health = 0;
            Health();
        }
        if (other.gameObject.CompareTag("Boss Projectile"))
        {
            
            int randomDMG = Random.Range(10, 15);
            health = health - randomDMG;
            Health();
        }
        //player interaction with ememy or hazards
        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("hazard") || other.gameObject.CompareTag("Boss"))
        {
            //random damage occurs when player touches enemy/hazard
            damagePSystem.Play();
            int randomDMG = Random.Range(2, 8);
            health = health - randomDMG;
            Health();

            //player jumps(moves) backwards to avoid constant damage (damage sound plays)
            if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("Boss"))
            {
                transform.Translate(Vector3.forward * -3);
                asPlayer.PlayOneShot(damageSound, 1.0f);
            }
            if (other.gameObject.CompareTag("hazard"))
            {
                Destroy(other.gameObject);
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

        if (rockSamples == 0)
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
        if (rockSamples == 0 && health > 0)
        {

            Win.SetActive(true);
            foundKey.SetActive(false);
            shipjets.Play();
            winCondition = true;


            //
            //gameManager.Level1();
            //SceneManager.LoadScene(1);
        }

        //else display objectives left
        else
        {

            FindText.text = "Samples Left: " + rockSamples.ToString();

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
        if (health < maxHealth)
        {
            healthBuffUI.SetActive(false);

        }
    }

}
