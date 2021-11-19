using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventaireMovement : MonoBehaviour
{
    //Position haute et basse de l'inventaire
    public GameObject BasPos;
    public GameObject HautPos;

    //Canvas
    public GameObject canvas;

    //Taille du canvas
    public Vector2 canvasSize;

    //autre
    public float rotateData = 0f;

    private void Start()
    {
        //Initialisation de la taille du canvas
        canvasSize = new Vector2(canvas.GetComponent<RectTransform>().rect.width , canvas.GetComponent<RectTransform>().rect.height);
    }

    //Monter et descendre en fonction de la state
    public void SwitchPosition(int _state )
    {
        if (_state == 0)
        {
            //Descendre
            transform.position = BasPos.transform.position;        
        }
        else
        {
            //Monter
            transform.position = HautPos.transform.position; 
        }
    }

    [System.Obsolete]
    //faire tourner l'inventaire
    public void rotateInventaire()
    {
        //Si le joueur va de Droite à Gauche ou de bas en haut a Gauche de l'écran
        if (Input.GetAxis("Mouse X") < 0 || (Input.GetAxis("Mouse Y") > 0 && Input.mousePosition.x > canvasSize.x / 2 ) )
        {
            rotateData += 45f;
            Debug.Log(Input.mousePosition.x);
            Debug.Log(canvasSize.x);
        }
        //Si le joueur va de Gauche à Droite ou de bas en haut a Droite de l'écran
        else if (Input.GetAxis("Mouse X") > 0 || (Input.GetAxis("Mouse Y") > 0 && Input.mousePosition.x < canvasSize.x / 2))
        {
            rotateData -= 45f;
            Debug.Log(canvasSize.x);
        }

        //Faire la rotation
        transform.localRotation = Quaternion.Euler(0, 0, rotateData);
    }
}
