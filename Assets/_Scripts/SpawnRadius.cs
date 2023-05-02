using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRadius : MonoBehaviour
{
    public int numEnemies;
    /// <summary>
    /// 0: Enemy Red
    /// 1: Enemy Green
    /// ...
    /// </summary>
    public GameObject[] enemyPrefabs;
    public float radius;
    /// <summary>
    /// 0: time for red enemy
    /// </summary>
    [SerializeField] private float[] timeDelaySpawn;
    void Start()
    {
        StartCoroutine(spawnEnemy(timeDelaySpawn[0], enemyPrefabs[0]));
        StartCoroutine(spawnEnemy(timeDelaySpawn[1], enemyPrefabs[1]));
        StartCoroutine(spawnEnemy(timeDelaySpawn[2], enemyPrefabs[2]));
    }
  
    
    private IEnumerator spawnEnemy(float timeSpawn, GameObject enemy){
        yield return new WaitForSeconds(timeSpawn);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5), Random.Range(-6f, 6) ,0), Quaternion.identity);
        StartCoroutine(spawnEnemy(timeSpawn, enemy));
    }
   
}
