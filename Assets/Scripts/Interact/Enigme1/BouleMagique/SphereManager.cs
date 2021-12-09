using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereManager : MonoBehaviour
{
    public bool bonnePos = false;

    public int morceauNbr = 0;

    public List<GameObject> listSphere;

    public GameObject enimge1;

    public AudioSource myAudioFinal;
    public AudioSource myAudioSemi;

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
            StartCoroutine(BonneEmplacement());
            myAudioSemi.Play();
            bonnePos = true;
            print("readyRecevoir");
        }
    }

    IEnumerator BonneEmplacement()
    {
        yield return new WaitForSeconds(0.1f);
        foreach (GameObject item in listSphere)
        {
            item.GetComponent<SphereDrag>().block = true;

            item.transform.localRotation = Quaternion.Euler(new Vector3(-123.569f, 0, 0));
            item.transform.localPosition = Vector3.zero;
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
        myAudioFinal.Play();
        yield return new WaitForSeconds(0.5f);

        gameObject.transform.position = new Vector3(3000, 3000, 3000);
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

        FindObjectOfType<CadenaManager>().addIndexIndice();

        myVoixManager.DeclencheDialogueIndice();
    }
}
