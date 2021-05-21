using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class enemyStats : MonoBehaviour
{
    private gun damage;
    private GameManager difficulty;

    private int hiDif = 4;
    private float fullHealth;

    public float health = 3;

    private Animator animEnemy;
    public bool dead = false;

    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        fullHealth = health;
        dead = false;
        damage = GameObject.Find("Player").GetComponent<gun>();
        difficulty = GameObject.Find("Player").GetComponent<GameManager>();
        animEnemy = gameObject.GetComponentInChildren<Animator>();



    }


    // Update is called once per frame
    void Update()
    {
        if (hiDif != difficulty.rockSamples)
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
        hiDif--;

        if (hiDif == 3 || hiDif == 2)
        {
            health = health + 5;
            fullHealth = fullHealth + 3;
        }
        if (hiDif == 1)
        {
            health = health + 10;
            fullHealth = fullHealth + 5;
        }
        Health();

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
        yield return new WaitForSeconds(1);
        Destroy(gameObject);

    }

}
