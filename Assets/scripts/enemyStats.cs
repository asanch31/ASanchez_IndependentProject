using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStats : MonoBehaviour
{
    private gun damage;
    private GameConditions difficulty;

    private int hiDif = 4;


    public int health=10;

    private Animator animEnemy;
    public bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        damage = GameObject.Find("Player").GetComponent<gun>();
        difficulty = GameObject.Find("Player").GetComponent<GameConditions>();
        animEnemy = gameObject.GetComponentInChildren<Animator>();
        
        
        
    }

    
    // Update is called once per frame
    void Update()
    {
        if(hiDif != difficulty.rockSamples)
        {
            BiggerHealth();
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
            health= health - damage.dmg;
            Health();
        }
        if (other.gameObject.CompareTag("bullet2"))
        {
            Destroy(other);
            health = health - damage.dmg2;
            Health();
        }
        
    }

    void BiggerHealth()
    {
        hiDif--;
        
        if (hiDif == 3 || hiDif == 2)
        {
            health = 5;
        }
        if (hiDif == 1)
        {
            health = 10;
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
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        
    }

}
