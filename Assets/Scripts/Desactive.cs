using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desactive : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Desactiver l'obj au demarage du jeu (son renderer pour ne pas le voir)
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
}
