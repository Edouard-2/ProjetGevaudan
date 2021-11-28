using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffreFortManager : MonoBehaviour
{
    public int state = 0;
    public GameObject emptyCamera;
    public GameObject CorrectPorte;

    public BoxCollider myCollider;

    public GameManager myGameManager;

    public CameraController cameraManager;

    public List<CaseDrag> listCases;

    // Start is called before the first frame update
    void Start()
    {
        cameraManager = FindObjectOfType<CameraController>();
        myCollider = gameObject.GetComponent<BoxCollider>();
        myGameManager = FindObjectOfType<GameManager>();
        VerifCollider();
    }

    private void Update()
    {
        VerifCollider();
    }

    //Si le joueur click dessus on zoom ou dezoom 
    private void OnMouseUpAsButton()
    {
        if(myGameManager.gameState == 3)
        {
            myGameManager.switchZoom(4);
            state = 1;
            cameraManager.moveCamera(emptyCamera, true);
        }
        else if (myGameManager.gameState == 4)
        {
            myGameManager.switchZoom(3);
            state = 0;
            cameraManager.moveCamera(emptyCamera, false);
        }
    }

    //Activer ou desactiver le collider en fonction de la state du jeu
    void VerifCollider()
    {
        if(myGameManager.gameState == 3 || state == 1)
        {
            myCollider.enabled = true;
        }
        else
        {
            myCollider.enabled = false;
        }
    }

    //Ouvrir la porte si  toutes les cases sont au bonne endroit
    public void VerifOuverture()
    {
        int nbrWin = 0;
        foreach (CaseDrag item in listCases)
        {
            if(item.win == true)
            {
                nbrWin++;
            }
        }

        print(nbrWin);
        if (nbrWin == 4)
        {
            myGameManager.switchZoom(3);
            cameraManager.moveCamera(gameObject, false);
            foreach (CaseDrag item in listCases)
            {
                item.gameObject.SetActive(false);
                item.gameObject.GetComponent<BoxCollider>().enabled = false;
                item.enabled = false;
            }

            CorrectPorte.SetActive(true);

            gameObject.SetActive(false);
            this.enabled = false;

            CorrectPorte.GetComponent<Animator>().SetTrigger("open");
        }
    }
}
