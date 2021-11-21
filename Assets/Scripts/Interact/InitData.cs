using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitData : MonoBehaviour
{
    //Transform valeur
    public Vector3 initPosition;
    public Vector3 initScale;

    //Autre
    public float rotSpeed = 5f;

    public Quaternion initRotation;

    public int state = 0;

    //Drag and drop
    private Vector3 mOffset;
    private float mZCoord;

    //Id
    public int id = -1;

    // Start is called before the first frame update
    void Start()
    {
        //Initialisation
        initPosition = transform.position;
        initScale = transform.lossyScale;
        initRotation = transform.rotation;
    }

    private void Update()
    {
        
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
        if ( state == 2 && FindObjectOfType<InteractifObject>().state == 0 && ready && FindObjectOfType<GameManager>().gameState != 0)
        {
            Debug.Log("hey");
            //On le sort de l'inventaire
            RemoveInventaire();

            //Le mettre a l'endroit zoomé devant le joueur
            FindObjectOfType<InteractifObject>().CheckMovement(gameObject.transform);
        }
    }

    //Sortir de l'inventaire
    public void RemoveInventaire()
    {
        FindObjectOfType<InventaireManager>().listObj.Remove(gameObject);

        //On lui change le parent
        transform.parent = FindObjectOfType<InteractifObject>().transform;

    }
}
