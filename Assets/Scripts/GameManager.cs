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

    public void switchZoom(int _nbr)
    {
        

        if (_nbr > 2)
        {
            prevGameState = gameState;
            gameState = _nbr;
        }

        else if (_nbr == -1) 
        {
            print("state");
            prevGameState = gameState;
            gameState = 0;
        }
        else if ( gameState == 1) 
        { 
            prevGameState = gameState;
            gameState = 2;
        }
        else if (gameState == 0 && prevGameState > 1)
        {
            int temp = prevGameState;
            prevGameState = gameState;
            gameState = temp;
        }
        else if (gameState == 0)
        {
            prevGameState = gameState;
            gameState = 1;
        }

        else if (gameState == 2 )
        {
            prevGameState = gameState;
            gameState = 1;
        }

        else if (gameState == 3)
        {
            prevGameState = gameState;
            gameState = 2;
        }
    }
}
