using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour

//script where game is managed and controlled
// player has to find keys(rock samples) and safely leave
// player has health and can be damaged by enemies and asteroids

    //controls player health
{
    //collect items and finish and health variables
    
    private bool finish;
    public float health;
    private float maxHealth =30;
    private int mxHealthBoost = 5 ;

    //how many samples need to be found

    public int rockSamples;

    //display player health
    public Image healthBar;


    //Ui elements and objectives
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI FindText;
    public GameObject findKey;
    public GameObject finishObj;
    public GameObject findBossKey;
    public GameObject Lose;
    public GameObject Win;

    //last key/boss key
    public GameObject LastKey;
    private bool bossKey;

    public bool winCondition = false;
    public GameObject ship;


  
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
        // Set the objectives/find number of total objectives
        
        
        finish = false;

        //objectives
        FoundKey();
        //Player health
        Health();


        asPlayer = GetComponent<AudioSource>();

        // Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
        LastKey.SetActive(false);
        bossKey = false;
        Lose.SetActive(false);
        finishObj.SetActive(false);
        findBossKey.SetActive(false);
        Win.SetActive(false);

    }

    //buff activates for 60 seconds
    
    //player interaction with other objects
    private void OnTriggerEnter(Collider other)
    {
        //if player interacts with health potion
        if (other.CompareTag("health"))
        {

            other.gameObject.SetActive(false);
            StartCoroutine(respawnBuff(other.gameObject));

            powerIndicator.SetActive(true);
            //increase health 
            health = health + maxHealth/2;
            //if health goes above maxhealth turn on Buff icon
            if (health > maxHealth)
            {
                healthBuffUI.SetActive(true);

            }

            //maxHealth receives small boost with each potion, boost is permanent
            maxHealth = maxHealth + mxHealthBoost;
            Health();

            //start timer for how long powerup last
            StartCoroutine(PowerUpCountdown());

        }
        //player interacts with key/rocks
        if (other.gameObject.CompareTag("key"))
        {
            other.gameObject.SetActive(false);
            // subtract one to the score variable 'rocksamples'
            rockSamples--;
            



            // Run the 'SetCountText()' function (see below)
            FoundKey();
        }
        if (other.gameObject.CompareTag("bossKey"))
        {
            other.gameObject.SetActive(false);
            bossKey = true;
            findBossKey.SetActive(false);
            FoundKey();
        }

        //if player falls off map player dies
        if (other.gameObject.CompareTag("Death"))
        {
            health = 0;
            Health();
        }

        //if player gets hit by Boss projectile dmg is received
        // boss does more dmg than regular enemy
        if (other.gameObject.CompareTag("Boss Projectile"))
        {
            
            int randomDMG = Random.Range(10, 15);
            health = health - randomDMG;
            Health();
        }
        
            //player interaction with ememy or hazards or boss
            if (other.gameObject.CompareTag("enemy") ||
                other.gameObject.CompareTag("hazard") || other.gameObject.CompareTag("Boss"))

            {
            //check to see if player is alive
            if (gameOver == false)
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
                // destroy non-enemy objects that hits player
                if (other.gameObject.CompareTag("hazard"))
                {
                    Destroy(other.gameObject);
                }
            }

            //gameManager.PositionPlayer();   respawn player not implemented
        }

        //finsh objective (return to ship)
        if (other.gameObject.CompareTag("ship"))
        {
            FoundFinish();
        }
    }

    private void OnTriggerExit(Collider other)
    {

        //finishKey.SetActive(false);  not implemented


    }

    //what happens when player finds key/rocksample
    void FoundKey()
    {
       
        //if all rock samples are found (0 are left)
        if (rockSamples == 0 && bossKey == true)
        {

            
            // if found all objective play text message
           
            finishObj.SetActive(true);

            //change sample text to blank
            FindText.text = "";

            //Player now has finsih objective
            finish = true;
            
        }
        else if (rockSamples == 0 && bossKey == false)
        {
            // if found all objective play text message
            FindText.text = "Samples Left: " + rockSamples.ToString();
            findBossKey.SetActive(true);

        }

        //else display objective (find rock samples)
        else 
        {
            
            FindText.text = "Samples Left: " + rockSamples.ToString();

        }

    }
    void FoundFinish()
    {
        // if found objectives (keys) and health >0 toggle win trigger
        if (finish==true && health > 0 )
        {

            
            finishObj.SetActive(false);
            shipjets.Play();
            winCondition = true;


            StartCoroutine(WinScreen());

            //level 2 if implemented
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
        healthBar.fillAmount = health / maxHealth;
        
        healthText.text = "Health: " + health.ToString();

        //player is dead
        if (health <= 0 && gameOver == false)
        {
            deathPSystem.Play();
            asPlayer.PlayOneShot(deathSound, 1.0f);
            Lose.SetActive(true);
            gameOver = true;

            StartCoroutine(DeathScreen());



        }
    }
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
    IEnumerator respawnBuff(GameObject buff)
    {
        //respan ammo box after 100 seconds
        yield return new WaitForSeconds(10);
        buff.gameObject.SetActive(true);

    }
    IEnumerator WinScreen()
    {
        yield return new WaitForSeconds(2);
        Win.SetActive(true);
    }
    IEnumerator DeathScreen()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(2);
    }

    // Update is called once per frame
    void Update()
    {
        rockSamples = GameObject.FindGameObjectsWithTag("key").Length + GameObject.FindGameObjectsWithTag("bossKey").Length;
        FoundKey();
        //don't display buff Icon if health < max health
        if (health < maxHealth)
        {
            healthBuffUI.SetActive(false);

        }
    }

}
