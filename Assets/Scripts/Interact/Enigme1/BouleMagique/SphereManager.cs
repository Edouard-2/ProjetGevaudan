using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereManager : MonoBehaviour
{
    public bool bonnePos = false;

    public int morceauNbr = 0;

    public List<GameObject> listSphere;

    public GameObject enimge1;

    public GameObject clef;
    public GameObject dent;

    public VoixManager myVoixManager;

    private void Start()
    {
        myVoixManager = FindObjectOfType<VoixManager>();
    }

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

    public void addMorceau()
    {
        morceauNbr++;

        if(morceauNbr == 3)
        {
            clef.transform.SetParent(enimge1.transform);
            dent.transform.SetParent(enimge1.transform);

            StartCoroutine(DesactiveAll());
        }
    }

    IEnumerator DesactiveAll()
    {
        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
        gameObject.GetComponent<BoxCollider>().enabled = false;
        foreach (GameObject item in listSphere)
        {
            item.SetActive(false);
            if (item.GetComponent<BoxCollider>())
            {
                item.GetComponent<BoxCollider>().enabled = false;
            }
            else
            {
                item.GetComponent<MeshCollider>().enabled = false;
            }
        }
        dent.GetComponent<BoxCollider>().enabled = true;
        clef.GetComponent<BoxCollider>().enabled = true;
        FindObjectOfType<InteractifObject>().GoInventaire(dent.transform);
        FindObjectOfType<InteractifObject>().GoInventaire(clef.transform);
        FindObjectOfType<InteractifObject>().flou.SetActive(false);
        FindObjectOfType<InteractifObject>().state = Mathf.Abs(FindObjectOfType<InteractifObject>().state - 1);

        myVoixManager.DeclencheDialogueIndice();
    }
}
