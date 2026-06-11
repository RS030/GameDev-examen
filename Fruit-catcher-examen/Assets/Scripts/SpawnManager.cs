using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameManager gameManager;
    
    public GameObject[] fruitPrefabs;
    public GameObject bombPrefab;
    public GameObject powerupPrefab;

    private float xSpawnRange = 70f;
    private float ySpawn = 115f;
    private float zSpawn = -5f;

    private float fruitSpawnTime = 2f;
    private float minimumFruitSpawnTime = 0.6f;
    private float spawnSpeedIncrease = 0.05f;

    public float bombSpawnTime = 2f;
    public float powerupSpawnTime = 10f;
    private float startDelay = 2f;


    public GameObject cleanerPrefab;

    private float cleanerSpawnX = -103f;
    private float cleanerSpawnY = 3f;
    private float cleanerSpawnZ = -5f;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();

        Invoke("SpawnRandomFruit", startDelay);
        InvokeRepeating("SpawnBomb", startDelay + 2f, bombSpawnTime);
        InvokeRepeating("SpawnPowerup", startDelay + 30f, powerupSpawnTime);
    }

    void SpawnRandomFruit()
    {
        if (!gameManager.isGameActive)
        {
            Invoke("SpawnRandomFruit", fruitSpawnTime);
            return;
        }

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
        if (!gameManager.isGameActive)
        {
            return;
        }

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

    void SpawnPowerup()
    {
        if (!gameManager.isGameActive)
        {
            return;
        }

        float randomX = Random.Range(-xSpawnRange, xSpawnRange);

        Vector3 spawnPosition = new Vector3(
            randomX,
            80f,
            zSpawn
        );

        GameObject powerup = Instantiate(
            powerupPrefab,
            spawnPosition,
            powerupPrefab.transform.rotation
        );

        Rigidbody powerupRb = powerup.GetComponent<Rigidbody>();

        if (powerupRb != null)
        {
            powerupRb.linearVelocity = Vector3.down * 25f;
        }
    }

    public void SpawnCleaner()
    {
        Vector3 spawnPosition = new Vector3(
            cleanerSpawnX,
            cleanerSpawnY,
            cleanerSpawnZ
        );

        Instantiate(
            cleanerPrefab,
            spawnPosition,
            cleanerPrefab.transform.rotation
        );
    }
}