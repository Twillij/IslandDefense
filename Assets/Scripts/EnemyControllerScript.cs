using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyControllerScript : MonoBehaviour
{
    public float moveSpeed;
    public float baseDamage;

    private GameObject target;

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

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
        Vector3 dir = Vector3.Normalize(target.transform.position - this.transform.position); //Debug.Log(dir);

        // translate the object towards the target
        this.transform.position += dir * moveSpeed * Time.deltaTime;
    }

    private void OnValidate()
    {
        moveSpeed = Mathf.Max(0, moveSpeed);
        baseDamage = Mathf.Max(0, baseDamage);
    }

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void Update()
    {
        SeekTarget();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag(target.tag))
        {
            //Debug.Log("triggered");
        }

        Destroy(this.gameObject);
    }
}
