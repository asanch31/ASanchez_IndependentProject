using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    //public GameObject powerUpPrefab;

    private float spawnRange = 6f;
    private int enemyCount;
    public int waveNum = 0;
    public int maxWaves;
    public bool waveCompleted= false;
   

    private BossStats boss;

    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.Find("Boss").GetComponent<BossStats>();
        
        
        enemyCount = FindObjectsOfType<MinionStats>().Length;
        
    }

    void SpawnWave(int enemyNum)
    {
        
            
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


        if (enemyCount == 0 && waveNum <= maxWaves )
        {
            boss.LoseHealth();
            waveNum++;
            SpawnWave(waveNum);
            
          
        }

        
        
    }
}
