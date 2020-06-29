using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    private Scene currentScene;

    public void ReloadScene()
    {
        SceneManager.LoadScene(currentScene.name);
    }

    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
    }
}