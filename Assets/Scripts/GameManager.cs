using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    enum GameState { LoadLevel, InitializedLevel, Play, Win, Setting, LostTrack };

    private GameState gameState;
    private LevelController levelController;
    private Terrian terrain;

    public Text gameAdvice;
    public GameObject WinPanel;
    public GameObject wall;
    public float startWait;

    void Awake()
    {
        gameState = GameState.LoadLevel;
        try
        {
            levelController = GameObject.Find("LevelController").GetComponent<LevelController>();
        }
        catch
        {
            throw new Exception("No level controller found.");
        }
    }

	// Use this for initialization
	void Start ()
    {
        string level = levelController.Level;
        GameObject levelTerrian = (GameObject)Resources.Load("levels/" + "1-1", typeof(GameObject));
        try
        {
            GameObject terrainObject = (GameObject)Instantiate(levelTerrian, new Vector3(0f, 0f, 0f), Quaternion.identity);
            terrain = terrainObject.GetComponentInChildren<Terrian>();
            
            GameObject wallObject = (GameObject)Instantiate(wall, new Vector3(0f, 0f, 0f), Quaternion.identity);

            // GameObject wallObject2 = (GameObject)Instantiate(wall, new Vector3(0f, 0f, 0f), Quaternion.identity);

            gameState = GameState.InitializedLevel;
        }
        catch
        {
            Debug.Log("No Level Terrian found");
            SceneManager.LoadScene(0);
        }
    }
	
	// Update is called once per frame
	void Update () {
        switch(gameState)
        {
            case GameState.LoadLevel: break;
            case GameState.InitializedLevel:
                gameAdvice.text = "Please point device at terrain target to initialize terrain.";
                if (!terrain)
                {
                    throw new Exception("No Terrian Compoenent inside TerrianObject");
                }

                if (terrain.IsFound)
                {
                    gameState = GameState.Play;
                }
                break;
            case GameState.Play:
                gameAdvice.text = "";
                if (!terrain)
                {
                    throw new Exception("No Terrian Compoenent inside TerrianObject");
                }

                if (!terrain.IsFound) gameState = GameState.LostTrack;

                if (terrain.IsWin)
                {
                    gameState = GameState.Win;
                }
                break;
            case GameState.LostTrack:
                gameAdvice.text = "Terrain track lost. Please point device at terrain image target.";
                if (terrain.IsFound) gameState = GameState.Play;
                break;
            case GameState.Win:
                WinPanel.SetActive(true);
                break;
            default: break;
        }
	}
}
