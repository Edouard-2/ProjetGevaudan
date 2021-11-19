using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GUIPapierMovement : MonoBehaviour, IPointerClickHandler
{
    // Centre de l'ui
    public GameObject centre;

    //Manager des papier UI
    public GameObject GUIpapierManager;

    //autres
    public int state;

    public bool zoom = true;

    private Vector3 initPosition;
    public Vector3 initScale;

    private void Start()
    {
        //Initialisation des variables
        state = 0;
        initPosition = gameObject.transform.position;
        initScale = gameObject.transform.localScale;
    }

    //Si le joueur clique
    public void OnPointerClick(PointerEventData eventData)
    {
        //Verifié que aucun autre papier soit activé
        verifOtherPaper();

        //Mettre en avant
        if (state == 0 && zoom)
        {

            gameObject.transform.SetAsLastSibling();
            gameObject.transform.position = centre.transform.position;
            gameObject.transform.localScale = centre.transform.localScale;
        }

        //Reculer
        else
        {
            gameObject.transform.position = initPosition;
            gameObject.transform.localScale = initScale;
        }

        //Changement de state
        state = Mathf.Abs(state - 1);
    }

    public void verifOtherPaper()
    {
        //List des cases
        List<GameObject> listPapier = GUIpapierManager.GetComponent<GUIPapierManager>().listPapier;

        //Nombre de case
        int length = listPapier.Count;

        //Verifié pour tout les papier de la list si aucun n'est activé
        for (int i = 0; i < length; i++)
        {
            if ( listPapier[i].GetComponent<GUIPapierMovement>().state == 0)
            {
                zoom = true;
            }
            else
            {
                zoom = false;
                i = length;
            }
        }
    }
}
