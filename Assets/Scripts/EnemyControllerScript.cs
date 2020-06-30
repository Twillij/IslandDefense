using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyControllerScript : MonoBehaviour
{
    public float moveSpeed = 10;
    public float baseDamage = 0;
    public float scoreValue = 0;

    public GameObject deathEffect;

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

    public void ShotDown()
    {
        ScoreManagerScript scoreKeeper = (ScoreManagerScript)FindObjectOfType(typeof(ScoreManagerScript));
        scoreKeeper.score += scoreValue;
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
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
