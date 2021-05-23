using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossStats : MonoBehaviour
{
    private gun damage;
    private SpawnManager difficulty;
    private int minionCount;

    private int waveNum;

    private float fullHealth;
    public float health=20;

    private Animator animEnemy;
    public bool dead = false;

    private float healthBossMult;

    public Image healthBar;
    public GameObject rockSample;


    // Start is called before the first frame update
    void Start()
    {
        
        dead = false;
        damage = GameObject.Find("Player").GetComponent<gun>();
        difficulty = GameObject.Find("Boss").GetComponent<SpawnManager>();
        animEnemy = gameObject.GetComponentInChildren<Animator>();


        healthBossMult = health;
        health = healthBossMult * difficulty.maxWaves+ health;
        fullHealth = health;
        print(health+"   " +fullHealth);
    }


    // Update is called once per frame
    void Update()
    {
        minionCount = FindObjectsOfType<MinionStats>().Length;


        if (minionCount == 0 && waveNum < 1)
        {
            print(waveNum + "   " + difficulty.maxWaves);
            LoseHealth();
            waveNum++;
        }


    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Death"))
        {

            health = 0;
            Health();
        }
        //player interaction with ememy or hazards
        if (other.gameObject.CompareTag("bullet"))
        {
            Destroy(other);
            //damage monster
            health = health - damage.dmg;
            Health();
        }
        if (other.gameObject.CompareTag("bullet2"))
        {
            Destroy(other);
            health = health - damage.dmg2;
            Health();
        }

    }

   public void LoseHealth()
    {
        if (difficulty.waveNum > 1)
        {
            print("losing health");
            health = health - healthBossMult;
            Health();
        }
        
    }
    void Health()
    {
        healthBar.fillAmount = health / fullHealth;
        if (health <= 0)
        {
            animEnemy.SetBool("death", true);
            dead = true;
            
            StartCoroutine(DeathAnim());


        }
    }
    IEnumerator DeathAnim()
    {
        yield return new WaitForSeconds(2);
        rockSample.SetActive(true);
        gameObject.SetActive(false);
        

    }
}
