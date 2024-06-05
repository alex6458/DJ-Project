using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleTail : TailBase
{
    public float explosionRange = 1f;
    public float explosionDamage = 5f;
    public GameObject whaleExplosionPrefab = null;
    public Vector3 explosionOffset = new Vector3 (0f, 0f, -0.1f);

    public override IEnumerator tailEffect(GameObject target)
    {
        if (whaleExplosionPrefab == null)
        {
            Debug.Log("No whaleExplosionPrefab found");
            yield break;
        }

        GameObject explosionGameObject = Instantiate(whaleExplosionPrefab, target.transform.position + explosionOffset, Quaternion.identity);

        Debug.Log($"Exploding around {target.name}");

        yield return new WaitForSeconds(0.5f);

        GameObject[] targetCandidates = GameObject.FindGameObjectsWithTag(target.tag);

        foreach (var candidate in targetCandidates)
        {
            var targetScript = candidate.GetComponent<Health>();
            if (targetScript == null)
            {
                Debug.Log("No target script found");
                continue;
            }

            if(Vector3.Distance(target.transform.position, candidate.transform.position) <= explosionRange)
            {
                targetScript.TakeDamage(explosionDamage);
            }
        }

        Destroy(explosionGameObject);
    }
}
