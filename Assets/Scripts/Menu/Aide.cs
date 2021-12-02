using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Aide : MonoBehaviour, IPointerClickHandler
{
    private int state = -1;

    private bool readyClick = false;

    public GameObject Fond;

    public GameObject boutonCollection;
    public GameObject boutonAide;

    public GameObject Aide1Obj;
    public GameObject Aide2Obj;

    public GameObject Patientez;
    private TextMeshProUGUI textPatientez;

    // Start is called before the first frame update
    void Start()
    {
        state = 0;

        textPatientez = Patientez.GetComponent<TextMeshProUGUI>();

        Aide1Obj.GetComponent<Animator>().SetTrigger("open");

        boutonCollection.SetActive(false);
        boutonAide.SetActive(false);

        Fond.SetActive(true);

        checkState();

        StartCoroutine(activeClickReady());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
        if (readyClick)
        {
            print("ezf");
            readyClick = false;
            state++;
            checkState();
            Aide1Obj.GetComponent<Image>().color = new Color(255, 255, 255, 1);
            Fond.GetComponent<Image>().color = new Color(16, 12, 12, 1);
            if (state <= 1)
            {
                StartCoroutine(activeClickReady());
            }
        }
        else
        {
            readyClick = true;
            StopAllCoroutines();
            ActiveClickText();
        }
    }

    void checkState()
    {
        if (state == 0)
        {
            Aide1Obj.SetActive(true);
            Aide2Obj.SetActive(false);
        }
        else if (state == 1)
        {
            textPatientez.SetText("Veuillez Patienter");
            Aide1Obj.SetActive(false);
            Aide2Obj.SetActive(true);
        }
        else if (state > 1)
        {
            FindObjectOfType<GameManager>().switchZoom(0);
            boutonCollection.SetActive(true);
            boutonAide.SetActive(true);
            gameObject.SetActive(false);

            Aide2Obj.GetComponent<Animator>().SetTrigger("open");
            Fond.GetComponent<Animator>().SetTrigger("open");
            Patientez.GetComponent<Animator>().SetTrigger("open");
        }
    }

    public void ClickAideButton()
    {
        readyClick = false;
        state = 0;
        FindObjectOfType<GameManager>().switchZoom(-1);
        boutonCollection.SetActive(false);
        boutonAide.SetActive(false);
        Fond.SetActive(true);
        gameObject.SetActive(true);
        Aide1Obj.SetActive(true);
        StartCoroutine(activeClickReady());
    }

    IEnumerator activeClickReady()
    {
        yield return new WaitForSeconds(2f);
        ActiveClickText();
        readyClick = true;
    }

    void ActiveClickText()
    {
        textPatientez.SetText("Passer");
    }
}
