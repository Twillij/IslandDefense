using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public ScoreManagerScript scoreManagerScript;

    public Text scoreNumber;

    private void Update()
    {
        scoreNumber.text = scoreManagerScript.score.ToString();
    }
}
