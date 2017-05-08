using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Text gameAdvice;
    public Terrain terrain;
	public int startWait;

	IEnumerator FindTerrain()
    {
		yield return new WaitForSeconds(5);

		gameAdvice.text = "Please capture Terrian pattern.";
        
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
		StartCoroutine(FindTerrain());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
