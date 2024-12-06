using UnityEngine;
// Camera movement
// Wanted to comment on how fricken nicely done this is btw ~ kenzie
public class CameraFollow : MonoBehaviour
{
    public Transform player1; // Reference to the first player
    public Transform player2; // Reference to the second player
    public Vector3 offset; // Offset distance between the camera and the midpoint of the players
    public float minDistance = 5f; // Minimum distance the camera will maintain from the players
    public float maxDistance = 15f; // Maximum distance the camera will maintain from the players
    public float zoomSpeed = 10f; // Speed at which the camera zooms

    void LateUpdate()
    {
        if (player1 == null || player2 == null) return;

        // Calculate the midpoint between the two players
        Vector3 midpoint = (player1.position + player2.position) / 2;

        // Calculate the distance between the two players
        float distance = Vector3.Distance(player1.position, player2.position);

        // Adjust the camera's position
        Vector3 targetPosition = midpoint + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * zoomSpeed);

        // Adjust the camera's field of view (zoom) based on the distance between players
        Camera camera = GetComponent<Camera>();
        if (camera != null)
        {
            float targetFOV = Mathf.Clamp(distance, minDistance, maxDistance);
            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
        }
    }
}
