using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    // Speed at which the object rotates towards the mouse position
    public float rotationSpeed = 5f;

    void Update()
    {
        RotateToCursor();
    }

    void RotateToCursor()
    {
        // Get the position of the mouse in screen space
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z; // z distance from the camera

        // Convert screen position to world position
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Calculate direction from the object to the mouse position
        Vector3 direction = worldPosition - transform.position;

        // Zero out the Y component to ensure rotation only on the Y axis
        direction.y = 0;

        // Check if the direction is not zero to avoid errors
        if (direction != Vector3.zero)
        {
            // Calculate the rotation needed for the object to look at the mouse
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Smoothly rotate towards the target rotation at the given speed
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, targetRotation.eulerAngles.y, 0), rotationSpeed * Time.deltaTime);
        }
    }
}
