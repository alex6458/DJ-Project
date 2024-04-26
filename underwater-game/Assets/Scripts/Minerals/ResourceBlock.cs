using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class ResourceBlock : MonoBehaviour
{

    public Mineral playerResources;
    public Camera myCamera;
    public float Resource1 = 0f;
    public float Resource2 = 0f;
    public float Resource3 = 0f;
    public float Resource4 = 0f;


    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1)) // Change 1 to 0 for left click, 2 for middle click
        {
            Debug.Log("Right clicked");
            MineBlock(); // Call your function here

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
