﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject target;
    public GameObject enemy;
    public float enemyBaseHP = 100;
    public float enemyHPMultiplier = 1;
    public float innerSpawnRadius = 20;
    public float outerSpawnRadius = 30;

    public bool timerOn = true;
    public float baseSpawnRate = 1.15f;
    public float spawnRateMultiplier = 1;
    public float timeInterval = 1;

    private float timer;
    private float spawnRate = 0;

    public void SpawnEnemy()
    {
        float[] sign = { -1, 1 }; // for randomizing whether a value is positive or negative
        float inRadSqr = innerSpawnRadius * innerSpawnRadius; // square of the inner radius
        float outRadSqr = outerSpawnRadius * outerSpawnRadius; // square of the outer radius

        // create a random distance to the target
        float distanceSqr = Random.Range(inRadSqr, outRadSqr);

        // use that distance to figure out the coordinate values
        float posXSqr = Random.Range(0, distanceSqr);
        float posZSqr = distanceSqr - posXSqr;

        // calculate the actual position
        Vector3 spawnPos;
        spawnPos.x = Mathf.Sqrt(posXSqr) * sign[Random.Range(0, sign.Length)];
        spawnPos.y = this.transform.position.y;
        spawnPos.z = Mathf.Sqrt(posZSqr) * sign[Random.Range(0, sign.Length)];

        // spawn the enemy and set its position
        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = spawnPos;
        newEnemy.GetComponent<EnemyControllerScript>().hp = enemyBaseHP * enemyHPMultiplier;
        newEnemy.GetComponent<EnemyControllerScript>().SetTarget(target);
        newEnemy.transform.SetParent(this.transform);
    }

    private void OnValidate()
    {
        enemyBaseHP = Mathf.Max(0, enemyBaseHP);
        enemyHPMultiplier = Mathf.Max(0, enemyHPMultiplier);
        innerSpawnRadius = Mathf.Max(0, innerSpawnRadius);
        outerSpawnRadius = Mathf.Max(innerSpawnRadius, outerSpawnRadius);
        timeInterval = Mathf.Max(0, timeInterval);
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
                spawnRate += baseSpawnRate * spawnRateMultiplier;

                for (int i = 0; i < (int)spawnRate; ++i)
                    SpawnEnemy();

                spawnRate -= (int)spawnRate;

                timer = timeInterval;
            }
        }
    }
}
