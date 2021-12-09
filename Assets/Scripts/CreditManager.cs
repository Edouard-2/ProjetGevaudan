using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditManager : MonoBehaviour
{
    public List<GameObject> OtherObjct;

    public Animator Titre;
    public Animator KillianTitre;
    public Animator Killian;
    public Animator EdouardTitre;
    public Animator Edouard;
    public Animator SoundrTitre;
    public Animator Sound;
    public Animator Playtest;

    GameManager myGameManager;

    // Start is called before the first frame update
    void Start()
    {
        myGameManager = FindObjectOfType<GameManager>();
    }

    public void LaunchCredit()
    {
        myGameManager.gameState = -3;
        myGameManager.prevGameState = -3;

        foreach (GameObject item in OtherObjct)
        {
            item.SetActive(false);
        }
        Titre.gameObject.SetActive(true);
        KillianTitre.gameObject.SetActive(true);
        Killian.gameObject.SetActive(true);
        EdouardTitre.gameObject.SetActive(true);
        Edouard.gameObject.SetActive(true);
        SoundrTitre.gameObject.SetActive(true);
        Sound.gameObject.SetActive(true);
        Playtest.gameObject.SetActive(true);

    StartCoroutine(StartAnimationTitre());
    }

    IEnumerator StartAnimationTitre()
    {
        yield return new WaitForSeconds(3f);
        Titre.SetTrigger("open");

        yield return new WaitForSeconds(5f);
        Titre.SetTrigger("close");

        yield return new WaitForSeconds(2f);
        StartCoroutine(StartAnimationKillian());
    }
    IEnumerator StartAnimationKillian()
    {
        yield return new WaitForSeconds(3f);
        Killian.SetTrigger("open");
        KillianTitre.SetTrigger("open");

        yield return new WaitForSeconds(5f);
        Killian.SetTrigger("close");
        KillianTitre.SetTrigger("close");

        yield return new WaitForSeconds(2f);
        StartCoroutine(StartAnimationEdouard());
    }
    IEnumerator StartAnimationEdouard()
    {
        yield return new WaitForSeconds(3f);
        Edouard.SetTrigger("open");
        EdouardTitre.SetTrigger("open");

        yield return new WaitForSeconds(5f);
        Edouard.SetTrigger("close");
        EdouardTitre.SetTrigger("close");

        
        yield return new WaitForSeconds(2f);
        StartCoroutine(StartAnimationSound());
    }
    IEnumerator StartAnimationSound()
    {
        yield return new WaitForSeconds(3f);
        Sound.SetTrigger("open");
        SoundrTitre.SetTrigger("open");

        yield return new WaitForSeconds(5f);
        Sound.SetTrigger("close");
        SoundrTitre.SetTrigger("close");


        yield return new WaitForSeconds(2f);
        StartCoroutine(StartAnimationPlaytest());
    }
    IEnumerator StartAnimationPlaytest()
    {
        yield return new WaitForSeconds(3f);
        Playtest.SetTrigger("open");

        yield return new WaitForSeconds(5f);
        Playtest.SetTrigger("close");

        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("SplashScreen");
    }
}
