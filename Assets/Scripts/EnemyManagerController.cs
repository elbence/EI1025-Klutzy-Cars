using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagerController : MonoBehaviour
{
    public Transform[] spawnPositions;
    [SerializeField]
    public GameObject enemyPrefab;
    public int maxNumberOfEnemies;
    public static int currentNumberOfEnemies = 0;
    public float spawnStart;
    public float spawnInterval;
    // Start is called before the first frame update
    void Start()
    {
        currentNumberOfEnemies = 0;
        InvokeRepeating("SpawnEnemy", spawnStart, spawnInterval);
    }

    void SpawnEnemy() {
        if (currentNumberOfEnemies < maxNumberOfEnemies) {
            int randomIndex = Random.Range(0, spawnPositions.Length);
            Transform spawnPosition = spawnPositions[randomIndex];
            Instantiate(enemyPrefab, spawnPosition.position, spawnPosition.rotation);
            IncrementNumerOfEnemies();
        }
    }

    static void IncrementNumerOfEnemies() {
        currentNumberOfEnemies++;
    }

    static void DecrementNumberOfEnemies() {
        currentNumberOfEnemies--;
    }

    public static void DeleteEnemy(GameObject enemy) {
        Destroy(enemy);
        DecrementNumberOfEnemies();
    }
}