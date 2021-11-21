using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingManager : MonoBehaviour
{
    private Vector2 initPoint;
    private float curDist;
    private bool readyClick = true;
    
    public GameObject cameraEmpty;

    private void Update()
    {
        switchColliderState();
    }

    private void OnMouseDown()
    {
        if (FindObjectOfType<GameManager>().gameState == 1)
        {
            readyClick = true;
            initPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }

    private void OnMouseDrag()
    {
        if (FindObjectOfType<GameManager>().gameState == 1)
        {
            //Calcul distance si cette distance est plus petite que x alors clique
            curDist = Vector2.Distance(initPoint, Input.mousePosition);
            if (curDist / 50 >= 1)
            {
                //Desactive le click
                readyClick = false;
                FindObjectOfType<CameraController>().readyClick = true;
            }
        }
    }

    private void OnMouseUp()
    {
        if( readyClick && FindObjectOfType<GameManager>().gameState == 1)
        {
            Debug.Log("Zoom");

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                FindObjectOfType<CameraController>().moveCamera(cameraEmpty, true);
                
            }
                FindObjectOfType<GameManager>().switchZoom();
        }
    }


    void switchColliderState()
    {
        if (readyClick && FindObjectOfType<GameManager>().gameState == 2)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        if (readyClick && FindObjectOfType<GameManager>().gameState == 1)
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }
}