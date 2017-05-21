using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextLevel : MonoBehaviour {

    private LevelController levelController;

    // Use this for initialization
    void Awake ()
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
	
    public void goToNextLevel()
    {
        string[] s = levelController.Level.Split('-');
        int nextSubLevel = int.Parse(s[1]) + 1;
        levelController.Level = s[0] + "-" + nextSubLevel.ToString();
        Debug.Log(levelController.Level);
        SceneManager.LoadScene(1);
    }
}
