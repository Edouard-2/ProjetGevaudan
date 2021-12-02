using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventaireManager : MonoBehaviour, /*IPointerClickHandler,*/ IDragHandler, IPointerUpHandler
{
    //Autre
    public int state;

    public float curDist = 0;

    private bool close = true;

    public bool activation = true;

    //Inventaire
    public GameObject caseInventaire;

    public GameObject downImage1;
    public GameObject downImage2;

    //Emplacement des items
    public List<GameObject> emplacementItems;

    //List des obj ds l'inventaire
    public GameObject[] listObj;
    
    public Vector2 initPoint = new Vector2(-1, -1);

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        listObj = new GameObject[8];
        print(listObj.Length);
    }

    [System.Obsolete]
    public void OnDrag(PointerEventData eventData)
    {
        //Si l'inventaire est sorti
        if (state == 1 && activation)
        {
            if (close)
            {
                close = false;
            }

            //Initialiser la première position
            if (initPoint == new Vector2(-1, -1))
            {
                initPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }

            //Calcul du point du doigt
            Vector2 curPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //Calcul de la distance
            curDist = Mathf.Abs(Mathf.Pow(initPoint.x - curPoint.x, 2) + Mathf.Pow(initPoint.y - curPoint.y, 2));

            //Si la distance avec le doit est de 30000 alors on fait un tour de 45°
            if (curDist / 10000 >= 1)
            {
                initPoint = curPoint;
                gameObject.GetComponent<InventaireMovement>().rotateInventaire();
                caseInventaire.GetComponent<InventaireMovement>().rotateInventaire();
            }
        }
    }

    //Lorsque le doigt est levé
    public void OnPointerUp(PointerEventData eventData)
    {
        //On désactive le OnDrag
        if (initPoint != new Vector2(-1, -1))
        {
            close = true;
            initPoint = new Vector2(-1, -1);
        }
    }

    //Changement de state
    public void switchState()
    {
        state = Mathf.Abs(state - 1);
        verifColliderObj();

        //Changement de positon
        print("switchState");
        gameObject.GetComponent<InventaireMovement>().SwitchPosition(state);
        caseInventaire.GetComponent<InventaireMovement>().SwitchPosition(state);

    }

    //Fermeture de l'inventaire
    public void Fermeture()
    {
        if( state == 1)
        {
            //verifier si le curobj de intéractif n'est pas sphere
            if (FindObjectOfType<InteractifObject>().state == 1 && FindObjectOfType<InteractifObject>().curObject.name == "sphere")
            {

            }
            else
            {
                print("fermeture");
                switchState();
                downImage1.SetActive(false);
                downImage2.SetActive(false);
            }
        }
    } 
    public void Ouverture()
    {
        if( state == 0)
        {
            switchState();
            downImage1.SetActive(true);
            downImage2.SetActive(true);
        }
    }

    //Si l'inventaire est plié le collider des obj est désactiver pour ne pas pouvoir clicker dessus
    public void verifColliderObj()
    {
        //Verification si l'inventaire est plié
        if (state == 0)
        {
            //Desactive du collider
            foreach (GameObject item in listObj)
            {
                if (item != null && item.GetComponent<InitData>().state == 0)
                {
                    item.GetComponent<BoxCollider>().enabled = false;
                }
            }
        }
        //Si l'inventaire est active
        else
        {
            //Collider active
            foreach (GameObject item in listObj)
            {
                if (item != null)
                {
                    item.GetComponent<BoxCollider>().enabled = true;
                }
            }
        }
    }
}