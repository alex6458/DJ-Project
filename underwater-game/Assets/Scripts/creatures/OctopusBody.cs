using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusBody : BodyBase
{
    public GameObject octopusAttackPrefab = null;
    public Vector3 attackOffset = new Vector3 (-1f, 0f, 0f);

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

        if(octopusAttackPrefab == null)
        {
            Debug.Log("No whaleAttackPrefab found");
            yield break;
        }
        if (target.tag == "Enemy") attackOffset.x *= -1;
        if (target.tag == "Enemy") Debug.Log("AAAAAAAAAAAAAA" + attackOffset);
        var octopusAttack = Instantiate(octopusAttackPrefab, source.transform.position + attackOffset, Quaternion.identity);
        if (target.tag == "Enemy") octopusAttack.transform.Rotate(0f, 180f, 0f);
        var oaScript = octopusAttack.GetComponent<OctopusAttack>();
        if (oaScript == null)
        {
            Debug.Log("No WhaleAttack script found");
            yield break;
        }

        oaScript.target = target;
        oaScript.tailEffect = tailEffect;
        oaScript.targetTag = target.tag;
    }
}
