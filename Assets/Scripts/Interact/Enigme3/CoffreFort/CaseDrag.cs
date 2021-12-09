using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseDrag : MonoBehaviour
{
    public CoffreFortManager myCoffreManager;

    public AudioSource myAudioSource;

    public Vector2 initPoint;

    Vector2 dir;

    public bool win = false;

    public GameObject emptyWin;

    private void Start()
    {
        initPoint = new Vector2(-10000, -100000);
        myCoffreManager = FindObjectOfType<CoffreFortManager>();
    }

    //Si le joueur appuiy sur cette case
    private void OnMouseUp()
    {
        if (myCoffreManager.state == 1)
        {
            initPoint = new Vector2(-10000, -100000);
        }
    }

    //Deplacement de la case si il fait une distance pour déplacer la case
    private void OnMouseDrag()
    {
        if (myCoffreManager.state == 1)
        {
            if (initPoint == new Vector2(-10000,-100000))
            {
                initPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }

            //Calcul du point du doigt
            Vector2 curPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            float curDistance = Vector3.Distance(new Vector3(curPoint.x, curPoint.y, 0), new Vector3(initPoint.x, initPoint.y, 0));

            dir = (new Vector2(curPoint.x, curPoint.y) - new Vector2(initPoint.x, initPoint.y)).normalized;

            //Verif de la distance
            if (curDistance / 150 >= 1)
            {
                initPoint = curPoint;
                ChoseDirection();
            }
        }
    }

    //Si la direction est positive ou negative
    public float CheckPositiveNegative(float _n)
    {
        if(_n > 0)
        {
            _n = 1;
        }
        else
        {
            _n = -1;
        }

        return _n;
    }

    //Selectionner la direction
    void ChoseDirection() 
    {
        if( Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            dir = new Vector2(CheckPositiveNegative(dir.x), 0);
        }
        else
        {
            dir = new Vector2(0, CheckPositiveNegative(dir.y));
        }

        RaycastCheckBordure();
        
    }

    //Regarde si y a une bordure pour deplacer la case ou pas
    void RaycastCheckBordure()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, new Vector3(dir.x, dir.y, 0), out hit, 0.1f))
        {
                print(hit.transform.tag);
        }
        else
        {
            print("oiuherggnerqg");
            MoveCase();
        }
    }

    //Bouger la case
    void MoveCase()
    {
        launchAudio();
        Vector3 pos = new Vector3(dir.x * 0.09f, dir.y * 0.08f, 0);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + pos.x, gameObject.transform.position.y + pos.y, gameObject.transform.position.z);
        VerifBonnePos();
    }

    //Verification si la case est sur une bonne case
    void VerifBonnePos()
    {
        if( emptyWin.transform.position == gameObject.transform.position)
        {
            print("gagné");
            win = true;
            myCoffreManager.VerifOuverture();
        }
        else
        {
            win = false;
        }
    }

    void launchAudio()
    {

        if (myAudioSource.time > 0.2f || !myAudioSource.isPlaying)
        {
            myAudioSource.Play();
        }
    }
}
