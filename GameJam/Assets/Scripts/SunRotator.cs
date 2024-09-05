using UnityEngine;

public class SunRotator : MonoBehaviour
{
    // Rotation speed of the sun (degrees per second)
    public float rotationSpeed = 10f;

    void Update()
    {
        RotateSun();
    }

    void RotateSun()
    {
        // Rotate around the X-axis to simulate the sun moving across the sky
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }
}
