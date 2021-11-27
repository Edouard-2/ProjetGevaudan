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

    private void Start()
    {

        camera = FindObjectOfType<CameraController>();
    }

    private void OnMouseUpAsButton()
    {
        if( FindObjectOfType<GameManager>().gameState == 2 && !besoinClef && state == -1)
        {
            print(name);
            GetComponent<Animator>().SetTrigger("open");
            state = 0;
            InitVariable();
            MoveCamera(emptyCamera.transform.position, emptyCamera.transform.rotation);
            /*gameObject.GetComponent<ScrollingManager>().enabled = true;*/
        }

        else if( ( FindObjectOfType<GameManager>().gameState == 2 || FindObjectOfType<GameManager>().gameState == 4 ) && state != -1)
        {
            print(name);
            if (state == 0)
            {
                MoveCamera(emptyCamera.transform.position, emptyCamera.transform.rotation);
                state = Mathf.Abs(state - 1);
                FindObjectOfType<GameManager>().prevGameState = FindObjectOfType<GameManager>().gameState;
                FindObjectOfType<GameManager>().gameState = 4;
            }
            else if (state == 1) 
            {
                MoveCamera(initPos, initRot);
                state = Mathf.Abs(state - 1);
                FindObjectOfType<GameManager>().prevGameState = FindObjectOfType<GameManager>().prevGameState;
                FindObjectOfType<GameManager>().gameState = 2;
            }
        }
    }

    void InitVariable()
    {
        initPos = FindObjectOfType<CameraController>().transform.position;
        initRot = FindObjectOfType<CameraController>().transform.rotation;
    }

    void MoveCamera(Vector3 _positon, Quaternion _rotation)
    {
        camera.NextPosition = _positon;
        camera.NextRotation = _rotation;
        camera.transform.rotation = _rotation;
    }

    public void ChangeState()
    {
        besoinClef = false;
    }
}
