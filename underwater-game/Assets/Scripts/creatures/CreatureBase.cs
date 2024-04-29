using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureBase : MonoBehaviour
{
    public GameObject body = null;
    public GameObject tail = null;
    BodyBase bodyScript = null;
    TailBase tailScript = null;
    float attackRange = -1f;
    float attackSpeed = -1f;
    float moveSpeed = -1f;
    float size = 0.1f;
    //Vector3 bodyOffset = Vector3.zero;
    //Vector3 tailOffset = Vector3.zero;
    Func<GameObject, GameObject, Func<GameObject, IEnumerator>, IEnumerator> doAttack = null;
    Func<GameObject, IEnumerator> tailEffect = null;

    public string targetTag = "Base";

    float currTime;
    float lastAttackTime;

    GameObject lastTarget = null;


    // Start is called before the first frame update
    public virtual void Start()
    {
        if (body == null)
        {
            Debug.Log("Missing Body");
            return;
        }
        if (tail == null)
        {
            Debug.Log("Missing Tail");
            return;
        }

        bodyScript = body.GetComponent<BodyBase>();
        if (bodyScript == null)
        {
            Debug.Log("Missing Body Script");
            return;
        }

        tailScript = tail.GetComponent<TailBase>();
        if (tailScript == null)
        {
            Debug.Log("Missing Tail Script");
            return;
        }

        attackRange = bodyScript.attackRange;
        moveSpeed = tailScript.moveSpeed;
        attackSpeed = bodyScript.attackSpeed;
        //bodyOffset = bodyScript.posOffset;
        //tailOffset = tailScript.posOffset;
        doAttack = bodyScript.doAttack;
        tailEffect = tailScript.tailEffect;

        currTime = 0f;
        lastAttackTime = 0f;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        currTime += Time.deltaTime;

        GameObject[] targetCandidates = GameObject.FindGameObjectsWithTag(targetTag);
        if(targetCandidates == null)
        {
            Debug.Log("No target candidates found");
            return;
        }

        GameObject closestTarget = null;
        
        float minDist = float.MaxValue;
        float dist;
        foreach(var tc in targetCandidates)
        {
            dist = Vector3.Distance(transform.position, tc.transform.position);
            if (dist < minDist)
            {
                closestTarget = tc;
                minDist = dist;
            }
        }

        if (closestTarget != null)
        {
            //Debug.Log($"Closest target at pos {closestTarget.transform.position}");

            if (minDist < attackRange / 100)
            {
                if (currTime - lastAttackTime > 1 / attackSpeed)
                {
                    StartCoroutine(doAttack(gameObject, closestTarget, tailEffect));
                    lastAttackTime = currTime;
                }
            }
            else
            {
                float step = moveSpeed / 100 * Time.deltaTime;

                transform.position = Vector3.MoveTowards(transform.position, closestTarget.transform.position, step);
            }
        }
    }
}
