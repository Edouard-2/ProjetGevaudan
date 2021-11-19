using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AddPapierGUI : MonoBehaviour
{
    //List papier in game
    public List<GameObject> listPapier;

    //Data des papier
    public PapierData data;

    private void Update()
    {
        //Si le joueur click et que aucun autres objet est en vu frontale (zoomé)
        if (Input.GetMouseButtonDown(0) && FindObjectOfType<InteractifObject>().state == 0 && FindObjectOfType<GameManager>().gameState == 2)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                //Si le raycast touche le flou 
                if( hit.transform.tag == "Flou" && FindObjectOfType<PapierManager>().zoom )
                {
                    int length = listPapier.Count;

                    for (int i = 0; i < length; i++)
                    {
                        //Mettre a jour les data papier et l'ui 
                        if (listPapier[i].GetComponent<PapierMovement>().state == 1)
                        {
                            data.boolPapier[i] = true;
                            listPapier[i].GetComponent<PapierMovement>().state = 0;
                            listPapier[i].transform.position = new Vector3(-10000, 0, 0);
                            listPapier[i].SetActive(false);
                        }
                    }

                    IsScrolling[] listScroll = FindObjectsOfType<IsScrolling>();

                    foreach (IsScrolling item in listScroll)
                    {
                        item.DeZoom = false;
                        item.DeZooming();
                    }

                    //faire disparaitre l'obj
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
