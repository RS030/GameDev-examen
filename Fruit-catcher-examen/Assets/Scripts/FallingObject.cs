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
                    fruitRb.useGravity = false;
                    fruitRb.isKinematic = true;
                }

                Collider fruitCollider = GetComponent<Collider>();

                if (fruitCollider != null)
                {
                    fruitCollider.isTrigger = true;
                }

                Destroy(gameObject, 3f);
            }

            if (gameObject.CompareTag("Bomb"))
            {
                Destroy(gameObject);
            }

            if (gameObject.CompareTag("PowerUp"))
            {
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.CompareTag("Fruit"))
        {
            if (gameObject.CompareTag("Fruit"))
            {
                Destroy(gameObject);
            }


            if (gameObject.CompareTag("Bomb"))
            {
                Destroy(gameObject);
            }

            if (gameObject.CompareTag("PowerUp"))
            {
                Destroy(collision.gameObject);
            }
        }
    }


}
