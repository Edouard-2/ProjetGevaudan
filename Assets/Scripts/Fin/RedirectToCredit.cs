using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RedirectToCredit : MonoBehaviour
{
    public GameObject myCamera;
    public GameObject emptyCamera;
    public GameObject inventaire;
    public CreditManager credit;
    bool readyTransition = false;

    private void Start()
    {
        credit = FindObjectOfType<CreditManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (readyTransition)
        {
            myCamera.transform.position = Vector3.MoveTowards(myCamera.transform.position, emptyCamera.transform.position, 2f * Time.deltaTime);
            if (myCamera.transform.position == emptyCamera.transform.position)
            {
                credit.LaunchCredit();
            }
        }
    }

    public void activeCredit()
    {
        FindObjectOfType<CameraController>().readyActive = false;
        FindObjectOfType<GameManager>().gameState = -2;
        FindObjectOfType<GameManager>().prevGameState = -2;
        inventaire.gameObject.transform.position = new Vector3(1000,1000,100);
        readyTransition = true;
    }

}
