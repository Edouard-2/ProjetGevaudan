using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereDrag : MonoBehaviour
{
    public int id;

    public bool solve = false;

    public bool block = false;

    public float rotateData;

    float initPoint;

    public SphereManager mySphereManager;

    public InitData myInitData;
    public int goodId;

    // Start is called before the first frame update
    void Start()
    {
        initPoint = -1000000;
        mySphereManager = FindObjectOfType<SphereManager>();
    }

    private void OnMouseDrag()
    {
        if (FindObjectOfType<GameManager>().gameState == 2 && myInitData.state == 1 && !block)
        { 
            if (initPoint == -1000000)
            {
                initPoint = Input.mousePosition.y;
            }

            //Calcul du point du doigt
            float curPoint = Input.mousePosition.y;

            float curDistance = Vector3.Distance(new Vector3(0, curPoint, 0), new Vector3(0, initPoint, 0));

            if (curDistance / 50 >= 1)
            {
                initPoint = curPoint;
                rotate();
            }
        }
    }

    public void checkBonneEmplacement()
    {
        if(id == goodId)
        {
            print(name+" gagne");
            solve = true;
            mySphereManager.verifBonnePosition();
        }
    }

    public void rotate()
    {
        if (Input.GetAxis("Mouse Y") > 0)
        {
            id++;
            gameObject.transform.RotateAround(GetComponent<MeshRenderer>().bounds.center, Vector3.back, rotateData);
        }

        else if (Input.GetAxis("Mouse Y") < 0)
        {
            id--;
            gameObject.transform.RotateAround(GetComponent<MeshRenderer>().bounds.center, Vector3.forward, rotateData);
        }

        verifOverId();

        checkBonneEmplacement();
        gameObject.transform.localPosition = Vector3.zero;
    }

    void verifOverId()
    {
        if(id > 12)
        {
            id = 1;
        }
        if(id < 1)
        {
            id = 12;
        }
    }
}
