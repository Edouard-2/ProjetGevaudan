using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receptacle : MonoBehaviour
{
    public Transform empty;

    public void ActiveBlock()
    {
        gameObject.GetComponent<OpenTiroir>().ChangeState();
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
