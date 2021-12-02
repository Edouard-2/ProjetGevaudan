using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoixManager : MonoBehaviour
{
    public TextVoixData myVoixData;

    private Animator myAnimator;

    private string actualDialogue;

    private int indexIndice = 0;
    private int indexEnigme= 0;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = gameObject.GetComponent<Animator>();
    }

    //Declencher le dialogue de l'enigme _i
    public void DeclencheDialogueEnigme(int _i)
    {
        if(_i == 4)
        {
            //Lancer dialogue D
            actualDialogue = myVoixData.dialEnigme[_i];
            StartCoroutine(DisplayDialogue(actualDialogue));
            indexEnigme--;
        }
        else if( indexEnigme == 0 || indexEnigme == 2)
        {
            //Lancer dialogue A
            LancementDial(0);
        }
        else if( indexEnigme == 1)
        {
            if( _i == 1 || _i == 2 && !myVoixData.VerifB)
            {
                //Lancer dialogue B
                LancementDial(0);
            }
            else if (_i == 3 && !myVoixData.VerifC)
            {
                //Lancer dialogue C
                LancementDial(1);

            }
        }
        indexEnigme++;
    }

    //Initialisation du dialogue choisis et lancement de l'affichage
    private void LancementDial(int _i)
    {
        //Actualisation du dialogue actuel
        actualDialogue = myVoixData.dialEnigme[_i];

        StartCoroutine(DisplayDialogue(actualDialogue));
        //Enlever de la list des Dialogues Enigmes
        myVoixData.dialEnigme.Remove(myVoixData.dialEnigme[_i]);
    }

    //Lancer le changement de dialogue Indice
    public void DeclencheDialogueIndice()
    {
        indexIndice++;
        actualDialogue = myVoixData.dialIndice[indexIndice];
        StartCoroutine(DisplayDialogue(actualDialogue));
    }

    //Affichage du Dialogue Indice avec un peut de délai
    IEnumerator DisplayDialogue(string dialogue)
    {
        yield return new WaitForSeconds(2f);
        print(dialogue);
    }
}
