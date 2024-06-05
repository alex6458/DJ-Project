using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowySharkBody : BodyBase
{
    public GameObject glowySharkAttackPrefab = null;

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

        if(glowySharkAttackPrefab == null)
        {
            Debug.Log("No glowySharkAttackPrefab found");
            yield break;
        }

        GameObject glowySharkAttack;
        if (target.tag == "Enemy")
        {
            glowySharkAttack = Instantiate(glowySharkAttackPrefab, source.transform.position + new Vector3(1f, 0, 0), Quaternion.identity);
            glowySharkAttack.transform.Rotate(0f, 180f, 0f);
        }
        else
        {
            glowySharkAttack = Instantiate(glowySharkAttackPrefab, source.transform.position + new Vector3(-1f, 0, 0), Quaternion.identity);
        }

        var gsaScript = glowySharkAttack.GetComponent<GlowySharkAttack>();
        if (gsaScript == null)
        {
            Debug.Log("No GlowySharkAttack script found");
            yield break;
        }

        gsaScript.target = target;
        gsaScript.tailEffect = tailEffect;
        gsaScript.targetTag = target.tag;
    }
}
