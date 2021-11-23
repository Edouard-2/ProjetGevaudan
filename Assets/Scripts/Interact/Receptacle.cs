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
}
