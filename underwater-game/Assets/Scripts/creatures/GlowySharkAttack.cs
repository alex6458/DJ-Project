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

    private AudioSource audioSource;

    bool directionSet = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get the existing AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Optional: Check if the AudioSource component exists and log a warning if it doesn't
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource component not found on the game object.");
        }
        else
        {
            audioSource.Play();
        }

        if (target == null)
        {
            Destroy(gameObject);
            return;
        }


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

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Range attack detected collision");
        if (targetTag != null && collision.gameObject.CompareTag(targetTag))
        {
            Debug.Log("RANGE ATTACK DOING DAMAGE");
            var enemyHealthScript = collision.gameObject.GetComponent<Health>();
            if (enemyHealthScript != null)
            {
                enemyHealthScript.TakeDamage(attackDMG);
                if(tailEffect != null)
                {
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
                            scriptsOTE.StartCoroutine(tailEffect(collision.gameObject));
                        }
                    }
                    
                }

                Destroy(gameObject);
            }
        }
    }
}
