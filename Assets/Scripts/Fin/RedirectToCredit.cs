using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RedirectToCredit : MonoBehaviour
{
    public GameObject myCamera;
    public GameObject emptyCamera;
    bool readyTransition = false;

    // Update is called once per frame
    void Update()
    {
        if (readyTransition)
        {
            myCamera.transform.position = Vector3.MoveTowards(myCamera.transform.position, emptyCamera.transform.position, 2f * Time.deltaTime);
            if (myCamera.transform.position == emptyCamera.transform.position)
            {
                SceneManager.LoadScene("SplashScreen");
            }
        }
    }

    private void OnMouseUpAsButton()
    {
        FindObjectOfType<CameraController>().readyActive = false;
        FindObjectOfType<GameManager>().gameState = -2;
        FindObjectOfType<GameManager>().prevGameState = -2;
        readyTransition = true;
    }

}
