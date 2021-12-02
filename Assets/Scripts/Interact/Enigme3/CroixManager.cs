using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CroixManager : MonoBehaviour
{
    public InventaireManager myInventaireManager;
    public InitData myInitData;

    public bool ready = false;

    private void Awake()
    {
        myInventaireManager = FindObjectOfType<InventaireManager>();   
        myInitData = gameObject.GetComponent<InitData>();   
    }

    //Ajouter un morceau de croix a l'emplacement definie 
    public void AddCroixEmpty(GameObject _item)
    {
        //Si aucun morceau de croix n'a encore était ramassé
        if (!ready)
        {
            ready = true;
            myInitData.enabled = true;

            myInitData.state = 2;
            myInitData.id = _item.GetComponent<InitData>().id;

            gameObject.transform.position = _item.transform.position;
            gameObject.transform.localRotation = Quaternion.Euler(myInitData.initRotationValue);
            
            gameObject.transform.SetParent(myInventaireManager.emplacementItems[_item.GetComponent<InitData>().id].transform);

            print(myInitData.initScale);

            myInventaireManager.listObj[_item.GetComponent<InitData>().id] = gameObject;
        }
        else
        {
            print(_item.GetComponent<InitData>().id);
            myInventaireManager.listObj[_item.GetComponent<InitData>().id] = null;
        }

        //Ajout du morceau a l'empty parent 

        _item.GetComponent<BoxCollider>().enabled = false;
        _item.GetComponent<InitData>().enabled = false;
        _item.transform.SetParent(gameObject.transform);
        _item.transform.position = gameObject.transform.position;
        _item.transform.rotation = gameObject.transform.rotation;
        _item.transform.localScale = _item.GetComponent<InitData>().facteurItem;
    }
}
