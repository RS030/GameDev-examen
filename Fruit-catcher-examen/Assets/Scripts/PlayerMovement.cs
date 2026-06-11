using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private GameManager gameManager;


    // movement
    private float speed = 100f;
    private float xBound = 70f;
    private float zBound = 70f;
    private Rigidbody playerRb;


    // slower
    private float normalSpeed = 100f;
    private float slowSpeed = 12f;
    private bool isSlowed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        playerRb = GetComponent<Rigidbody>();
        playerRb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isGameActive)
        {
            playerRb.linearVelocity = Vector3.zero;
            return;
        }

        PlayerMovement();
        MovementLimit();
    }

    // Left and right movement
    void PlayerMovement()
    {
        float HorizontalInput = Input.GetAxis("Horizontal");

        playerRb.linearVelocity = new Vector3(HorizontalInput * speed, playerRb.linearVelocity.y, 0);
    }

    // Player movement limit
    void MovementLimit()
    {
        Vector3 pos = transform.position;

        if (pos.x < -xBound)
        {
            pos.x = -xBound;
        }
        else if (pos.x > xBound)
        {
            pos.x = xBound;
        }
        else if (pos.z > zBound)
        {
            pos.z = zBound;
        }

        transform.position = pos;
    }

    // Detects collision and destroy
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fruit"))
        {
            FallingObject fallingObject = collision.gameObject.GetComponent<FallingObject>();

            if (fallingObject != null && fallingObject.isOnGround)
            {
                return;
            }

            gameManager.AddFruit();
            Destroy(collision.gameObject);
        }

        // Bomb

        if (collision.gameObject.CompareTag("Bomb"))
        {
            gameManager.LoseLife();
            Destroy(collision.gameObject);
        }

        // Powerup
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            FindFirstObjectByType<SpawnManager>().SpawnCleaner();
            Destroy(collision.gameObject);
        }
    }

    // Detects fruit on ground to slow player

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fruit"))
        {
            FallingObject fallingObject = other.GetComponent<FallingObject>();

            if (fallingObject != null && fallingObject.isOnGround && !isSlowed)
            {
                StartCoroutine(SlowPlayer());
            }
        }
    }

    // Slow player function

    private System.Collections.IEnumerator SlowPlayer()
    {
        speed = slowSpeed;

        yield return new WaitForSeconds(1f);

        speed = normalSpeed;
    }

}
