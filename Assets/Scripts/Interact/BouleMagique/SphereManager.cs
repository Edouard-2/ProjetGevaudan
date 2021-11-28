using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereManager : MonoBehaviour
{
    public bool bonnePos = false;

    public List<GameObject> listSphere;

    public void verifBonnePosition()
    {
        int _nbr = 0;
        foreach (GameObject item in listSphere)
        {
            if (item.GetComponent<SphereDrag>().solve == true)
            {
                _nbr++;
            }
            print(item.GetComponent<SphereDrag>().solve);
        }
        
        if( _nbr == 4)
        {
            foreach (GameObject item in listSphere)
            {
                item.GetComponent<SphereDrag>().block = true;
            }

            bonnePos = true;
            print("readyRecevoir");
        }
    }
}
