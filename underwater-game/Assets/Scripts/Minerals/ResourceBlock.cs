using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class ResourceBlock : MonoBehaviour
{

    public Mineral playerResources;
    public float Resource1 = 0f;
    public float Resource2 = 0f;
    public float Resource3 = 0f;
    public float Resource4 = 0f;
    private bool CollisionState = false;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            CollisionState = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        CollisionState = false;
    }

    private void OnMouseOver()
    {
        // right click clicked and collision detected
        if (Input.GetMouseButtonDown(1) && CollisionState) // Change 1 to 0 for left click, 2 for middle click
        {
            Debug.Log("Right clicked");
            MineBlock(); 

        }
    }


    private void MineBlock()
    {

        // Check if the Resources script was found
        if (playerResources != null)
        {
            playerResources.AddResources(Resource1, Resource2, Resource3, Resource4);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("No playerResource found");
        }

    }
}
