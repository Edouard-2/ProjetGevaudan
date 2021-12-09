using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAnim : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        if(FindObjectOfType<GameManager>().gameState == 2)
        {
            GetComponent<Animator>().SetTrigger("open");
            GetComponent<AudioSource>().Play();
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}