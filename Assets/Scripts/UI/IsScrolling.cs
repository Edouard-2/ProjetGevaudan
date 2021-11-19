using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IsScrolling : MonoBehaviour, IPointerExitHandler, IPointerClickHandler
{
    public bool isExit = true;
    public bool readyDown = false;

    public bool Zoom = true;
    public bool DeZoom = true;

    private Vector3 mousePosition;
    private Vector3 initPos;

    private void Start()
    {
        initPos = gameObject.transform.position;
    }

    private void Update()
    {
        if (readyDown && FindObjectOfType<GameManager>().gameState == 1 && Zoom )
        {
            gameObject.transform.position = mousePosition;
            
            if (Input.GetMouseButtonUp(0))
            {
                readyDown = false;
                gameObject.transform.position = initPos;
                FindObjectOfType<GameManager>().switchZoom();
                Debug.Log("Zoom");
            }
        }
    }

    //State du click et position camera
    public void stateClick(bool _b, Vector3 _mPosition)
    {
        readyDown = _b;
        mousePosition = _mPosition;
        isExit = false;

        
    }

    // Autoriser le drag (le mouvement de camera)
    public void OnPointerExit(PointerEventData eventData)
    {
        if( Zoom )
        {
            readyDown = false;
            gameObject.transform.position = initPos;
        } 
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if ( DeZoom && FindObjectOfType<GameManager>().gameState == 2 && FindObjectOfType<InteractifObject>().curObject.transform.position == FindObjectOfType<InteractifObject>().initPosition )
        {
            readyDown = false;
            FindObjectOfType<GameManager>().switchZoom();
            Debug.Log("DeZoom");
        }
    }

    public void DeZooming()
    {
        StartCoroutine(activeDeZoom());
    }

    IEnumerator activeDeZoom()
    {
        yield return new WaitForSeconds(0.1f);

        DeZoom = true;
    }
}