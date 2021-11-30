using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObEnigme1 : MonoBehaviour
{
    public GameManager myGameManager;

    public Rigidbody myRb;

    bool readyOver = false;
    private float initPoint;

    // Start is called before the first frame update
    void Start()
    {
        myGameManager = FindObjectOfType<GameManager>();

        initPoint = -1000000;
    }

    private void OnMouseDrag()
    {
        if (FindObjectOfType<GameManager>().gameState == 2)
        {
            if (initPoint == -1000000)
            {
                initPoint = Input.mousePosition.y;
            }

            //Calcul du point du doigt
            float curPoint = Input.mousePosition.y;

            float curDistance = Vector3.Distance(new Vector3(0, curPoint, 0), new Vector3(0, initPoint, 0));

            if (curDistance / 50 >= 1)
            {
                initPoint = curPoint;
                moveObject();
            }
        }
    }

    void moveObject()
    {
        print("animation");
        GetComponent<Animator>().SetTrigger("open");
        GetComponent<BoxCollider>().enabled = false;
    }
}
