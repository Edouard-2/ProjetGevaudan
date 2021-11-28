using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTiroir : MonoBehaviour
{
    public bool besoinClef;

    public int state = -1;

    Vector3 initPos;

    Quaternion initRot;

    public GameObject emptyCamera;

    CameraController camera;

    GameManager myGameManager;

    private void Start()
    {
        camera = FindObjectOfType<CameraController>();
        myGameManager = FindObjectOfType<GameManager>();
    }

    //Si le joueur click alors la camera va au dessus ou en dessous / le tiroir s'ouvre s'il est fermé
    private void OnMouseUpAsButton()
    {
        //Si le tiroir est fermé
        if( myGameManager.gameState == 2 && !besoinClef && state == -1)
        {
            print(name);
            GetComponent<Animator>().SetTrigger("open");
            state = 1;
            InitVariable();
            MoveCamera(emptyCamera.transform.position, emptyCamera.transform.rotation);
            /*gameObject.GetComponent<ScrollingManager>().enabled = true;*/
        }

        //Si le tiroir est ouvert on change les camera
        else if( ( myGameManager.gameState == 2 || myGameManager.gameState == 4 ) && state != -1)
        {
            print(state);
            if (state == 0)
            {
                MoveCamera(emptyCamera.transform.position, emptyCamera.transform.rotation);
                state = Mathf.Abs(state - 1);
                myGameManager.prevGameState = myGameManager.gameState;
                myGameManager.gameState = 4;
            }
            else if (state == 1) 
            {
                MoveCamera(initPos, initRot);
                state = Mathf.Abs(state - 1);
                myGameManager.prevGameState = myGameManager.prevGameState;
                myGameManager.gameState = 2;
            }
        }
    }

    void InitVariable()
    {
        initPos = camera.transform.position;
        initRot = camera.transform.rotation;
    }

    //Bouger la camera au dessus du tiroir
    void MoveCamera(Vector3 _positon, Quaternion _rotation)
    {
        camera.NextPosition = _positon;
        camera.NextRotation = _rotation;
        camera.transform.rotation = _rotation;
    }

    //Changement de state
    public void ChangeState()
    {
        besoinClef = false;
    }
}
