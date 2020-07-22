using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public float levelDuration = 10;

    private bool gameIsRunning = false;
    private int level = 0;
    private float levelTimer = 0;
    private EnemySpawnerScript enemySpawner;

    public void Startevel()
    {
        level++;
        levelTimer = 10;
        enemySpawner.enemyHPMultiplier = level + (level / 10) * level;
        enemySpawner.spawnRateMultiplier = level;
        enemySpawner.timerOn = true;
        gameIsRunning = true;
    }

    public void EndLevel()
    {
        enemySpawner.timerOn = false;
        gameIsRunning = false;
    }

    public void GameOver()
    {
        // to do: show a restart button
    }

    private void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawnerScript>();
        Startevel();
    }

    private void Update()
    {
        if (gameIsRunning)
        {
            levelTimer -= Time.deltaTime;

            if (levelTimer < 0)
            {
                EndLevel();
            }
        }
    }
}