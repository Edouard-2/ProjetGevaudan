using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{

    public bool downDrag = false;
    public bool exitDrag = false;
    public bool readyDrag = true;


    private void OnMouseDrag()
    {
        downDrag = true;
        if ((exitDrag || gameObject.GetComponent<InitData>().state == 3) && FindObjectOfType<GameManager>().gameState >= 2)
        {
            DetachObj();
            FindObjectOfType<InventaireManager>().Fermeture();
            if ( name == "cle_croix" || name == "cle_dent" || name == "cle_bureau" )
            {   
                gameObject.transform.localScale = gameObject.GetComponent<InitData>().initScale;
                gameObject.transform.localPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
                if (name == "cle_dent" || name == "cle_bureau")
                {
                    gameObject.transform.rotation = Quaternion.Euler(new Vector3(270, -90, 0));
                }
                else
                {

                    gameObject.transform.rotation = Quaternion.Euler(new Vector3(90, 180, 0));
                }
            }
            else
            {
                gameObject.transform.localScale = gameObject.GetComponent<InitData>().facteur / 2;
                gameObject.transform.localPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.4f));
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(270, 90, 0));
            }
            FindObjectOfType<InventaireManager>().activation = false;
            /*gameObject.transform.rotation = gameObject.GetComponent<InitData>().initRotation;*/
        }
    }

    private void OnMouseExit()
    {
        if (downDrag && gameObject.GetComponent<InitData>().state == 2 && FindObjectOfType<GameManager>().gameState >= 2)
        {
            exitDrag = true;
        }
    }

    private void OnMouseUp()
    {
        if (gameObject.GetComponent<InitData>().state == 3 && readyDrag)
        {
            FindObjectOfType<InventaireManager>().activation = true;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == gameObject.name + "_Receptacle")
                {
                    if (hit.transform.name == "piece_loup_Receptacle" && hit.transform.GetComponent<Receptacle>().verifRond())
                    {
                        gameObject.GetComponent<InitData>().state = 4;
                        gameObject.transform.position = hit.transform.GetComponent<Receptacle>().empty.position;
                        gameObject.transform.rotation = hit.transform.GetComponent<Receptacle>().empty.rotation;
                        gameObject.transform.SetParent(hit.transform);
                        gameObject.transform.localScale = Vector3.one;
                    }
                    else if (hit.transform.name == "piece_loup_Receptacle")
                    {
                        gameObject.GetComponent<BoxCollider>().enabled = true;
                        gameObject.GetComponent<InitData>().state = 2;
                        exitDrag = false;
                        FindObjectOfType<InteractifObject>().GoInventaire(gameObject.transform);
                    }
                    else if(hit.transform.name == "cle_croix_Receptacle" || hit.transform.name == "cle_bureau_Receptacle" || hit.transform.name == "cle_dent_Receptacle")
                    {
                        gameObject.GetComponent<InitData>().state = 4;
                        gameObject.transform.position = hit.transform.GetComponent<Receptacle>().empty.position;

                        if(hit.transform.name != "cle_bureau_Receptacle")
                        {
                            hit.transform.GetComponent<Receptacle>().ActiveBlock();
                        }
                        else
                        {
                            hit.transform.GetComponent<Receptacle>().ActiveAnimationBureau();
                        }
                        gameObject.transform.SetParent(hit.transform);
                    }
                }
                else if (hit.transform.name == "morceau_Receptacle" && (gameObject.name == "morceau1" || gameObject.name == "morceau2" || gameObject.name == "morceau3") && 
                    (hit.transform.GetComponent<Receptacle>().id == 0))
                {
                    hit.transform.GetComponent<Receptacle>().id = 1;
                    gameObject.GetComponent<InitData>().state = 4;
                    gameObject.transform.position = hit.transform.GetComponent<Receptacle>().empty.position;

                    gameObject.transform.SetParent(FindObjectOfType<LabyrinthManager>().gameObject.transform);

                    gameObject.transform.rotation = FindObjectOfType<LabyrinthManager>().gameObject.transform.rotation;
                    gameObject.transform.localScale = gameObject.transform.localScale * 3.2f;
                    gameObject.GetComponent<BoutLabyrinthDrag>().state = 1;
                    gameObject.GetComponent<BoutLabyrinthDrag>().receptacle = hit.transform.gameObject;
                    gameObject.GetComponent<BoxCollider>().enabled = true;
                    gameObject.GetComponent<BoxCollider>().size = new Vector3(gameObject.GetComponent<BoxCollider>().size.x, gameObject.GetComponent<BoxCollider>().size.y, 6);
                    readyDrag = false;
                }
                else
                {
                    gameObject.GetComponent<BoxCollider>().enabled = true;
                    gameObject.GetComponent<InitData>().state = 2;
                    exitDrag = false;
                    FindObjectOfType<InteractifObject>().GoInventaire(gameObject.transform);
                }
            }
        }
        exitDrag = false;
        downDrag = false;
    }

    private void OnMouseOver()
    {
        if (gameObject.GetComponent<InitData>().state == 3)
        {
            exitDrag = false;
        }
    }

    public void DetachObj()
    {
        if (gameObject.GetComponent<InitData>().state == 2)
        {
            gameObject.GetComponent<InitData>().state = 3;

            //Le mettre dans l'empty de la case a l'interieur de l'inventaire
            gameObject.transform.parent = FindObjectOfType<InteractifObject>().transform;

            //Ajouter l'inventaire a la list
            FindObjectOfType<InventaireManager>().listObj[gameObject.GetComponent<InitData>().id] = null;
            
        }
    }
}
