using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeEyedFishTail : TailBase
{
    public override IEnumerator tailEffect(GameObject target)
    {
        var targetScript = target.GetComponent<Health>();
        if (targetScript == null)
        {
            Debug.Log("No target script found");
            yield break;
        }

        Debug.Log($"Poisoning {target.name}");

        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1f);
            targetScript.TakeDamage(0.5f);
        }
    }
}
