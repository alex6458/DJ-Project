using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowySharkAttack : MonoBehaviour
{
    public GameObject target = null;
    public string targetTag = null;
    public Func<GameObject, IEnumerator> tailEffect = null;
    public float attackMS = 100f;
    public float attackDMG = 1f;

    bool directionSet = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); 
            return;
        }
        //if (!directionSet)
        //{
        //    var direction = (target.transform.position - transform.position).normalized;
        //    transform.rotation = Quaternion.LookRotation(direction);
        //}

        float step = attackMS / 100 * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (targetTag != null && collision.gameObject.CompareTag(targetTag))
        {
            var enemyHealthScript = collision.gameObject.GetComponent<Health>();
            if (enemyHealthScript != null)
            {
                enemyHealthScript.TakeDamage(attackDMG);
                if(tailEffect != null)
                {
                    enemyHealthScript.StartCoroutine(tailEffect(collision.gameObject));
                }
            }

            Destroy(gameObject);
        }
    }
}
