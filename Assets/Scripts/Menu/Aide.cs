using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Aide : MonoBehaviour, IPointerClickHandler
{
    public int state = -1;

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
        //Initialisation des variables / objets / texte
        state = 0;

        textPatientez = Patientez.GetComponent<TextMeshProUGUI>();

        Aide1Obj.GetComponent<Animator>().SetTrigger("open");

        boutonCollection.SetActive(false);
        boutonAide.SetActive(false);

        Fond.SetActive(true);

        checkState();

        StartCoroutine(activeClickReady());
    }

    //Dès qu'on click sur l'objet
    public void OnPointerClick(PointerEventData eventData)
    {
        //Si le texte est en mode Passer
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

        //Si le joueur n'a pas attendu
        else
        {
            readyClick = true;
            StopAllCoroutines();
            ActiveClickText();
        }
    }

    //Changement de state
    void checkState()
    {
        //Lancement du menu aide
        if (state == 0)
        {
            Aide1Obj.SetActive(true);
            Aide2Obj.SetActive(false);
        }

        //Affichage de la deuxième aide
        else if (state == 1)
        {
            textPatientez.SetText("Veuillez Patienter");
            Aide1Obj.SetActive(false);
            Aide2Obj.SetActive(true);
        }

        //Fermeture de l'aide
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

        //Changement de la state du Jeu
        FindObjectOfType<GameManager>().switchZoom(-1);

        //Reactivation et désactivation de tout les objets necessaire
        boutonCollection.SetActive(false);
        boutonAide.SetActive(false);
        Fond.SetActive(true);
        Aide1Obj.SetActive(true);
        gameObject.SetActive(true);
        Patientez.SetActive(true);

        //Mettre le non texte pour Patienter
        textPatientez.SetText("Veuillez Patienter");

        //Lancement des animations
        Fond.GetComponent<Animator>().SetTrigger("close");
        Aide2Obj.GetComponent<Animator>().SetTrigger("close");
        Patientez.GetComponent<Animator>().SetTrigger("close");

        //Mettre le fond en non transparent
        Fond.GetComponent<Image>().color = new Color(16, 12, 12, 1);
        Aide2Obj.GetComponent<Image>().color = new Color(255, 255, 255, 1);

        //Lancer les fonction pour le menu aide
        checkState();
        StartCoroutine(activeClickReady());
    }

    IEnumerator activeClickReady()
    {
        //Activation du "Passer" pour le joueur
        yield return new WaitForSeconds(2f);
        ActiveClickText();
        readyClick = true;
    }

    //Mettre le texte Patientez en "Passer"
    void ActiveClickText()
    {
        textPatientez.SetText("Passer");
    }
}
