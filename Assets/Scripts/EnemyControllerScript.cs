using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyControllerScript : MonoBehaviour
{
    public GameObject deathEffect;
    public float moveSpeed = 10;
    public float rotateSpeed = 1;
    public float baseDamage = 0;
    public float scoreValue = 0;

    public float hp { get; set; }

    private GameObject target;

    public void TakeDamage(float damage)
    {
        hp -= damage;

        if (hp <= 0)
            ShotDown();
    }

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
        //this.transform.LookAt(target.transform);

        // calculate the direction from itself to the target
        Vector3 dir = Vector3.Normalize(target.transform.position - this.transform.position);

        // translate the object towards the target
        this.transform.position += dir * moveSpeed * Time.deltaTime;

        Vector2 perp = Vector2.Perpendicular(new Vector2(dir.x, dir.z));
        Vector3 rollAxis = new Vector3(perp.x, 0, perp.y);
        transform.Rotate(rollAxis, rotateSpeed);
    }

    public void ShotDown()
    {
        ScoreManagerScript scoreKeeper = FindObjectOfType<ScoreManagerScript>();
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
        if (other.transform.root.CompareTag("Player"))
        {
            target.GetComponent<PlayerScript>().ChangeLife(-1);
            Destroy(this.gameObject);
        }
    }
}
