using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsScrolling : MonoBehaviour
{
    public bool DeZoom = true;

    private void OnMouseUpAsButton()
    {
        if (DeZoom && FindObjectOfType<GameManager>().gameState == 2 && (FindObjectOfType<InteractifObject>().curObject == FindObjectOfType<InteractifObject>().transform || FindObjectOfType<InteractifObject>().curObject.GetComponent<InitData>().state != 1) )
        {
            FindObjectOfType<GameManager>().switchZoom();
            FindObjectOfType<CameraController>().moveCamera(gameObject, false);
            Debug.Log("DeZoom");
        }
    }

    //Appeler dans le script addPapier
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