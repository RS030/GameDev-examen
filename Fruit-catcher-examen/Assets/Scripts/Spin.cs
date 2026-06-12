using UnityEngine;

public class PowerUpSpin : MonoBehaviour
{
    private float rotateSpeed = 180f;

    void Update()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
    }
}