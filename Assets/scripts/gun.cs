using TMPro;
using UnityEngine;


public class gun : MonoBehaviour
{
    // Start is called before the first frame update
    public float bulletSpeed = 10;
    public Rigidbody[] bulletPrefab;
    int bulletIndex = 0;
    public int ammo = 5;
    public TextMeshProUGUI ammoText;

    void Start()
    {
        amountAmmo();
        
        
    }

    // Use this for initialization
    void amountAmmo()
    {
        
        ammoText.text = "Ammo: " + ammo.ToString();
    }



    void Fire()
    {

        if (ammo > 0)
        {
            print("ammo = " + ammo);
            
            Rigidbody bullet = Instantiate(bulletPrefab[bulletIndex], transform.position + (transform.forward) + (transform.up), Quaternion.identity);
            //bullet.transform.Rotate(0, 0, 90);
            bullet.velocity = transform.forward * bulletSpeed;
            Destroy(bullet.gameObject, 3f);
            ammo--;
            amountAmmo();
        }
        else
        {
            
            print("look for ammo");
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if ((other.gameObject.CompareTag("ammo")))
        {
            other.gameObject.SetActive(false);
            // Add one to the score variable 'count'
            ammo = ammo + 50;
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
