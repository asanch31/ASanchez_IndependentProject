using System.Collections;
using UnityEngine;

public class BossStats : MonoBehaviour
{
    private gun damage;
    private SpawnManager difficulty;
    private int minionCount;

    private int waveNum = 0;


    public int health=20;

    private Animator animEnemy;
    public bool dead = false;

    private int healthBossMult;


   

    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        damage = GameObject.Find("Player").GetComponent<gun>();
        difficulty = GameObject.Find("Boss").GetComponent<SpawnManager>();
        animEnemy = gameObject.GetComponentInChildren<Animator>();

       
        healthBossMult = health;
        health = healthBossMult * difficulty.maxWaves+ health;
    }


    // Update is called once per frame
    void Update()
    {
        minionCount = FindObjectsOfType<MinionStats>().Length;
        
        
        if (minionCount ==0 && waveNum != difficulty.maxWaves)
        {

            LoseHealth();
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

    void LoseHealth()
    {
        waveNum++;
        print(minionCount);
        if (minionCount == 0)
        {
            health = health - healthBossMult;
            Health();
        }
        


    }
    void Health()
    {
        print("Enemy Health = " + health);
        if (health <= 0)
        {
            animEnemy.SetBool("death", true);
            dead = true;

            print("enemy is dead " + dead);

            StartCoroutine(DeathAnim());


        }
    }
    IEnumerator DeathAnim()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);

    }
}
