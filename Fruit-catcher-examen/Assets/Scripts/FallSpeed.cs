using UnityEngine;

public class FallSpeed : MonoBehaviour
{
    public static bool isSlowFalling = false;

    public float normalFallSpeed = 25f;
    public float slowFallSpeed = 8f;

    private Rigidbody rb;
    private FallingObject fallingObject;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fallingObject = GetComponent<FallingObject>();
    }

    void FixedUpdate()
    {
        if (rb == null)
        {
            return;
        }

        if (fallingObject != null && fallingObject.isOnGround)
        {
            return;
        }

        float currentSpeed = normalFallSpeed;

        if (isSlowFalling)
        {
            currentSpeed = slowFallSpeed;
        }

        rb.linearVelocity = new Vector3(
            rb.linearVelocity.x,
            -currentSpeed,
            rb.linearVelocity.z
        );
    }
}