using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CroixManager : MonoBehaviour
{
    public InventaireManager myInventaireManager;

    public bool ready = false;

    private void Awake()
    {
        myInventaireManager = FindObjectOfType<InventaireManager>();   
    }

    public void AddCroixEmpty(GameObject _item)
    {
        if (!ready)
        {
            ready = true;
            gameObject.GetComponent<InitData>().enabled = true;

            gameObject.GetComponent<InitData>().state = 2;
            gameObject.GetComponent<InitData>().id = _item.GetComponent<InitData>().id;

            gameObject.transform.position = _item.transform.position;
            gameObject.transform.localRotation = Quaternion.Euler( gameObject.GetComponent<InitData>().initRotationValue);
            
            gameObject.transform.SetParent(FindObjectOfType<InventaireManager>().emplacementItems[_item.GetComponent<InitData>().id].transform);

            print(gameObject.GetComponent<InitData>().initScale);
        }

        FindObjectOfType<InventaireManager>().listObj[_item.GetComponent<InitData>().id] = gameObject;

        _item.GetComponent<BoxCollider>().enabled = false;
        _item.GetComponent<InitData>().enabled = false;
        _item.transform.SetParent(gameObject.transform);
        _item.transform.position = gameObject.transform.position;
        _item.transform.rotation = gameObject.transform.rotation;
        _item.transform.localScale = _item.GetComponent<InitData>().facteurItem;
    }
}
