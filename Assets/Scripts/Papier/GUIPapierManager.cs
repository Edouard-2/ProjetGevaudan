using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIPapierManager : MonoBehaviour
{
    //Data pour savoir quelle papier doivent �tre activ�
    public PapierData papierData;

    //List de tt les papier UI
    public List<GameObject> listPapier;

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        //Lorsque de nouveau papier doivent �tre affich� ils pourront s'afficher dans l'ui
        for (int i = 0; i < 6; i++)
        {
            if ( papierData.boolPapier[i] && !listPapier[i].active )
            {
                //Afficher le papier
                Debug.Log(i);
                listPapier[i].SetActive(true);
            }
            else if ( !papierData.boolPapier[i] && listPapier[i].active)
            {
                //D�safficher le papier
                listPapier[i].SetActive(false);
            }
        }
    }
}
