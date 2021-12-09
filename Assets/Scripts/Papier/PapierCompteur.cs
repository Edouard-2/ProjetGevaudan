using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PapierCompteur : MonoBehaviour
{
    public PapierData myPapierData;
    public int nbrPapierTrue;

    TextMeshProUGUI myTmp;
    // Start is called before the first frame update
    void Start()
    {
        nbrPapierTrue = 0;
        myTmp = GetComponent<TextMeshProUGUI>();
    }

    public void VerifDataText()
    {
        nbrPapierTrue++;
        UpdateText();
    }

    void UpdateText()
    {
        myTmp.SetText(nbrPapierTrue + "/6");
        GetComponent<Animator>().SetTrigger("open");
        StartCoroutine(hideAnimation());
    }

    IEnumerator hideAnimation()
    {
        yield return new WaitForSeconds(5f);
        GetComponent<Animator>().SetTrigger("hide");
    }
}