using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailBase : MonoBehaviour
{
    public Vector3 posOffset = Vector3.zero;
    public float moveSpeed = 100f;

    public virtual IEnumerator tailEffect(GameObject target)
    {
        yield return null;
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        //if(gameObject != null)
        //{
        //    gameObject.transform.position += pos_offset;
        //}
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }
}
