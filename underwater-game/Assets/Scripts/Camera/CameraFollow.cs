using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset; 

    void Update()
    {
        if (player != null)
        {
            // Update camera position to follow the player with the specified offset
            transform.position = new Vector3(player.position.x + offset.x, transform.position.y, offset.z);
        }
    }
}
