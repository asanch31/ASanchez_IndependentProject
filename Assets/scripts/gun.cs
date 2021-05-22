using System.Collections;
using TMPro;
using UnityEngine;

//Gun script that keeps track of ammo amount and which bullet is equipped
public class gun : MonoBehaviour
{



    public float bulletSpeed = 10;
    public Rigidbody[] bulletPrefab;
    private string [] gunEquip=new string[] { "Pistol", "Rifle", "Shotgun" } ;
    
    int bulletIndex = 0;
    public int ammo = 25;
    public TextMeshProUGUI ammoText;

    private AudioSource gunAudio;
    public AudioClip[] ShotSound;
    public AudioClip emptyAmmo;

    public int dmg = 1;
    public int dmg2 = 2;

    //powerup var
    public GameObject powerIndicator;
    public GameObject dmgBuffUI;


    public TextMeshProUGUI gunText; 

    void Start()
    {
        amountAmmo();
        gunAudio = GetComponent<AudioSource>();
        

    }

    // Use this for initialization
    void amountAmmo()
    {
        //how much ammo does player have
        ammoText.text = "Ammo: " + ammo.ToString();
    }



    void Fire()
    {
        //if player has ammo which bullet prefab is equiped
        if (ammo > 0)
        {
            Rigidbody bullet = Instantiate(bulletPrefab[bulletIndex], transform.position + (transform.forward) + (transform.up), Quaternion.identity);

            bullet.velocity = transform.forward * bulletSpeed;
            gunAudio.PlayOneShot(ShotSound[bulletIndex], 1.0f);

            //Destroy bullet after 3 secs.
            Destroy(bullet.gameObject, 3f);

            ammo--;
            amountAmmo();
        }
        //if no ammo play emplyclip sound when attempting to shot
        else
        {
            gunAudio.PlayOneShot(emptyAmmo, 1.0f);

        }
    }
    IEnumerator PowerUpCountdown()
    {
        yield return new WaitForSeconds(60);

        
        powerIndicator.SetActive(false);
        dmgBuffUI.SetActive(false);
    }


    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("dmgBuff"))
        {
           
            dmgBuffUI.SetActive(true);
            dmg = dmg+2;
            dmg2 = dmg2+3;

            other.gameObject.SetActive(false);
            StartCoroutine(respawnBuff(other.gameObject));
            powerIndicator.SetActive(true);

            StartCoroutine(PowerUpCountdown());

        }
        //if player interacts with ammo box increase ammo by 50
        if ((other.gameObject.CompareTag("ammo")))
        {
            other.gameObject.SetActive(false);
            //respawn ammo box 
           StartCoroutine(respawnAmmo(other.gameObject));

            // Add one to the score variable 'count'
            int ammoBox = Random.Range(25, 100);
            ammo = ammo + ammoBox;

            //max ammo is 200
            if (ammo > 200)
            {
                ammo = 200;
            }
            amountAmmo();

        }

        else
        {
            amountAmmo();
        }
    }
    IEnumerator respawnBuff(GameObject box)
    {
        //respan ammo box after 100 seconds
        yield return new WaitForSeconds(10);
        box.gameObject.SetActive(true);

    }
    IEnumerator respawnAmmo(GameObject box)
    {
        //respan ammo box after 100 seconds
        yield return new WaitForSeconds(100);
        box.gameObject.SetActive(true);

    }
    // Update is called once per frame 

    void Update()

    {
        //check to see if rifle is equiped
        if (bulletIndex == 1)
        {
            gunText.text = "Equpied: " + gunEquip[bulletIndex] + " DMG:" + dmg2;
        }
        else
        {
            gunText.text = "Equpied: " + gunEquip[bulletIndex] + " DMG:" + dmg;
        }
        //rotate weapon by pressing right click (right mouse button) 
        //shot weapon with left click (left mouse button)
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            bulletIndex++;
            if (bulletIndex == bulletPrefab.Length)
            {
                bulletIndex = 0;
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
            Fire();

    }

}
