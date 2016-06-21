using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemy;
    public GameObject spawnedEnemy;
    public float timer;
    public float spawnTimeAfterDeath;
    public int enemyCount;
    public int enemyLimit;
    public float minSpawnTime;
    public float maxSpawnTime;

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        if (timer >= spawnTimeAfterDeath && enemyCount < enemyLimit)
        {
            SpawnEnemy ();
            timer = 0;
            enemyCount ++;
        }
	}

    public void SpawnEnemy ()
    {
        spawnedEnemy = (GameObject)Instantiate(enemy, transform.position, Quaternion.identity);
        spawnedEnemy.GetComponent<EnemyBehaviour>().enemySpawner = gameObject;
        spawnTimeAfterDeath = (Random.Range(minSpawnTime, maxSpawnTime));
    }
}
