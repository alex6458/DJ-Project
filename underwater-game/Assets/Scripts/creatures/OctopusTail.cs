using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusTail : TailBase
{
    public float stunDuration = 4f;
    public GameObject octopusEffectPrefab = null;
    public Vector3 effectOffset = new Vector3 (0f, 0f, -0.5f);

    public override IEnumerator tailEffect(GameObject target)
    {
        if (octopusEffectPrefab == null)
        {
            Debug.Log("No whaleExplosionPrefab found");
            yield break;
        }

        var targetScript = target.GetComponent<Health>();
        if (targetScript == null)
        {
            Debug.Log("No target script found");
            yield break;
        }

        GameObject effectGameObject = Instantiate(octopusEffectPrefab, target.transform.position + effectOffset, Quaternion.identity);

        Debug.Log($"Stunning {target.name}");

        targetScript.stunned = true;

        yield return new WaitForSeconds(stunDuration);

        targetScript.stunned = false;

        Destroy(effectGameObject);
    }
}
