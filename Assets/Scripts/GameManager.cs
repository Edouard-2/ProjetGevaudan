using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 0: no interaction/ 1: interact with camera view / 2: interact object
    public int gameState = 0;
    public int prevGameState;

    // Start is called before the first frame update
    void Start()
    {
        prevGameState = 0;
        gameState = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void switchZoom()
    {
        if ( gameState == 1) 
        { 
            gameState = 2;
            prevGameState = 1;
        }

        else 
        {
            gameState = 1;
            prevGameState = 2;
        }
    }
}
