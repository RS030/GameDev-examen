using UnityEngine;

public class Cleaner : MonoBehaviour
{
    private float speed = 80f;
    private float destroyX = 100f;

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (transform.position.x > destroyX)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fruit"))
        {
            FallingObject fallingObject = other.GetComponent<FallingObject>();

            if (fallingObject != null && fallingObject.isOnGround)
            {
                Destroy(other.gameObject);
            }
        }
    }
}