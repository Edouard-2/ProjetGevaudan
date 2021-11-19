using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CollectionManager : MonoBehaviour, IPointerClickHandler
{
    //Liste des objets UI a faire apparaitre
    public List<GameObject> uiListObj;

    [System.Obsolete]
    public void OnPointerClick(PointerEventData eventData)
    {
        if (uiListObj[0].active)
        {
            activeDesactive(false);
            FindObjectOfType<GameManager>().gameState = FindObjectOfType<GameManager>().prevGameState;
        }
        else
        {
            activeDesactive(true);
            FindObjectOfType<GameManager>().gameState = 0;
        }
    }

    public void activeDesactive(bool _bool)
    {
        for (int i = 0; i < uiListObj.Count; i++)
        {
            uiListObj[i].SetActive(_bool);
        }
    }
}
