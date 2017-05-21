using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Terrian : MonoBehaviour {
    public List<GoalController> goals;

    private DefaultTrackableEventHandler mDefaultTrackableEventHandler;
    private bool isFound;
    private bool isWin;

    public bool IsWin { get { return isWin; } }

    public bool IsFound {
        get { return isFound; }
        set { isFound = value; }
    }

    // Use this for initialization
    void Start () {
        isWin = false;
        isFound = false;
        mDefaultTrackableEventHandler = GetComponent<DefaultTrackableEventHandler>();
    }
	
	// Update is called once per frame
	void Update () {
        updateGameWinningState();
    }

    void updateGameWinningState()
    {
        bool win = true;
        foreach (GoalController goal in goals)
        {
            win = win & goal.IsHit;
        }
        isWin = win;
    }
}
