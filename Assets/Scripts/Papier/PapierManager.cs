using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PapierManager : MonoBehaviour
{
    //List de tout les Papiers
    public List<GameObject> listPapier;

    //Ready pour faire le zoom du papier
    public bool zoom = true;

    bool raycastReady = true;

    //Autre
    public GameObject flou;
    public GameObject inventaire;

    private void Update()
    {
        //Lorsque le bouton gauche de la souris est appuyé
        if (Input.GetMouseButtonDown(0) && (FindObjectOfType<GameManager>().gameState == 2 || FindObjectOfType<GameManager>().gameState == 3 ))
        {
            //On verifie si aucun autre papier n'est déja avancé
            int length = listPapier.Count;
            
            for (int i = 0; i < length; i++)
            {
                if (listPapier[i].GetComponent<PapierMovement>().state != 0)
                {
                    zoom = false;
                    i = length;
                }
            }

            //On fait un raycast pour selectionner le papier viser
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                //On verifie qu'il n'est pas déja en avancé
                if (hit.transform.tag == "Papier" && zoom && raycastReady) 
                {
                    // On change son état
                    hit.transform.GetComponent<PapierMovement>().state = 1;
                    //On enlève les ombre
                    hit.transform.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                    //On avtive le fond
                    flou.SetActive(true);
                    Debug.Log("recup papier");
                    
                    //On abaisse l'inventaire si il est levé
                    if (inventaire.GetComponent<InventaireManager>().state != 0)
                    {
                        inventaire.GetComponent<InventaireManager>().switchState();
                    }
                }
                else
                {
                    raycastReady = false;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            raycastReady = true;
        }
    }
}
