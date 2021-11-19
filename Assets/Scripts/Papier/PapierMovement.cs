using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PapierMovement : MonoBehaviour
{
    //Obj ou le papier dois se rendre
    public GameObject reference;

    //Autres
    public int state = 0;
    public int id;

    //Vitesse 
    public float rotSpeed = 10f;
    public float speedPosition = 0.1f;

    private void Update()
    {
        //Si le papier a été cliquer
        if ( state == 1 )
        {
            //Le faire avancer a la position définis en x temps
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, reference.transform.position, Vector3.Distance(gameObject.transform.position, reference.transform.position) / speedPosition * Time.deltaTime);

            //Le faire scale a la dimension définis en x temps
            gameObject.transform.localScale = Vector3.MoveTowards(gameObject.transform.localScale, reference.transform.localScale, Vector3.Distance(gameObject.transform.localScale, reference.transform.localScale) / speedPosition * Time.deltaTime);
        }
    }

    [System.Obsolete]
    //LORSQUE LA SOURIS RESTE APPUYE
    private void OnMouseDrag()
    {
        //Si le papier a été cliquer
        if (state == 1)
        {
            //Calcul des angle en fonction de la souris
            float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
            float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;

            //Rotation du papier 
            transform.RotateAround(Vector3.up, -rotX);
            transform.RotateAround(Vector3.right, rotY);
        }
        
    }
}
