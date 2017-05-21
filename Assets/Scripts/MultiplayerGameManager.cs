using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerGameManager : MonoBehaviour {

    enum GameState {
        START,
        WAIT_FOR_TERRIAN,
        SOLVED,
        PLAY,
    };

    private GameState gameState;

    IEnumerator GameRoutine()
    {
        yield return new WaitForSeconds(5);
        yield return WaitForTerrian();
    }

    IEnumerator WaitForTerrian()
    {
        gameState = GameState.WAIT_FOR_TERRIAN;
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                yield break;
            }
        }
        yield return null;
    }

    // Use this for initialization
    void Start ()
    {
        gameState = GameState.START;
        StartCoroutine(GameRoutine());
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
