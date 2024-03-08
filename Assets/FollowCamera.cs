using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;  // Reference to the car's transform
    public Vector3 offset = new Vector3(0f, 5f, -10f);  // Offset to position the camera relative to the car
    public float smoothSpeed = 1f;  // Smoothing factor for camera movement

    void LateUpdate()
    {
        // Check if the target (car) is assigned
        if (target != null)
        {
            // Calculate the desired position based on the car's position and rotation
            Vector3 desiredPosition = target.position + target.TransformDirection(offset);

            // Smoothly move the camera towards the desired position
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Look at the target
            transform.LookAt(target);
        }
    }
}