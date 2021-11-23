using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTiroir : MonoBehaviour
{
    public bool besoinClef;

    public int state = 0;

    private void OnMouseUpAsButton()
    {
        if( FindObjectOfType<GameManager>().gameState == 2 && !besoinClef && state == 0)
        {
            print(name);
            GetComponent<Animator>().SetTrigger("open");
            state = 1;
            gameObject.GetComponent<ScrollingManager>().enabled = true;
        }
    }

    void changeState()
    {
        besoinClef = false;
    }
}
