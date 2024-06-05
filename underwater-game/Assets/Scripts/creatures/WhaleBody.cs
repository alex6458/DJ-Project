using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleBody : BodyBase
{
    public GameObject whaleAttackPrefab = null;

    public override IEnumerator doAttack(GameObject source, GameObject target, Func<GameObject, IEnumerator> tailEffect)
    {
        if (source == null)
        {
            Debug.Log("No source found");
            yield break;
        }

        if (target == null)
        {
            Debug.Log("No target found");
            yield break;
        }

        Debug.Log($"{source.name} dealing {attackDamage} damage to {target.name}");
        var targetScript = target.GetComponent<Health>();
        if (targetScript == null)
        {
            Debug.Log("No target script found");
            yield break;
        }

        if(whaleAttackPrefab == null)
        {
            Debug.Log("No whaleAttackPrefab found");
            yield break;
        }

        GameObject whaleAttack;
        if (target.tag == "Enemy")
        {
            whaleAttack = Instantiate(whaleAttackPrefab, source.transform.position + new Vector3(1f, 0, 0), Quaternion.identity);
            whaleAttack.transform.Rotate(0f, 180f, 0f);
        }
        else
        {
            whaleAttack = Instantiate(whaleAttackPrefab, source.transform.position + new Vector3(-1f, 0, 0), Quaternion.identity);
        }
        var waScript = whaleAttack.GetComponent<WhaleAttack>();
        if (waScript == null)
        {
            Debug.Log("No WhaleAttack script found");
            yield break;
        }

        waScript.target = target;
        waScript.tailEffect = tailEffect;
        waScript.targetTag = target.tag;
    }
}
