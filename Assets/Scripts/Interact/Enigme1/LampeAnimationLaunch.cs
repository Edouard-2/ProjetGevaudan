using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampeAnimationLaunch : MonoBehaviour
{
    int state = 0;
    private void OnMouseUpAsButton()
    {
        if(state == 0)
        {
            state++;
            GetComponent<Animator>().SetTrigger("open");
        }
    }
}
