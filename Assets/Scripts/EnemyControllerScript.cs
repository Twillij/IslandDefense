using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyControllerScript : MonoBehaviour
{
    public float hp = 100;
    public float moveSpeed = 10;
    public float baseDamage = 0;
    public float scoreValue = 0;

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
        Vector3 dir = Vector3.Normalize(target.transform.position - this.transform.position);

        // translate the object towards the target
        this.transform.position += dir * moveSpeed * Time.deltaTime;
    }

    public void ShotDown()
    {
        ScoreManagerScript scoreKeeper = FindObjectOfType<ScoreManagerScript>();
        scoreKeeper.score += scoreValue;
        Destroy(this.gameObject);
    }

    private void OnValidate()
    {
        hp = Mathf.Max(0, hp);
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

        if (hp < 0)
            ShotDown();
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
