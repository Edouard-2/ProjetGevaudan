using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutLabyrinthDrag : MonoBehaviour
{
    public int state = 0;

    int idCentre = 0;
    public int goodIdFinal = 0;

    public bool readyClick = false;
    public bool up = false;

    public GameObject receptacle;

    public LabyrinthManager myLabyrinthManager;

    public BoutLabyrinthDrag autreBout1;
    public BoutLabyrinthDrag autreBout2;

    private Vector2 initPoint;
    private Vector2 dir;

    // Start is called before the first frame update
    void Start()
    {
        myLabyrinthManager = FindObjectOfType<LabyrinthManager>();
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
        if (state >= 1 && FindObjectOfType<GameManager>().gameState == 3)
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
        if (state >= 1 && readyClick && FindObjectOfType<GameManager>().gameState == 3)
        {
            state = 0;
            idCentre = 0;
            receptacle.GetComponent<Receptacle>().id = 0;
            gameObject.GetComponent<Drag>().readyDrag = true;
            gameObject.GetComponent<BoxCollider>().size = new Vector3(gameObject.GetComponent<BoxCollider>().size.x, gameObject.GetComponent<BoxCollider>().size.y, 2.2f);
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
        if (Physics.Raycast(transform.position, new Vector3(dirZ, dir.y, dir.x), out hit, 0.15f) )
        {
            if( hit.transform.tag == "Centre_Labyrinthe" || ( (idCentre == 2 || idCentre == 3 ) && dir.y == 1 )|| ( idCentre == 1 && dir.y == -1 ) )
            {
                print("centre");

                //Si il est a côté du centre
                if (idCentre == 0 && autreBout1.idCentre != 1 && 

                    //Verif des autres poistions des bouts
                    autreBout2.idCentre != 1)
                {
                    idCentre = 1;
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x - 0.01f * -1, gameObject.transform.position.y + -1 * 0.01f, gameObject.transform.position.z - 1 * 0.12f);

                }

                //Si il en haut et qu'il veut déscendre
                else if ( idCentre == 1  && dir.y == -1)
                {
                    if(autreBout1.idCentre != 2 && autreBout2.idCentre != 2)
                    {
                        idCentre = 2;

                        gameObject.transform.position = new Vector3(gameObject.transform.position.x - 0.06f * -1, gameObject.transform.position.y + -1 * 0.08f, gameObject.transform.position.z + -1 * 0.0475f);
                    }
                    else if (autreBout1.idCentre != 3 && autreBout2.idCentre != 3)
                    {
                        idCentre = 3;
                        gameObject.transform.position = new Vector3(gameObject.transform.position.x - 0.06f * -1, gameObject.transform.position.y + -1 * 0.08f, gameObject.transform.position.z + 1 * 0.0475f);
                    }
                }

                //Si il est en bas et qu'il veut monter
                else if ( ( idCentre == 2 || idCentre == 3 ) && dir.y == 1 &&

                    //Verif des autres poistions des bouts
                    autreBout1.idCentre != 1 && autreBout2.idCentre != 1)
                {
                    if(idCentre == 2)
                    {
                        gameObject.transform.position = new Vector3(gameObject.transform.position.x - 0.06f * 1, gameObject.transform.position.y + 1 * 0.08f, gameObject.transform.position.z + 1 * 0.0475f);

                    }
                    else
                    {
                        gameObject.transform.position = new Vector3(gameObject.transform.position.x - 0.06f * 1, gameObject.transform.position.y + 1 * 0.08f, gameObject.transform.position.z -1 * 0.0475f);
                    }

                    idCentre = 1;
                }

                //Si il est a en bas a gauche et qu'il veut aller a droite
                else if ( idCentre == 2 && dir.x == 1 &&

                    //Verif des autres poistions des bouts
                    autreBout1.idCentre !=3 && autreBout2.idCentre != 3)
                {
                    idCentre = 3;
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x , gameObject.transform.position.y , gameObject.transform.position.z +1 * 0.095f);

                }

                //Si il est a en bas a droite et qu'il veut aller a gauche
                else if (idCentre == 3 && dir.x == -1 &&

                    //Verif des autres poistions des bouts
                    autreBout2.idCentre != 2 && autreBout2.idCentre != 2)
                {
                    idCentre = 2;
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 1 * 0.095f);
                }
                print(idCentre);
                VerifBonnePos();
            }
            else
            {
                Debug.DrawRay(transform.position, new Vector3(dirZ, dir.y, dir.x), Color.yellow, 3);
                print("Mur Bloqué");
            }
        }
        else
        {
            if(idCentre == 1)
            {
                idCentre = 0;
                gameObject.transform.position = new Vector3(gameObject.transform.position.x - 0.01f * 1, gameObject.transform.position.y + 1 * 0.02f, gameObject.transform.position.z + 1 * 0.12f);

            }
            else
            {
                MoveCase(0);
            }
        }
    }

    //Bouger la case
    void MoveCase(float _i)
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
    }

    //Verification si la case est sur une bonne case
    void VerifBonnePos()
    {
        if (goodIdFinal == idCentre && autreBout1.goodIdFinal == autreBout1.idCentre && autreBout2.goodIdFinal == autreBout2.idCentre)
        {
            print("bravo");
            myLabyrinthManager.finishLabyrinth();
        }
    }
}