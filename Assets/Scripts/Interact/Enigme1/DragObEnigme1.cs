using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObEnigme1 : MonoBehaviour
{
    public GameManager myGameManager;

    bool readyOver = false;
    private float initPointy;
    private float initPointx;

    // Start is called before the first frame update
    void Start()
    {
        myGameManager = FindObjectOfType<GameManager>();

        initPointx = -1000000;
    }

    private void OnMouseDrag()
    {
        if (FindObjectOfType<GameManager>().gameState == 2)
        {
            if (initPointx == -1000000)
            {
                initPointy = Input.mousePosition.y;
                initPointx = Input.mousePosition.x;
            }

            //Calcul du point du doigt
            float curPoint = Input.mousePosition.y;

            float curDistance = Vector3.Distance(new Vector3(Input.mousePosition.x, curPoint, 0), new Vector3(initPointx, initPointy, 0));

            if (curDistance / 50 >= 1)
            {
                initPointy = curPoint;
                initPointx = Input.mousePosition.x;
                moveObject();
            }
        }
    }

    void moveObject()
    {
        GetComponent<Animator>().SetTrigger("open");
        GetComponent<BoxCollider>().enabled = false;
    }
}
