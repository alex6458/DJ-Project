using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CaptureTower : MonoBehaviour
{
    public GameObject capturedCreature = null;
    public float towerRange = 5f;
    public Vector3 creaturePosOffset = new Vector3 (0.1f, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (capturedCreature == null)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                var enemyHealthScript = enemy.GetComponent<Health>();
                if (enemyHealthScript != null && enemyHealthScript.currentHealth < 0.5f * enemyHealthScript.maxHealth 
                    && Vector3.Distance(transform.position, enemy.transform.position) < towerRange)
                {
                    capturedCreature = enemy;
                    capturedCreature.transform.position = transform.position;
                    capturedCreature.transform.localScale = new Vector3(-capturedCreature.transform.localScale.x, capturedCreature.transform.localScale.y, capturedCreature.transform.localScale.z);

                    var capturedCreatureScript = capturedCreature.GetComponent<CreatureBase>();
                    if (capturedCreatureScript == null)
                    {
                        Debug.Log("No enemy base creature script found");
                        continue;
                    }

                    capturedCreatureScript.moveSpeed = 0;
                    capturedCreatureScript.targetTag = "Enemy";
                    capturedCreatureScript.targetTag2 = "";
                    capturedCreature.tag = "Untagged";

                    var capturedCreatureBC = capturedCreature.GetComponent<BoxCollider2D>();
                    var capturedCreatureRB = capturedCreature.GetComponent<Rigidbody2D>();
                    if (capturedCreatureBC == null || capturedCreatureRB == null)
                    {
                        Debug.Log("Captured creature BC/RB missing");
                        continue;
                    }
                    capturedCreatureBC.enabled = false;
                    capturedCreatureRB.simulated = false;

                }
            }
        }
        else
        {
            var healthScript = GetComponent<Health>();
            if (healthScript == null)
            {
                Debug.Log("No healthScript found");
                return;
            }

            if (healthScript.currentHealth <= 0f)
            {
                Destroy(capturedCreature);
                Destroy(gameObject);
            }


            var capturedHealthScript = GetComponent<Health>();
            if (capturedHealthScript == null)
            {
                Debug.Log("No captured creature healthScript found");
                return;
            }

            if (healthScript.stunned != capturedHealthScript.stunned)
            {
                capturedHealthScript.stunned = healthScript.stunned;
            }
        }
    }
}
