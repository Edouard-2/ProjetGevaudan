using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingManager : MonoBehaviour
{
    private Vector2 initPoint;
    private float curDist;
    private bool readyClick = true;

    public bool activate;
    
    public GameManager myGameManager;

    public ScryptTextManager myScryotTextManager;

    public CameraController myCameraController;

    public InventaireManager myInventaireManager;

    public BoxCollider myCollider;

    public GameObject cameraEmpty;

    private void Awake()
    {
        //Initialisation des variables

        myGameManager = FindObjectOfType<GameManager>();
        myCollider = gameObject.GetComponent<BoxCollider>();
        myScryotTextManager = FindObjectOfType<ScryptTextManager>();
        myCameraController = FindObjectOfType<CameraController>();
        myInventaireManager = FindObjectOfType<InventaireManager>();
    }

    private void Update()
    {
        switchColliderState();
    }

    private void OnMouseDown()
    {
        //Si le joueur appuy dessus activation du bouton
        if (myGameManager.gameState == 1)
        {
            readyClick = true;
            initPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }

    private void OnMouseDrag()
    {
        //Si le joueur appuy vraiment dessus sans faire un scroll
        if (myGameManager.gameState == 1)
        {
            //Calcul distance si cette distance est plus petite que x alors clique
            curDist = Vector2.Distance(initPoint, Input.mousePosition);
            if (curDist / 50 >= 1)
            {
                //Desactive le click
                readyClick = false;
                myCameraController.readyClick = true;
            }
        }
    }

    private void OnMouseUp()
    {
        //Si le joueur a appuyé et relaché presque au meme endroit
        if(activate && readyClick && (myGameManager.gameState == 1 || myGameManager.gameState == 2 || myGameManager.gameState == 3))
        {
            //Projection d'un raycast pour voir quelle hitbox a été touché et zoom ou dezoom en fonction
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (( (gameObject.tag == "Seconde_Hitbox" || gameObject.tag == "interieur") && myGameManager.gameState == 2) || (gameObject.tag == "First_Hitbox" && (myGameManager.gameState == 1|| myGameManager.gameState == 3)) )
                {
                    myCameraController.moveCamera(cameraEmpty, true);
                }

                if (myGameManager.gameState == 2)
                {
                    if(gameObject.tag == "First_Hitbox" && (gameObject.name == "Avant" || gameObject.name == "Avant" + 2 || gameObject.name == "Avant" + 3 || gameObject.name == "Avant" + 4))
                    {
                        print("dezooù");
                        myGameManager.switchZoom(0);
                    }
                    else if (gameObject.tag == "interieur" || gameObject.tag == "Seconde_Hitbox")
                    {
                        myGameManager.switchZoom(3);
                    }
                    myInventaireManager.Fermeture();
                }
                else if (myGameManager.gameState == 3 || myGameManager.gameState == 1)
                {
                    myGameManager.switchZoom(0);
                }
                else if(hit.transform.tag != "Untagged" && hit.transform.tag != "interieur")
                {
                    Debug.Log(hit.transform.name);
                    myGameManager.switchZoom(0);
                }
            }
        }
    }

    //Faire en sorte que le collider d'une precedente hitbox soit désactiver pour ne pas géner le joueur
    void switchColliderState()
    {
        if (readyClick && myGameManager.gameState == 3 )
        {
            if (gameObject.tag == "First_Hitbox")
            {
                myCollider.enabled = true;
            }
            
            if (gameObject.tag == "Seconde_Hitbox")
            {
                myCollider.enabled = false;
            }
            else
            {
                myCollider.enabled = true;
            }

            if (gameObject.tag == "interieur" && !myScryotTextManager.done)
            {
                myCollider.enabled = true;
            }
            else if ( gameObject.tag == "interieur" )
            {
                myCollider.enabled = false;
            }

            if (gameObject.name == "Avant")
            {
                myCollider.enabled = false;
            }
        }
        
        if (readyClick && myGameManager.gameState == 2 )
        {
            if (gameObject.tag == "Seconde_Hitbox")
            {
                myCollider.enabled = true;
            }
            else if(gameObject.tag == "First_Hitbox")
            {
                myCollider.enabled = true;
            }
            if (gameObject.tag == "interieur" && !myScryotTextManager.done)
            {
                myCollider.enabled = false;
            }
            else if (gameObject.tag == "interieur")
            {
                myCollider.enabled = true;
            }
            if (gameObject.name == "Avant"|| gameObject.name == "Avant"+2 || gameObject.name == "Avant" + 3 || gameObject.name == "Avant" + 4)
            {
                myCollider.enabled = false;
            }
        }

        if (readyClick && myGameManager.gameState == 1)
        {
            if (gameObject.tag == "Seconde_Hitbox")
            {
                myCollider.enabled = false;
            }
            else if (gameObject.tag == "Seconde_Hitbox")
            {
                myCollider.enabled = true;
            }
            if (gameObject.name == "Avant")
            {
                myCollider.enabled = true;
            }
            if (gameObject.tag == "interieur")
            {
                myCollider.enabled = false;
            }

        }
    }
}