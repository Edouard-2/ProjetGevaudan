using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollingManager : MonoBehaviour, IPointerDownHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        if (FindObjectOfType<GameManager>().gameState == 1)
        {
            IsScrolling[] listScroll = FindObjectsOfType<IsScrolling>();

            foreach (IsScrolling item in listScroll)
            {
                item.stateClick(true, Input.mousePosition);
            }
        }
    }
}