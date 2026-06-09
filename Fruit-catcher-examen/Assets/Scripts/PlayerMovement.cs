using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // movement
    private float speed = 100f;
     private float xBound = 70f;
     private float zBound = 70f;
    private Rigidbody playerRb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
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
        } else if (pos.z > zBound)
        {
            pos.z = zBound;
        }

        transform.position = pos;
    }

    // Detects collision
    private void OnCollisionEnter(Collision collision)
    {

        // With fruit
        if(collision.gameObject.CompareTag("Fruit"))
        {
            Destroy(collision.gameObject);
        }

        // With Bomb
        if(collision.gameObject.CompareTag("Bomb"))
        {
            Destroy(collision.gameObject);
        }



    }

}
