using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rouage : MonoBehaviour
{
    public int id = 0;
    public string actualLetter;
    public List<string> letterList;
    private int indexLetter = 0;

    AudioSource myAudioSource;

    public bool solve = false;

    float rotateData = 60;

    float initPoint;

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = FindObjectOfType<ScryptTextManager>().GetComponent<AudioSource>();
        initPoint = -1000000;
        actualiseLetter();
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

            if (curDistance / 50 >= 1 )
            {
                initPoint = curPoint;
                rotate();
            }
        }
    }

    void switchLetter(int _nbr)
    {
        indexLetter += _nbr;
        if (indexLetter < 0)
        {
            indexLetter = 5;
        }
        else if (indexLetter > 5)
        { 
            indexLetter = 0;
        }
        
        actualiseLetter();
    }

    void actualiseLetter()
    {
        actualLetter = letterList[indexLetter];
        FindObjectOfType<ScryptTextManager>().verifClef();
    }

    public void rotate()
    {
        if( Input.GetAxis("Mouse Y") > 0)
        {
            switchLetter(-1);
            myAudioSource.Play();
            gameObject.transform.RotateAround(GetComponent<Renderer>().bounds.center, Vector3.right, rotateData);
        }
        else if( Input.GetAxis("Mouse Y") < 0)
        {
            switchLetter(1);
            myAudioSource.Play();
            gameObject.transform.RotateAround(GetComponent<Renderer>().bounds.center, Vector3.left, rotateData);
        }
    }
}
