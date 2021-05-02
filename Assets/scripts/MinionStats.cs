using System.Collections;
using UnityEngine;

public class MinionStats : MonoBehaviour
{
    private gun damage;



    public int health = 1;

    private Animator animEnemy;
    public bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        
        dead = false;
        damage = GameObject.Find("Player").GetComponent<gun>();

        animEnemy = gameObject.GetComponentInChildren<Animator>();



    }


    // Update is called once per frame
    void Update()
    {


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

    void BiggerHealth()
    {


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
