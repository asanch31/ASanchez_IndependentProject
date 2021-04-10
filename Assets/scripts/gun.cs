using TMPro;
using UnityEngine;

//Gun script that keeps track of ammo amount and which bullet is equipped
public class gun : MonoBehaviour
{
    
    public float bulletSpeed = 10;
    public Rigidbody[] bulletPrefab;
    int bulletIndex = 0;
    public int ammo = 5;
    public TextMeshProUGUI ammoText;

    private AudioSource gunAudio;
    public AudioClip[] ShotSound;
    public AudioClip emptyAmmo;
   

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

    private void OnTriggerExit(Collider other)
    {
        //if player interacts with ammo box increase ammo by 50
        if ((other.gameObject.CompareTag("ammo")))
        {
            other.gameObject.SetActive(false);
            // Add one to the score variable 'count'
            ammo = ammo + 50;

            //max ammo is 200
            if( ammo> 200)
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


    // Update is called once per frame 

    void Update()

    {

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
