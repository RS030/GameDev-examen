using UnityEngine;

public class FallingObject : MonoBehaviour
{

    public bool isOnGround = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !isOnGround)
        {
            isOnGround = true;

            if (gameObject.CompareTag("Fruit"))
            {
                Rigidbody fruitRb = GetComponent<Rigidbody>();

                if (fruitRb != null)
                {
                    fruitRb.linearVelocity = Vector3.zero;
                    fruitRb.angularVelocity = Vector3.zero;
                }

                Destroy(gameObject, 3f);
            }

            if (gameObject.CompareTag("Bomb"))
            {
                Destroy(gameObject);
            }
        }
    }

    
}
