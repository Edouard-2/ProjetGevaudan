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
            activeDeZoom();
            activeDesactive(false);
            FindObjectOfType<GameManager>().switchZoom();
        }
        else
        {
            DesactiveDeZoom();
            activeDesactive(true);

            FindObjectOfType<GameManager>().switchZoom();
        }
    }

    public void activeDesactive(bool _bool)
    {
        for (int i = 0; i < uiListObj.Count; i++)
        {
            uiListObj[i].SetActive(_bool);
        }
    }
    void DesactiveDeZoom()
    {
        IsScrolling[] listObj = FindObjectsOfType<IsScrolling>();

        foreach (var item in listObj)
        {
            item.state = false;
        }
    }
    void activeDeZoom()
    {
        IsScrolling[] listObj = FindObjectsOfType<IsScrolling>();

        foreach (var item in listObj)
        {
            item.activeState();
        }
    }
}
