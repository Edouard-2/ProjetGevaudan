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
            

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if ((gameObject.tag == "Seconde_Hitbox" || gameObject.tag == "interieur") && FindObjectOfType<GameManager>().gameState == 2)
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

                if (FindObjectOfType<GameManager>().gameState == 2)
                {
                    if(gameObject.tag == "First_Hitbox")
                    {
                        FindObjectOfType<GameManager>().switchZoom(0);
                    }
                    else if (gameObject.tag == "interieur" || gameObject.tag == "Seconde_Hitbox")
                    {
                        FindObjectOfType<GameManager>().switchZoom(3);
                    }
                }
                else if (FindObjectOfType<GameManager>().gameState == 3)
                {
                    FindObjectOfType<GameManager>().switchZoom(0);
                }
                else if(hit.transform.tag != "Untagged" && hit.transform.tag != "interieur")
                {
                    Debug.Log(hit.transform.name);
                    FindObjectOfType<GameManager>().switchZoom(0);
                }
            }
        }
    }


    void switchColliderState()
    {
        if (readyClick && FindObjectOfType<GameManager>().gameState == 3 )
        {
            if (gameObject.tag == "First_Hitbox")
            {
                gameObject.GetComponent<BoxCollider>().enabled = true;
            }
            
            if (gameObject.tag == "Seconde_Hitbox")
            {
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<BoxCollider>().enabled = true;
            }

            if (gameObject.tag == "interieur" && !FindObjectOfType<ScryptTextManager>().done)
            {
                gameObject.GetComponent<BoxCollider>().enabled = true;
            }
            else if ( gameObject.tag == "interieur" )
            {
                gameObject.GetComponent<BoxCollider>().enabled = false;
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
            else if(gameObject.tag == "First_Hitbox")
            {
                gameObject.GetComponent<BoxCollider>().enabled = true;
            }
            if (gameObject.tag == "interieur" && !FindObjectOfType<ScryptTextManager>().done)
            {
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
            else if (gameObject.tag == "interieur")
            {
                gameObject.GetComponent<BoxCollider>().enabled = true;
            }
            if (gameObject.name == "Avant")
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
            if (gameObject.tag == "interieur")
            {
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }

        }
    }
}