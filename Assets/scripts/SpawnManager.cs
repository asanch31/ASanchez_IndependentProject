using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    //public GameObject powerUpPrefab;

    private float spawnRange = 8.5f;
    private int enemyCount;
    private int waveNum = 1;
    public int maxWaves = 1;



    // Start is called before the first frame update
    void Start()
    {
        SpawnWave(waveNum);
    }

    void SpawnWave(int enemyNum)
    {

        //Instantiate(powerUpPrefab, GenerateSpawnPosition(), powerUpPrefab.transform.rotation);
        for (int i = 0; i < enemyNum; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }
    Vector3 GenerateSpawnPosition()
    {
        float xPos = Random.Range(-spawnRange, spawnRange);
        float zPos = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPos = new Vector3(transform.position.x + xPos, transform.position.y, transform.position.z + zPos);
        return spawnPos;
    }

    // Update is called once per frame
    void Update()
    {

        enemyCount = FindObjectsOfType<MinionStats>().Length;
        if (enemyCount == 0 && waveNum < maxWaves)
        {
            waveNum++;
            SpawnWave(waveNum);
        }
    }
}
