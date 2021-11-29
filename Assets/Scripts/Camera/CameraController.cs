using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 initPosition;
    Vector3 startPosition;
    public Vector3 NextPosition;
    Quaternion initRotation;
    public Quaternion NextRotation;

    private float rotSpeed = 10f;
    private float posSpeed = 0.5f;
    
    private float initPoint = -1000000;

    public bool readyRotate = false;

    public bool readyClick = false;

    private void Start()
    {
        initPosition = transform.position;
        startPosition = transform.position;
        initRotation = transform.rotation;
        NextPosition = Vector3.zero;
    }

    private void Update()
    {
        //Si la position est différent de la prochaine pos
        if (NextPosition != Vector3.zero && gameObject.transform.position != NextPosition )
        {
            if(FindObjectOfType<GameManager>().gameState == 1 )
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, startPosition, posSpeed);
                gameObject.transform.rotation = NextRotation;
            }
            else 
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, NextPosition, posSpeed);
                gameObject.transform.rotation = NextRotation;
            }

        }

        bool ready = VerifInitState();

        if( FindObjectOfType<GameManager>().gameState == 1 && ready)
        {
            DeplacementCamera();
            gameObject.transform.position = startPosition;
            NextPosition = Vector3.zero;
        }

        if (Input.touchCount == 0)
        {
            readyClick = false;
        }
    }

    bool VerifInitState()
    {
        InitData[] listInit = FindObjectsOfType<InitData>();
        bool ready = true;

        for (int i = 0; i < listInit.Length; i++)
        {
            if(listInit[i].state == 1)
            {
                ready = false;
            }
        }
        return ready;
    }

    //Changer la prochain position ou revenir en arrière avec la camera
    public void moveCamera(GameObject _hit, bool _switch)
    {
        if (_switch == true)
        {
            initVariables();
            NextPosition = _hit.transform.position;
            NextRotation = _hit.transform.rotation;
        }
        else
        {
            NextPosition = initPosition;
            NextRotation = initRotation;
        }
    }

    //Initialiser les variable d'avant mouvement
    void initVariables()
    {
        initPosition = transform.position;
        initRotation = transform.rotation;
        NextPosition = Vector3.zero;
    }

    //Faire touner la camera sur elle meme si il est au centre de la pièce
    void DeplacementCamera()
    {
        
        if (Input.GetMouseButton(0) && FindObjectOfType<InventaireManager>().state == 0 )
        {
        
            if (initPoint == -1000000)
            {
                initPoint = Input.mousePosition.x;
            }

            //Calcul du point du doigt
            float curPoint = Input.mousePosition.x;

            float angle = Vector3.Angle(new Vector3 (initPoint,0,0), new Vector3(curPoint, 0, 0));

            float curDistance = Vector3.Distance(new Vector3(curPoint, 0, 0), new Vector3(initPoint, 0, 0));

            if (curDistance / 50 >= 1 || readyRotate )
            {
                readyRotate = true;
                initPoint = curPoint;
                rotate();
            }

        }

        else if (Input.GetMouseButtonUp(0))
        {
            initPoint = -1000000;
            readyRotate = false;
        }
    }

    //Faire la rotation
    void rotate()
    {
        float rotateData = Input.GetAxis("Mouse X") * 2;

        transform.Rotate(0, -rotateData, 0 ,Space.World);

    }
}
