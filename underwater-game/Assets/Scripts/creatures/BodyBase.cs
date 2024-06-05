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
    private AudioSource audioSource;

    public virtual IEnumerator doAttack(GameObject source, GameObject target, Func<GameObject, IEnumerator> tailEffect)
    {
        Debug.Log($"{source.name} dealing {attackDamage} damage to {target.name}");
        var targetScript = target.GetComponent<Health>();
        if(targetScript == null)
        {
            Debug.Log("No target script found");
            yield break;
        }

        audioSource.Play();
        targetScript.TakeDamage(attackDamage);
        var scriptsGameObject = GameObject.Find("_Scripts");

        if (scriptsGameObject == null)
        {
            Debug.Log("Scripts Game Object not found");
        }
        else
        {
            var scriptsOTE = scriptsGameObject.GetComponent<overTimeEffects>();
            if (scriptsOTE == null)
            {
                Debug.Log("OTE Script not found");
            }
            else
            {
                scriptsOTE.StartCoroutine(tailEffect(target));
            }
        }
    }

    // Start is called before the first frame update
    public virtual void Start()
    {

        // Get the existing AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Optional: Check if the AudioSource component exists and log a warning if it doesn't
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource component not found on the game object.");
        }
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
