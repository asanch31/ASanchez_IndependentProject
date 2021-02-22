using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    // Start is called before the first frame update
    public float bulletSpeed = 10;
    public Rigidbody bulletPrefab;


    void Fire()
    {

        Rigidbody bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        //bullet.transform.Rotate(0, 0, 90);
        bullet.velocity = transform.forward * bulletSpeed;
    }

        // Use this for initialization
        void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
            Fire();

    }
}
