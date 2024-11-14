using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player
    public Vector3 offset; // Offset distance between the camera and the player

    void Start()
    {
        // Set an initial offset if not specified
        if (offset == Vector3.zero)
        {
            offset = transform.position - player.position;
        }
    }

    void LateUpdate()
    {
        // Update the camera's position to follow the player
        transform.position = player.position + offset;
    }
}
