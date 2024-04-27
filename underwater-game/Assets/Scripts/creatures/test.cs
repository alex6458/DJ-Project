using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject coroutineHost = null;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        if (coroutineHost != null)
        {
            coroutineHost.GetComponent<CoroutineManager>().StartCoroutine(test_ie(1f));
            coroutineHost.GetComponent<CoroutineManager>().StartCoroutine(test_ie(2f));
        }
        Debug.Log("End");
        
            
    }

    public IEnumerator test_ie(float secs)
    {
        for (int i = 0; i < 10; i++)
        {
            Debug.Log("Before " + i + " " + secs);
            yield return new WaitForSeconds(secs);
            Debug.Log("After " + i + " " + secs);
        }
    } 

    // Update is called once per frame
    void Update()
    {
        
    }
}
