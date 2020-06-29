using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyControllerScript : MonoBehaviour
{
    public GameObject target;
    public float moveSpeed;
    public float baseDamage;

    private Collider col;

    public void SeekTarget()
    {
        if (target == null)
        {
            Debug.LogError("The enemy has no target to seek.");
            return;
        }

        // look at the target
        this.transform.LookAt(target.transform);

        // calculate the direction from itself to the target
        Vector3 dir = Vector3.Normalize(target.transform.position - this.transform.position);

        // translate the object towards the target
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }

    private void OnValidate()
    {
        moveSpeed = Mathf.Max(0, moveSpeed);
        baseDamage = Mathf.Max(0, baseDamage);
    }

    private void Start()
    {
        col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    private void Update()
    {
        SeekTarget();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(target.tag))
        {
            // deal damage to island/tower
            
        }

        Destroy(this.gameObject);
    }
}
