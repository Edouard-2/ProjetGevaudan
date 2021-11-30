using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Aide : MonoBehaviour, IPointerClickHandler
{
    private int state = -1;

    private bool readyClick = false;

    public GameObject Fond;

    public GameObject boutonCollection;
    public GameObject boutonAide;

    public GameObject Aide1Obj;
    public GameObject Aide2Obj;

    // Start is called before the first frame update
    void Start()
    {
        state = 0;

        Aide1Obj.GetComponent<Animator>().SetTrigger("open");

        boutonCollection.SetActive(false);
        boutonAide.SetActive(false);

        Fond.SetActive(true);

        checkState();

        StartCoroutine(activeClickReady());
    }

    private void Update()
    {
        if( state == 0)
        {

        }
        if( state == 1)
        {

        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
        if (readyClick)
        {
            print("ezf");
            readyClick = false;
            state++;
            checkState();
            if(state <= 1)
            {
                StartCoroutine(activeClickReady());
            }
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
            Aide1Obj.SetActive(false);
            Aide2Obj.SetActive(true);
        }
        else if (state > 1)
        {
            FindObjectOfType<GameManager>().switchZoom(0);
            boutonCollection.SetActive(true);
            boutonAide.SetActive(true);
            Fond.SetActive(false);
            gameObject.SetActive(false);
            Aide2Obj.SetActive(false);
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
        readyClick = true;
    }

}
