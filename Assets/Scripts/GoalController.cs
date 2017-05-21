using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour {

    private bool isHit;

    public bool IsHit
    {
        get
        {
            return isHit;
        }

        set
        {
            isHit = value;
        }
    }

    // Use this for initialization
    void Start () {
        isHit = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isHit)
        {

        }
        isHit = false;
    }
}

