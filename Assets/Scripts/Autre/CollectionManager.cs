using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CollectionManager : MonoBehaviour, IPointerClickHandler
{
    //Liste des objets UI a faire apparaitre
    public List<GameObject> uiListObj;

    public Sprite spriteState1;
    public Sprite spriteState2;

    [System.Obsolete]
    public void OnPointerClick(PointerEventData eventData)
    {
        if (uiListObj[0].active)
        {
            activeDeZoom();
            activeDesactive(false);
            FindObjectOfType<GameManager>().switchZoom(0);
        }
        else
        {
            DesactiveDeZoom();
            activeDesactive(true);

            FindObjectOfType<GameManager>().switchZoom(0);
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

        gameObject.GetComponent<Image>().sprite = spriteState2;

        foreach (var item in listObj)
        {
            item.state = false;
        }
    }
    void activeDeZoom()
    {
        IsScrolling[] listObj = FindObjectsOfType<IsScrolling>();

       
        gameObject.GetComponent<Image>().sprite = spriteState1;

        foreach (var item in listObj)
        {
            item.activeState();
        }
    }
}
