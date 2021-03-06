﻿using UnityEngine;

public class asteroidRandom : MonoBehaviour

{

    //spawn asteroids over the map in random locations

    public GameObject[] asteroidPrefab;

    //maxlimit of where you want to spawn
    private float SpawnX = 350;
    private float SpawnZ = 300;
    private float SpawnY = 50;

    //how large you want the largest scale of asteroid
    private float scaleMax = 8;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnAsteroid", 1f, 1f);
        InvokeRepeating("SpawnAsteroid", 1f, .9f);
        InvokeRepeating("SpawnAsteroid", 1f, .85f);
        InvokeRepeating("SpawnAsteroid", 1f, 1.3f);
        InvokeRepeating("SpawnAsteroid", 1f, 1.2f);
        InvokeRepeating("SpawnAsteroid", 1f, 1.6f);

    }

    // Update is called once per frame
    void Update()
    {

    }
    void SpawnAsteroid()
    {
        float randomXspawn = Random.Range(-SpawnX, SpawnX);
        float randomZspawn = Random.Range(-SpawnZ, SpawnZ);
        float scale = Random.Range(1, scaleMax);
        int asteroidPrefabIndex = Random.Range(0, asteroidPrefab.Length);
        Vector3 randPos = new Vector3(randomXspawn, SpawnY, randomZspawn);
        GameObject asteroid = Instantiate(asteroidPrefab[asteroidPrefabIndex], randPos, asteroidPrefab[asteroidPrefabIndex].transform.rotation);
        asteroid.transform.localScale = new Vector3(scale, scale, scale);

        //destroy after 10 seconds
        Destroy(asteroid.gameObject, 10f);
    }

    //when asteroid collides with other objects asteroid is destroyed
    private void OnTriggerEnter(Collider other)
    {
        //destroy asteroid if hits other objects
        Destroy(gameObject);
    }
}
