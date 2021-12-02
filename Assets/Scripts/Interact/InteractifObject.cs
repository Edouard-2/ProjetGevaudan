using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractifObject : MonoBehaviour
{
    //Speed de mouvement
    public float speedPosition = 0.1f;

    public int state = 0;

    public bool activeMovement = false;

    //Facteur de scale petit / grand
    public float facteurGrand;
    public float facteurPetit;

    //Facteur item (scale)
    public Vector3 facteurItem = new Vector3(1,1,1);

    public GameObject myZoomLight;

    //Facteur zoom (scale)
    private Vector3 facteur;

    //Position / scale / rotation initiale
    public Vector3 initPosition;
    private Vector3 initScale;
    public Quaternion initRotation;

    //Position front camera
    public GameObject positionFront;

    //Fond Flou
    public GameObject flou;

    //obj actuellement en zoom (cliquer)
    public Transform curObject;

    //Ready inventaire
    public bool ReadyInventaire = true;

    public InventaireManager myInventaireManager;

    private void Awake()
    {
        myInventaireManager = FindObjectOfType<InventaireManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        curObject = transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Calcul du raycast
        RaycastHit();

        //Si l'obj vien d'arriver
        if (state == 1 && curObject.position != positionFront.transform.position)
        {
            Zoom();
        }

        //Si l'obj vne vas pas dans l'inventaire et retourne donc a sa position de base
        else if (state == 0 && curObject.position != initPosition && curObject.transform.tag != "Petit_Inventaire" && curObject.transform.tag != "Petit_Drag" && curObject.transform.tag != "croix")
        {
            DeZoom();
        }

        //Si l'obj vien d'arriver et qu'il peut aller dans l'inventaire
        else if (state == 0 && curObject.position != initPosition && ReadyInventaire && curObject != transform )
        {
            
            GoInventaire(curObject);
        }

    }

    //Verifier si quelle facteur prendre
    public void verifTag(Transform _hit)
    {
        
        if ( _hit.tag == "Grand" )
        {
            Debug.Log(_hit.tag);
            facteur = new Vector3(facteurGrand, facteurGrand, facteurGrand);
        }
        else if( _hit.tag == "Petit" || _hit.tag == "Petit_Inventaire" || _hit.tag == "Petit_Drag" || _hit.tag == "croix" )
        {
            facteur = new Vector3(facteurPetit, facteurPetit, facteurPetit);
        }
    }

    //Initialisation des nouvelle variable init de l'obj choisis
    public void initVariables(Transform _hit)
    {
        if( state == 0)
        {
            curObject = _hit;
            initPosition = curObject.GetComponent<InitData>().initPosition;
            initScale = curObject.GetComponent<InitData>().facteur;
            initRotation = curObject.GetComponent<InitData>().initRotation;
        }
    }

    //Zoom
    public void Zoom()
    {
        curObject.position = Vector3.MoveTowards(curObject.position, positionFront.transform.position, Vector3.Distance(curObject.position, positionFront.transform.position) / speedPosition * Time.deltaTime);
        if(curObject.tag != "croix")
        {
            curObject.localScale = Vector3.MoveTowards(curObject.localScale, curObject.GetComponent<InitData>().facteur * 0.05f, Vector3.Distance(curObject.localScale, curObject.GetComponent<InitData>().facteur) / speedPosition * Time.deltaTime);
        }
        else
        {
            curObject.localScale = Vector3.MoveTowards(curObject.localScale, curObject.GetComponent<InitData>().facteur, Vector3.Distance(curObject.localScale, curObject.GetComponent<InitData>().facteur) / speedPosition * Time.deltaTime);
        }
        StartCoroutine(SecondStateInitData());
    }

    //DeZoom
    public void DeZoom()
    {
        curObject.position = Vector3.MoveTowards(curObject.position, initPosition, Vector3.Distance(curObject.position, initPosition) / speedPosition * Time.deltaTime);
        curObject.localScale = Vector3.MoveTowards(curObject.localScale, initScale, Vector3.Distance(curObject.localScale, initScale) / speedPosition * Time.deltaTime);
        if(curObject.GetComponent<InitData>().rotSpeed != 0)
        {
            curObject.rotation = initRotation;
        }
        StartCoroutine(InitStateInitData());
    }

    IEnumerator SecondStateInitData()
    {
        yield return new WaitForSeconds(0.1f);

        curObject.GetComponent<InitData>().state = 1;
    }
    IEnumerator InitStateInitData()
    {
        yield return new WaitForSeconds(0.1f);

        curObject.GetComponent<InitData>().state = 0;
    }
    
    IEnumerator switchStateInventaireObj(Transform _curObject)
    {
        yield return new WaitForSeconds(0.1f);

        _curObject.GetComponent<InitData>().state = 2;
    }

    //Mettre l'obj dans l'inventaire 
    public void GoInventaire(Transform _curObject)
    {
        int id;

        //Creation de l'id du joueur
        if(_curObject.GetComponent<InitData>().id != -1)
        {
            //Initialisation de l'id du joueur
            id = _curObject.GetComponent<InitData>().id;
            myInventaireManager.listObj[_curObject.GetComponent<InitData>().id] = _curObject.gameObject;
        }
        else
        {
            //Ajouter l'inventaire a la list
            int lenght = myInventaireManager.listObj.Length;
            print(lenght);
            bool insert = true;
            for (int i = 0; i < lenght; i++)
            {
                print(myInventaireManager.listObj[i]);
                if ( !myInventaireManager.listObj[i] && insert)
                {
                    print("réussi");
                    insert = false;
                    myInventaireManager.listObj[i] = _curObject.gameObject;

                    _curObject.GetComponent<InitData>().id = i;
                    
                }
            }
            print(_curObject.GetComponent<InitData>().id);

            id = _curObject.GetComponent<InitData>().id;
        }
        
        //Le changer d'état
        StartCoroutine(switchStateInventaireObj(_curObject));
        
        //La bonne rotation
        _curObject.rotation = _curObject.GetComponent<InitData>().initRotation;

        //Le placer dans la case assigné
        _curObject.position = myInventaireManager.emplacementItems[id].transform.position;
        
        //Le mettre dans l'empty de la case a l'interieur de l'inventaire
        _curObject.parent = myInventaireManager.emplacementItems[id].transform;
        
        //Lui mettre la bonne taille
        _curObject.localScale = _curObject.GetComponent<InitData>().facteurItem;

        //Verifier les collider des obj
        myInventaireManager.verifColliderObj();

        if( _curObject.tag == "croix")
        {
            print("croixInventaire");
            FindObjectOfType<CroixManager>().AddCroixEmpty(_curObject.gameObject);
        }

        ReadyInventaire = false;
    }


    public void RaycastHit()
    {
        //Lorsque le bouton gauche est appuyé
        if (Input.GetMouseButtonDown(0) )
        {
            
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                
                //Si c'est un obj qui ne va pas dans l'inventaire
                if ( (hit.transform.tag == "Grand" || hit.transform.tag == "Petit" ) && state == 0 && FindObjectOfType<GameManager>().gameState != 1 && FindObjectOfType<GameManager>().gameState != 0)
                {
                    print("1");
                    CheckMovement(hit.transform);
                }

                //Si il va dans l'inventaire
                else if ( ( hit.transform.tag == "Petit_Inventaire" || hit.transform.tag == "Petit_Drag" || hit.transform.tag == "croix") && hit.transform.GetComponent<InitData>().state == 0 && FindObjectOfType<GameManager>().gameState != 1 && FindObjectOfType<GameManager>().gameState != 0)
                {
                    print("2");
                    CheckMovement(hit.transform);
                    ReadyInventaire = true;
                }

                //Si c'est le flou arrière
                if ( state == 1 && hit.transform.tag == "Flou" && FindObjectOfType<GameManager>().gameState != 0)
                {
                    print("flou");
                    ReadyInventaire = false;
                    //Si c'est un obj de inventaire les ombre porté sont toujours désactivé
                    if (curObject.transform.tag != "Petit_Inventaire" && curObject.transform.tag != "Petit_Drag" && curObject.transform.tag != "croix" )
                    {
                        if (curObject.transform.GetComponent<Renderer>())
                        {
                            curObject.transform.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                        }
                        ReadyInventaire = true;
                    }
                    else
                    {
                        print("fct");
                        if (ReadyInventaire == false)
                        {
                            print("fctInventaire");
                            GoInventaire(curObject);
                        }
                    }

                    //Enlever le flou
                    flou.SetActive(false);

                    myZoomLight.SetActive(false);

                    //Changement de state
                    state = Mathf.Abs(state - 1);
                }
            }
        }
    }

    //Faire toute les verifications / initialisation avant de le bouger
    public void CheckMovement(Transform _hit)
    {
        //Verifier le tag pour le facteur
        verifTag(_hit);

        //Initialiser les variablies init
        initVariables(_hit);

        //Enlever les ombre porté
        if (_hit.GetComponent<Renderer>())
        {
            _hit.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }
        
        //Activer le flou arrière
        flou.SetActive(true);

        if( curObject.name != "sphere")
        {
            myZoomLight.SetActive(true);
        }

        //Changer de state
        state = Mathf.Abs(state - 1);
    }
}
