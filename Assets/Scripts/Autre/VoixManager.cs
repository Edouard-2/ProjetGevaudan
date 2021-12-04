using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VoixManager : MonoBehaviour
{
    public TextVoixData VoixData;

    public GameObject fond;

    public List<string> voixListIndice;
    public List<string> voixListEnigme;

    private Animator myAnimator;
    private Animator fondAnimator;

    private string actualDialogue;
    private int state = 0;

    private int indexIndice = 0;
    private int indexEnigme = 0;

    // Start is called before the first frame update
    void Start()
    {
        VoixData.VerifA = false;
        VoixData.VerifB = false;
        VoixData.VerifC = false;
        VoixData.VerifD = false;

        VoixData.VerifEnigme1 = false;
        VoixData.VerifEnigme2 = false;
        VoixData.VerifEnigme3 = false;
        VoixData.VerifEnigme3Bis = false;

        voixListIndice = new List<string>(VoixData.dialIndice);
        voixListEnigme = new List<string>(VoixData.dialEnigme);

        fondAnimator = fond.GetComponent<Animator>();
        myAnimator = gameObject.GetComponent<Animator>();
    }

    //Declencher le dialogue de l'enigme _i
    public void DeclencheDialogueEnigme(int _i)
    {
        if( state != 0)
        {

            StartCoroutine(waitForNextDialEnigme(_i));
        }
        else
        {
            if (_i == 4 && !VoixData.VerifEnigme3Bis)
            {
                VoixData.VerifEnigme3Bis = true;
                //Lancer dialogue D
                LancementDial(voixListEnigme.Count - 1);
            }
            else if (_i != 4)
            {
                if ((indexEnigme == 0 && !VoixData.VerifA) || indexEnigme == 2)
                {
                    if (_i == 1)
                    {
                        VoixData.VerifEnigme1 = true;
                    }
                    else if (_i == 2)
                    {
                        VoixData.VerifEnigme2 = true;
                    }
                    else
                    {
                        VoixData.VerifEnigme3 = true;
                    }
                    //Lancer dialogue A
                    VoixData.VerifA = true;
                    LancementDial(0);
                    indexEnigme++;
                }
                else if (indexEnigme == 1)
                {
                    if (((_i == 1 && !VoixData.VerifEnigme1) || (_i == 2 && !VoixData.VerifEnigme2)) && !VoixData.VerifB)
                    {
                        if (_i == 1)
                        {
                            VoixData.VerifEnigme1 = true;
                        }
                        else
                        {
                            VoixData.VerifEnigme2 = true;
                        }

                        //Lancer dialogue B
                        VoixData.VerifB = true;
                        LancementDial(0);
                        indexEnigme++;
                    }
                    else if (_i == 3 && !VoixData.VerifC && !VoixData.VerifEnigme3)
                    {
                        //Lancer dialogue C
                        VoixData.VerifC = true;
                        VoixData.VerifEnigme3 = true;
                        LancementDial(1);
                        indexEnigme++;

                    }
                }
            }
        }
    }

    //Initialisation du dialogue choisis et lancement de l'affichage
    private void LancementDial(int _i)
    {
        state++;
        //Actualisation du dialogue actuel
        actualDialogue = voixListEnigme[_i];

        StartCoroutine(DisplayDialogue(actualDialogue));
        //Enlever de la list des Dialogues Enigmes
        voixListEnigme.Remove(voixListEnigme[_i]);
    }

    //Lancer le changement de dialogue Indice
    public void DeclencheDialogueIndice()
    {
        if (state != 0)
        {
            StartCoroutine(waitForNextDialIndice());
        }
        else
        {
            state++;
            indexIndice++;
            actualDialogue = voixListIndice[indexIndice];
            StartCoroutine(DisplayDialogue(actualDialogue));
        }
    }

    IEnumerator waitForNextDialEnigme(int _i)
    {
        yield return new WaitForSeconds(15f * state);
        DeclencheDialogueEnigme(_i);
    }
    
    IEnumerator waitForNextDialIndice()
    {
        yield return new WaitForSeconds(15f * state);
        DeclencheDialogueIndice();
    }
    //Affichage du Dialogue Indice avec un peut de délai
    IEnumerator DisplayDialogue(string dialogue)
    {
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<TextMeshProUGUI>().SetText(dialogue);
        fondAnimator.SetTrigger("open");
        fond.GetComponent<Image>().color = new Color(255, 255, 255, 1);
        myAnimator.SetTrigger("open");
        StartCoroutine(HideDialogue());
        print(dialogue);
    }

    IEnumerator HideDialogue()
    {
        yield return new WaitForSeconds(12f);
        fondAnimator.SetTrigger("close");
        fond.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        myAnimator.SetTrigger("close");
        state = 0;
    }
}
