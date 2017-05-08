using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Text gameAdvice;
    public Terrain terrain;
    public float startWait;

    IEnumerator FindTerrain()
    {
        yield return new WaitForSecond(startWait);

        gameAdvice = "Please capture Terrian pattern.";
        gameAdvice.setActive();
        
        while (true)
        {
            if (terrain.isShow)
            {
                break;
            }
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
