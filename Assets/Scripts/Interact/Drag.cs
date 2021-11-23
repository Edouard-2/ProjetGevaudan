using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{

    public bool downDrag = false;
    public bool exitDrag = false;


    private void OnMouseDrag()
    {
        if((exitDrag || gameObject.GetComponent<InitData>().state == 3) && FindObjectOfType<GameManager>().gameState == 2)
        {
            DetachObj();
            gameObject.transform.localScale = gameObject.GetComponent<InitData>().initScale;
            gameObject.transform.rotation = gameObject.GetComponent<InitData>().initRotation;

            gameObject.transform.localPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3));
        }
    }

    private void OnMouseExit()
    {
        if (gameObject.GetComponent<InitData>().state == 2 && FindObjectOfType<GameManager>().gameState == 2)
        {
            exitDrag = true;
        }
        
    }

    private void OnMouseUp()
    {
        if (gameObject.GetComponent<InitData>().state == 3)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.tag);
                if (hit.transform.name == gameObject.name + "_Receptacle")
                {
                    gameObject.GetComponent<InitData>().state = 4;
                    gameObject.transform.position = hit.transform.position;
                }
                else
                {
                    gameObject.GetComponent<BoxCollider>().enabled = true;
                    gameObject.GetComponent<InitData>().state = 2;
                    
                    FindObjectOfType<InteractifObject>().rangerInventaire(gameObject.transform);
                }
            }
            
        }
        exitDrag = false;
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
            FindObjectOfType<InventaireManager>().listObj.Remove(gameObject);

            FindObjectOfType<InventaireManager>().switchState();
        }
    }
}
