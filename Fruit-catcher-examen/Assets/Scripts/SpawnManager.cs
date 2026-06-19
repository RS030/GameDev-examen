using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameManager gameManager;

    public GameObject[] fruitPrefabs;
    public GameObject bombPrefab;
    public GameObject powerupPrefab;

    private float xSpawnRange = 70f;
    private float ySpawn = 115f;
    private float zSpawn = -7f;

    private float fruitSpawnTime = 2f;
    private float minimumFruitSpawnTime = 0.1f;
    private float spawnSpeedIncrease = 0.03f;

    private float bombSpawnTime = 1f;
    private float powerupSpawnTime = 20f;
    private float startDelay = 2f;


    public GameObject cleanerPrefab;

    private float cleanerSpawnX = -103f;
    private float cleanerSpawnY = 3f;
    private float cleanerSpawnZ = -7f;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    public void StartSpawning()
    {
        CancelInvoke();

        Invoke("SpawnRandomFruit", startDelay);

        InvokeRepeating("SpawnBomb", startDelay + 5f, bombSpawnTime);

        // after 30 sec spawn twice as much bombs
        InvokeRepeating("SpawnBomb", startDelay + 30f, bombSpawnTime);

        InvokeRepeating("SpawnPowerup", startDelay + 30f, powerupSpawnTime);
    }

    void SpawnRandomFruit()
    {
        if (!gameManager.isGameActive)
        {
            return;
        }

        if (FallSpeed.isSlowFalling)
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

        if (FallSpeed.isSlowFalling)
        {
            return;
        }

        float randomX = Random.Range(-xSpawnRange, xSpawnRange);

        Vector3 spawnPosition = new Vector3(randomX, 60f, zSpawn);

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

        if (FallSpeed.isSlowFalling)
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