using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CollectionManager : MonoBehaviour, IPointerClickHandler
{
    //Liste des objets UI a faire apparaitre
    public List<GameObject> uiListObj;

    //Les différents sprite du bouton
    public Sprite spriteState1;
    public Sprite spriteState2;

    public GameObject boutonAide;

    [System.Obsolete]
    //Activer ou desactiver l''ui de la collection
    public void OnPointerClick(PointerEventData eventData)
    {
        if (uiListObj[0].active)
        {
            activeDeZoom();
            activeDesactive(false);
            boutonAide.SetActive(true);
            FindObjectOfType<GameManager>().switchZoom(0);
        }
        else
        {
            DesactiveDeZoom();
            activeDesactive(true);
            boutonAide.SetActive(false);
            FindObjectOfType<GameManager>().switchZoom(-1);
        }
    }

    //Activation ou desactivation de l'ui de la collection
    public void activeDesactive(bool _bool)
    {
        for (int i = 0; i < uiListObj.Count; i++)
        {
            uiListObj[i].SetActive(_bool);
        }
    }

    //Desactiver les zoom in game 
    void DesactiveDeZoom()
    {
        IsScrolling[] listObj = FindObjectsOfType<IsScrolling>();

        gameObject.GetComponent<Image>().sprite = spriteState2;

        foreach (var item in listObj)
        {
            item.state = false;
        }
    }

    //Activer les zoom in game 
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
