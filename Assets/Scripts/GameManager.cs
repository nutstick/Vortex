using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vuforia;

public class GameManager : MonoBehaviour {

    enum GameState { LoadLevel, InitializedLevel, Play, Win, Setting, LostTrack, DisplayLevelName };

    private GameState gameState;
    private LevelController levelController;
    private Terrian terrain;
    private ObjectTracker objectTracker;
    private List<string> trackersName;

    public Text gameAdvice;
    public Text centerText;
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
        if (centerText)
        {
            centerText.text = "";
        }
        if (gameAdvice)
        {
            gameAdvice.text = "";
        }

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
        GameObject levelTerrian = (GameObject)Resources.Load("levels/" + levelController.Level, typeof(GameObject));

        try
        {
            trackersName = new List<string>();
            GameObject terrainObject = (GameObject)Instantiate(levelTerrian, new Vector3(0f, 0f, 0f), Quaternion.identity);
            terrain = terrainObject.GetComponentInChildren<Terrian>();
            trackersName.Add("Stone");
            if (terrain.A && AImageTarget)
            {
                GameObject parent = Instantiate(AImageTarget);
                GameObject child = Instantiate(terrain.A);
                AddIcon("A", terrain.A.tag);
                trackersName.Add("A");
                child.transform.SetParent(parent.transform, false);
            }
            if (terrain.B && BImageTarget)
            {
                GameObject parent = Instantiate(BImageTarget);
                GameObject child = Instantiate(terrain.B);
                AddIcon("B", terrain.B.tag);
                trackersName.Add("B");
                child.transform.SetParent(parent.transform, false);
            }
            if (terrain.C && CImageTarget)
            {
                GameObject parent = Instantiate(CImageTarget);
                GameObject child = Instantiate(terrain.C);
                AddIcon("C", terrain.C.tag);
                trackersName.Add("C");
                child.transform.SetParent(parent.transform, false);
            }
            if (terrain.D && DImageTarget)
            {
                GameObject parent = Instantiate(DImageTarget);
                GameObject child = Instantiate(terrain.D);
                AddIcon("D", terrain.D.tag);
                trackersName.Add("D");
                child.transform.SetParent(parent.transform, false);
            }
            if (terrain.E && EImageTarget)
            {
                GameObject parent = Instantiate(EImageTarget);
                GameObject child = Instantiate(terrain.E);
                AddIcon("E", terrain.E.tag);
                trackersName.Add("E");
                child.transform.SetParent(parent.transform, false);
            }
            if (terrain.F && FImageTarget)
            {
                GameObject parent = Instantiate(FImageTarget);
                GameObject child = Instantiate(terrain.F);
                AddIcon("F", terrain.F.tag);
                trackersName.Add("F");
                child.transform.SetParent(parent.transform, false);
            }
            VuforiaARController.Instance.RegisterVuforiaStartedCallback(LoadDataSet);
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
            case GameState.DisplayLevelName: break;
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
                ItemBar.SetActive(false);
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
            case "Splitter":
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

    void LoadDataSet()
    {
        objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();

        foreach (string name in trackersName)
        {
            DataSet dataSet = objectTracker.CreateDataSet();
            if (dataSet.Load(name))
            {

                objectTracker.Stop();  // stop tracker so that we can add new dataset

                if (!objectTracker.ActivateDataSet(dataSet))
                {
                    // Note: ImageTracker cannot have more than 100 total targets activated
                    Debug.Log("<color=yellow>Failed to Activate DataSet: " + name + "</color>");
                }

                if (!objectTracker.Start())
                {
                    Debug.Log("<color=yellow>Tracker Failed to Start.</color>");
                }
            }
            else
            {
                Debug.LogError("<color=yellow>Failed to load dataset: '" + name + "'</color>");
            }
        }

        gameState = GameState.DisplayLevelName;
        StartCoroutine(DisplayLevelName());
    }

    IEnumerator DisplayLevelName()
    {
        if (centerText)
        {
            centerText.text = levelController.Level;
            centerText.transform.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(startWait);
        if (centerText)
        {
            centerText.text = "";
        }
        gameState = GameState.InitializedLevel;
    }


    void OnDestroy()
    {
        // objectTracker.Stop();
        // objectTracker.DestroyAllDataSets(false);
        VuforiaARController.Instance.UnregisterVuforiaStartedCallback(LoadDataSet);
        Debug.Log("-------------");
    }

}