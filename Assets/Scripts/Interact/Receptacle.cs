using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receptacle : MonoBehaviour
{
    public Transform empty;

    public int id = 0;

    public void ActiveBlock()
    {
        gameObject.GetComponent<OpenTiroir>().ChangeState();
    }

    public void ActiveAnimationBureau()
    {
        FindObjectOfType<LabyrinthManager>().porteDessus.GetComponent<Animator>().SetTrigger("open");
    }

    public bool verifRond()
    {
        if( FindObjectOfType<SphereManager>().bonnePos )
        {
            FindObjectOfType<SphereManager>().addMorceau();
            return true;
        }
        else
        {
            return false;
        }
    }
}
