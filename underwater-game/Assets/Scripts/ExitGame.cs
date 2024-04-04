using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExitGame : MonoBehaviour
{
    public void Exit()
    {
        UnityEngine.Application.Quit();
        Debug.Log("Exiting");
    }
}
