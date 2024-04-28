using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyBase : MonoBehaviour
{
    public float attackRange = 100;
    public float attackSpeed = 1f;
    public float attackDamage = 1f;
    public Vector3 posOffset = Vector3.zero;

    public virtual IEnumerator doAttack(GameObject source, GameObject target, Func<GameObject, IEnumerator> tailEffect)
    {
        Debug.Log($"{source.name} dealing {attackDamage} damage to {target.name}");
        var targetScript = target.GetComponent<Health>();
        if(targetScript == null)
        {
            Debug.Log("No target script found");
            yield break;
        }

        targetScript.TakeDamage(attackDamage);
        targetScript.StartCoroutine(tailEffect(target));
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        //transform.Find("AttackRange").GetComponent<CircleCollider2D>().radius = attackRange/100;
        //if (gameObject != null)
        //{
        //    gameObject.transform.position += pos_offset;
        //}

    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }
}
