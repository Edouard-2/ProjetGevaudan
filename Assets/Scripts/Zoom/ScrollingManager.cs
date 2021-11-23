using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingManager : MonoBehaviour
{
    private Vector2 initPoint;
    private float curDist;
    private bool readyClick = true;

    public bool activate;
    
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
        if(activate && readyClick && (FindObjectOfType<GameManager>().gameState == 1 || FindObjectOfType<GameManager>().gameState == 2 || FindObjectOfType<GameManager>().gameState == 3))
        {
            Debug.Log("Zoom");

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (gameObject.tag == "Seconde_Hitbox" && FindObjectOfType<GameManager>().gameState == 2)
                {
                    FindObjectOfType<CameraController>().moveCamera(cameraEmpty, true);
                }
                else if (gameObject.tag == "First_Hitbox" && FindObjectOfType<GameManager>().gameState == 1)
                {
                    FindObjectOfType<CameraController>().moveCamera(cameraEmpty, true);
                }
                else if (gameObject.tag == "First_Hitbox" && FindObjectOfType<GameManager>().gameState == 3)
                {
                    FindObjectOfType<CameraController>().moveCamera(cameraEmpty, true);
                }

                if(FindObjectOfType<GameManager>().gameState == 2)
                {
                    FindObjectOfType<GameManager>().switchZoom(3);
                    
                }
                else
                {
                    FindObjectOfType<GameManager>().switchZoom(0);
                }
            }
        }
    }


    void switchColliderState()
    {
        if (readyClick && FindObjectOfType<GameManager>().gameState == 3 )
        {
            if (gameObject.tag == "Seconde_Hitbox")
            {
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<BoxCollider>().enabled = true;
            }
            if (gameObject.name == "Avant")
            {
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }

        }
        
        if (readyClick && FindObjectOfType<GameManager>().gameState == 2 )
        {
            if (gameObject.tag == "Seconde_Hitbox")
            {
                gameObject.GetComponent<BoxCollider>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }

        if (readyClick && FindObjectOfType<GameManager>().gameState == 1)
        {
            if (gameObject.tag == "Seconde_Hitbox")
            {
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<BoxCollider>().enabled = true;
            }
            if (gameObject.name == "Avant")
            {
                gameObject.GetComponent<BoxCollider>().enabled = true;
            }

        }
    }
}