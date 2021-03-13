using UnityEngine;

public class asteroidRandom : MonoBehaviour

{
    public GameObject[] asteroidPrefab;
    private float SpawnX = 50;
    private float SpawnZ = 50;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnAsteroid", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SpawnAsteroid()
    {
        float randomXspawn = Random.Range(-SpawnX, SpawnX);
        float randomZspawn = Random.Range(-SpawnZ, SpawnZ);
        int asteroidPrefabIndex = Random.Range(0, asteroidPrefab.Length);
        Vector3 randPos = new Vector3(randomXspawn, 20, randomZspawn);
        GameObject asteroid = Instantiate(asteroidPrefab[asteroidPrefabIndex], randPos, asteroidPrefab[asteroidPrefabIndex].transform.rotation);
    }
    private void OnTriggerEnter(Collider other)
    {

        Destroy(gameObject);
    }
}
