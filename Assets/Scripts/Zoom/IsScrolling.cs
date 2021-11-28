using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsScrolling : MonoBehaviour
{
    public bool DeZoom = true;
    public bool state = true;

    private void OnMouseUpAsButton()
    {
        StartCoroutine(DeZoomAction());
    }

    //Appeler dans le script addPapier
    public void DeZooming()
    {
        StartCoroutine(activeDeZoom());
    }
    public void activeState()
    {
        StartCoroutine(changeState());
    }

    IEnumerator activeDeZoom()
    {
        yield return new WaitForSeconds(0.1f);

        DeZoom = true;
    }
    IEnumerator changeState()
    {
        yield return new WaitForSeconds(0.1f);

        state = true;
    }

    IEnumerator DeZoomAction()
    {
        yield return new WaitForSeconds(0.01f);
        if (state && DeZoom && FindObjectOfType<GameManager>().gameState == 2 && (FindObjectOfType<InteractifObject>().curObject == FindObjectOfType<InteractifObject>().transform || FindObjectOfType<InteractifObject>().curObject.GetComponent<InitData>().state != 1))
        {
            
            FindObjectOfType<GameManager>().switchZoom(0);
            FindObjectOfType<CameraController>().moveCamera(gameObject, false);
            Debug.Log("DeZoom");
        }
    }
}