using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CadenaRouage : MonoBehaviour
{
    public int id = 0;

    public bool solve = false;

    float rotateData = 36;

    float initPoint;

    // Start is called before the first frame update
    void Start()
    {
        initPoint = -1000000;
        verifCode();
    }

    private void OnMouseDrag()
    {
        if (FindObjectOfType<GameManager>().gameState == 3 && !solve)
        {
            if (initPoint == -1000000)
            {
                initPoint = Input.mousePosition.y;
            }

            //Calcul du point du doigt
            float curPoint = Input.mousePosition.y;

            float curDistance = Vector3.Distance(new Vector3(0, curPoint, 0), new Vector3(0, initPoint, 0));

            if (curDistance / 70 >= 1)
            {
                initPoint = curPoint;
                rotate();
            }
        }
    }

    void addNumber(int _nbr)
    {
        id += _nbr;

        if (id < 0)
        {
            id = 9;
        }
        else if (id > 9)
        {
            id = 0;
        }

        verifCode();
    }

    void verifCode()
    {
        FindObjectOfType<CadenaManager>().verifCadena();
    }

    public void rotate()
    {
        if (Input.GetAxis("Mouse Y") > 0)
        {
            addNumber(-1);
            gameObject.transform.RotateAround(GetComponent<Renderer>().bounds.center, Vector3.right, rotateData);
        }
        else if (Input.GetAxis("Mouse Y") < 0)
        {
            addNumber(1);
            gameObject.transform.RotateAround(GetComponent<Renderer>().bounds.center, Vector3.left, rotateData);
        }
    }
}
