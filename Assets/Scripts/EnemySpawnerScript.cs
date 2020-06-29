using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject target;
    public GameObject enemy;
    public float innerSpawnRadius = 20;
    public float outerSpawnRadius = 30;

    public bool timerOn = true;
    public float timeInterval = 1;
    public int spawnCount = 1;

    private float timer;

    public void SpawnEnemy()
    {
        // set the spawn position
        Vector3 spawnPos;
        spawnPos.x = Random.Range(innerSpawnRadius, outerSpawnRadius);
        spawnPos.y = this.transform.position.y;
        spawnPos.z = Random.Range(innerSpawnRadius, outerSpawnRadius);

        // spawn the enemy and set its position
        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = spawnPos;
    }

    private void OnValidate()
    {
        innerSpawnRadius = Mathf.Max(0, innerSpawnRadius);
        outerSpawnRadius = Mathf.Max(innerSpawnRadius, outerSpawnRadius);
        timeInterval = Mathf.Max(0, timeInterval);
        spawnCount = Mathf.Max(0, spawnCount);
    }

    private void Start()
    {
        timer = timeInterval;
    }

    private void Update()
    {
        if (timerOn)
        {
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                for (int i = 0; i < spawnCount; ++i)
                    SpawnEnemy();

                timer = timeInterval;
            }
        }
    }
}
