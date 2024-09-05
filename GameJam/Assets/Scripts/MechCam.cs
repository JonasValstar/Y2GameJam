using UnityEngine;

public class MechCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;

    public GameObject basicCam;

    [HideInInspector] public CameraStyle currentStyle;
    public enum CameraStyle
    {
        Basic,
    }

    private void Update()
    {
        //rotate orientation
        Vector3 viewDir = player.position - transform.position;
        viewDir.y = 0f;
        orientation.forward = viewDir.normalized;

        //rotate player object
        if (currentStyle == CameraStyle.Basic)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (inputDir != Vector3.zero)
            {
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            }
        }
    }
}