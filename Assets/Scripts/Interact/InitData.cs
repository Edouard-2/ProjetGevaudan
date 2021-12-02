using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitData : MonoBehaviour
{
    //Transform valeur
    public Vector3 initPosition;
    public Vector3 initScale;
    public Vector3 facteurItem;
    public Vector3 facteur;

    //Autre
    public float rotSpeed = 5f;

    public Vector3 initRotationValue;
    public Quaternion initRotation;

    public InventaireManager myInventaireManager;
    public InteractifObject myInteractifObject;

    public int state = 0;

    //Id
    public int id = -1;

    // Start is called before the first frame update
    void Start()
    {
        //Initialisation
        initPosition = transform.position;

        initScale = transform.lossyScale;
        
        initRotation =  Quaternion.Euler(initRotationValue);

        myInventaireManager = FindObjectOfType<InventaireManager>();

        myInteractifObject = FindObjectOfType<InteractifObject>();
    }


    [System.Obsolete]
    //LORSQUE LA SOURIS RESTE APPUYE
    private void OnMouseDrag()
    {
        //Si l'obj est en position de zoom et que l'état est autre que zoom
        if ( transform.position != initPosition && state == 1 )
        {
            //Calcul des angle en fonction de la souris
            float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
            float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;

            //Rotation du papier 
            transform.RotateAround(Vector3.up, -rotX);
            transform.RotateAround(Vector3.right, rotY);
        }
    }

    //Lorsqu'on click sur l'objet 
    private void OnMouseUpAsButton()
    {
        var otherInitData = FindObjectsOfType<InitData>();

        bool ready = true;

        foreach (InitData item in otherInitData)
        {
            if (item.state == 1 && item != this)
            {
                ready = false;
            }
        }

        //Verifie qu'il est dans l'état inventaire
        if ( state == 2 && myInteractifObject.state == 0 && ready && FindObjectOfType<GameManager>().gameState != 0)
        {
            Debug.Log("hey");
            //Le mettre a l'endroit zoomé devant le joueur
            myInteractifObject.CheckMovement(gameObject.transform);
            //On le sort de l'inventaire
            RemoveInventaire();
            if( gameObject.name != "sphere")
            {
                print("sphere name");
                myInventaireManager.Fermeture();
            }
        }
    }

    //Sortir de l'inventaire
    public void  RemoveInventaire()
    {

        //On lui change le parent
        transform.parent = myInteractifObject.transform;

    }
}
