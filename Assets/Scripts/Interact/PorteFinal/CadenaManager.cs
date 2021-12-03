using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CadenaManager : MonoBehaviour
{
    public List<CadenaRouage> listCode;

    public GameObject porteFinale;
    public GameObject barreFer;

    public GameObject hitBoxCadena;

    public bool done = false;

    int correctCode = 124;

    // Start is called before the first frame update
    void Start()
    {
        verifCadena();
    }

    public void verifCadena()
    {

        if ((listCode[0].id*100 + listCode[1].id*10 + listCode[2].id) == correctCode)
        {
            foreach (CadenaRouage item in listCode)
            {
                item.solve = true;
                item.transform.SetParent(gameObject.transform);
            }

            StartCoroutine(activeAnimation());

            done = true;
            FindObjectOfType<CameraController>().moveCamera(hitBoxCadena.GetComponent<ScrollingManager>().cameraEmpty, true);

            hitBoxCadena.SetActive(false);
        }
    }

    IEnumerator activeAnimation()
    {
        FindObjectOfType<GameManager>().gameState = -1;
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponent<Animator>().SetTrigger("play");
        StartCoroutine(activePorteAnimation());
    }

    IEnumerator activePorteAnimation()
    {
        yield return new WaitForSeconds(1f);
        barreFer.GetComponent<Animator>().SetTrigger("play");
        yield return new WaitForSeconds(2f);
        porteFinale.GetComponent<Animator>().SetTrigger("play");
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<GameManager>().gameState = 2;
    }
}
