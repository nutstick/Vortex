using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vuforia;

public class GameManager : MonoBehaviour {

    enum GameState { LoadLevel, InitializedLevel, Play, Win, Setting, LostTrack };

    private GameState gameState;
    private LevelController levelController;
    private Terrian terrain;

    public Text gameAdvice;
    public GameObject WinPanel;
    public GameObject ItemBar;

    public GameObject WallIcon;
    public GameObject SplitterlIcon;

    public GameObject AImageTarget;
    public GameObject BImageTarget;
    public GameObject CImageTarget;
    public GameObject DImageTarget;
    public GameObject EImageTarget;
    public GameObject FImageTarget; 

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
            if (terrain.A && AImageTarget)
            {
                GameObject parent = Instantiate(AImageTarget);
                GameObject child = Instantiate(terrain.A);
                AddIcon("A", terrain.A.tag);
                child.transform.SetParent(parent.transform);
            }
            if (terrain.B && BImageTarget)
            {
                GameObject parent = Instantiate(BImageTarget);
                GameObject child = Instantiate(terrain.B);
                AddIcon("B", terrain.B.tag);
                child.transform.SetParent(parent.transform);
            }
            if (terrain.C && CImageTarget)
            {
                GameObject parent = Instantiate(CImageTarget);
                GameObject child = Instantiate(terrain.C);
                AddIcon("C", terrain.C.tag);
                child.transform.SetParent(parent.transform);
            }
            if (terrain.D && DImageTarget)
            {
                GameObject parent = Instantiate(DImageTarget);
                GameObject child = Instantiate(terrain.D);
                AddIcon("D", terrain.D.tag);
                child.transform.SetParent(parent.transform);
            }
            if (terrain.E && EImageTarget)
            {
                GameObject parent = Instantiate(EImageTarget);
                GameObject child = Instantiate(terrain.E);
                AddIcon("E", terrain.E.tag);
                child.transform.SetParent(parent.transform);
            }
            if (terrain.F && FImageTarget)
            {
                GameObject parent = Instantiate(FImageTarget);
                GameObject child = Instantiate(terrain.F);
                AddIcon("F", terrain.F.tag);
                child.transform.SetParent(parent.transform);
            }
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
 

        switch (gameState)
        {
            case GameState.LoadLevel: break;
            case GameState.InitializedLevel:
                ItemBar.SetActive(false);
                WinPanel.SetActive(false);
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
                ItemBar.SetActive(true);

                if (!terrain)
                {
                    throw new Exception("No Terrian Compoenent inside TerrianObject");
                }

                if (!terrain.IsFound)
                {
                    ItemBar.SetActive(false);
                    gameState = GameState.LostTrack;
                }

                if (terrain.IsWin) gameState = GameState.Win;
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

    void AddIcon(string name, string tag)
    {
        GameObject obj = null;
        switch(tag)
        {
            case "Mirror":
                obj = Instantiate(WallIcon);
                break;
            case "Split":
                obj = Instantiate(SplitterlIcon);
                break;
            default:
                break;
        }
        if (obj)
        {
            obj.transform.GetChild(0).GetComponent<Text>().text = name;
            obj.transform.SetParent(ItemBar.transform);
        }
    }
}
