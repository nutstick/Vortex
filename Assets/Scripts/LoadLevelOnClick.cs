using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevelOnClick : MonoBehaviour
{
    public string level;
    private LevelController levelController;

    // Use this for initialization
    void Awake()
    {
        try
        {
            levelController = GameObject.Find("LevelController").GetComponent<LevelController>();
        }
        catch
        {
            throw new Exception("No level controller found.");
        }
    }

    public void LoadByIndex(int sceneIndex)
    {
        if (level != null)
        {
            levelController.Level = level;
        }
        SceneManager.LoadScene(sceneIndex);
    }
}
