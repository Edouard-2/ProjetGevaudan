using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScryptTextManager : MonoBehaviour
{
    public List<Rouage> listRouage;

    public GameObject porteDroite;
    public GameObject porteGauche;

    public GameObject hitBoxActive;
    public GameObject hitBoxDesactive;

    public GameObject camera;

    public GameObject antiHit;

    string rouageWord;
    string correctWord = "MANDEMENT";

    // Start is called before the first frame update
    void Start()
    {
        verifClef();
        print(rouageWord);
    }


    public void verifClef()
    {
        rouageWord = "";
        //mandement
        for (int i = 0; i < listRouage.Count; i++)
        {
            rouageWord = rouageWord + listRouage[i].actualLetter;
            
        }
        print(rouageWord);

        if ( rouageWord == correctWord)
        {
            print("correct : " + rouageWord);

            Rouage[] rouageList = FindObjectsOfType<Rouage>();

            for (int i = 0; i < rouageList.Length; i++)
            {
                rouageList[i].solve = true;
                rouageList[i].transform.SetParent(gameObject.transform);
            }

            StartCoroutine(activeAnimation());
            
            hitBoxActive.SetActive(true);
            hitBoxDesactive.SetActive(false);
            antiHit.SetActive(false);

            FindObjectOfType<CameraController>().moveCamera(hitBoxActive.GetComponent<ScrollingManager>().cameraEmpty, true);
            
        }
    }

    IEnumerator activeAnimation()
    {
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<Animator>().SetTrigger("open");
        StartCoroutine(activePorteAnimation());
    }

    IEnumerator activePorteAnimation()
    {
        yield return new WaitForSeconds(2f);
        porteGauche.GetComponent<Animator>().SetTrigger("open");
        yield return new WaitForSeconds(0.1f);
        porteDroite.GetComponent<Animator>().SetTrigger("open");
    }
}
