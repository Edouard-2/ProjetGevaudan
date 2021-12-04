using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutLabyrinthDrag : MonoBehaviour
{
    public int state = 0;

    public bool readyClick = false;
    public bool up = false;

    public GameObject receptacle;

    private Vector2 initPoint;
    private Vector2 dir;

    // Start is called before the first frame update
    void Start()
    {
        initPoint = new Vector2(-10000, -100000);
    }

    //Si le joueur appuiy sur cette case
    private void OnMouseUp()
    {
        if (state > 1)
        {
            readyClick = true;
            initPoint = new Vector2(-10000, -100000);
            state = 1;
        }
    }

    //Deplacement de la case si il fait une distance pour déplacer la case
    private void OnMouseDrag()
    {
        if (state >= 1)
        {
            state = 2;

            if (initPoint == new Vector2(-10000, -100000))
            {
                initPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }

            //Calcul du point du doigt
            Vector2 curPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            float curDistance = Vector3.Distance(new Vector3(curPoint.x, curPoint.y, 0), new Vector3(initPoint.x, initPoint.y, 0));

            dir = (new Vector2(curPoint.x, curPoint.y) - new Vector2(initPoint.x, initPoint.y)).normalized;

            //Verif de la distance
            if (curDistance / 75 >= 1)
            {
                readyClick = false;
                initPoint = curPoint;
                ChoseDirection();
            }
        }
    }

    //Si le joueur click dessus
    private void OnMouseUpAsButton()
    {
        if (state >= 1 && readyClick)
        {
            state = 0;
            receptacle.GetComponent<Receptacle>().id = 0;
            gameObject.GetComponent<Drag>().readyDrag = true;
            FindObjectOfType<InteractifObject>().GoInventaire(gameObject.transform);
            print("récup");
        }
    }

    //Si la direction est positive ou negative
    public float CheckPositiveNegative(float _n)
    {
        if (_n > 0)
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
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
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
        float dirZ = 0;
        if( dir.y != 0)
        {
            dirZ = 0.72f * -dir.y;
        }
        if (Physics.Raycast(transform.position, new Vector3(dirZ, dir.y, dir.x), out hit, 0.15f))
        {
            Debug.DrawRay(transform.position, new Vector3(dirZ, dir.y, dir.x), Color.yellow, 3);
            print("Mur Bloqué");
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
        Vector3 pos = new Vector3(0, dir.y * 0.107f, dir.x * 0.13f);
        if (pos.y != 0)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - 0.08f * dir.y, gameObject.transform.position.y + pos.y, gameObject.transform.position.z + pos.z);
        }
        else
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + pos.y, gameObject.transform.position.z + pos.z);
        }
        VerifBonnePos();
    }

    //Verification si la case est sur une bonne case
    void VerifBonnePos()
    {
        
    }
}