using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] fruitPrefabs;
    public GameObject bombPrefab;

    private float xSpawnRange = 70f;
    private float ySpawn = 115f;
    private float zSpawn = -5f;

    private float fruitSpawnTime = 2f;
    private float minimumFruitSpawnTime = 0.6f;
    private float spawnSpeedIncrease = 0.01f;

    private float bombSpawnTime = 2f;
    private float startDelay = 2f;

    void Start()
    {
        Invoke("SpawnRandomFruit", startDelay);
        InvokeRepeating("SpawnBomb", startDelay + 2f, bombSpawnTime);
    }

    void SpawnRandomFruit()
    {
        int randomIndex = Random.Range(0, fruitPrefabs.Length);
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);

        Vector3 spawnPosition = new Vector3(
            randomX,
            ySpawn,
            zSpawn
        );

        Instantiate(
            fruitPrefabs[randomIndex],
            spawnPosition,
            fruitPrefabs[randomIndex].transform.rotation
        );

        fruitSpawnTime -= spawnSpeedIncrease;
        fruitSpawnTime = Mathf.Max(fruitSpawnTime, minimumFruitSpawnTime);

        Invoke("SpawnRandomFruit", fruitSpawnTime);
    }

    void SpawnBomb()
    {
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);

        Vector3 spawnPosition = new Vector3(
            randomX,
            60f,
            zSpawn
        );

        GameObject bomb = Instantiate(
            bombPrefab,
            spawnPosition,
            bombPrefab.transform.rotation
        );

        Rigidbody bombRb = bomb.GetComponent<Rigidbody>();

        if (bombRb != null)
        {
            bombRb.linearVelocity = Vector3.down * 25f;
        }
    }
}