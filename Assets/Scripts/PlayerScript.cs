using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int maxLife = 3;

    private int life;

    public void ChangeLife(int change)
    {
        life += change;

        if (life <= 0)
        {
            FindObjectOfType<GameManagerScript>().GameOver();
        }
    }

    private void OnValidate()
    {
        maxLife = Mathf.Max(0, maxLife);
    }

    private void Start()
    {
        this.transform.tag = "Player";
        life = maxLife;
    }
}