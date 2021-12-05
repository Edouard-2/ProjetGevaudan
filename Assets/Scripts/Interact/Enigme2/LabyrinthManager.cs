using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthManager : MonoBehaviour
{
    BoutLabyrinthDrag[] myListBout;

    public GameObject receptableClefLabyrinth;
    public GameObject porteDessus;

    public GameObject AntiHitbox;
    public GameObject LabyrinthRetour;

    // Start is called before the first frame update
    void Start()
    {
        receptableClefLabyrinth.SetActive(false);
        receptableClefLabyrinth.GetComponent<Receptacle>().enabled = false;
        receptableClefLabyrinth.GetComponent<BoxCollider>().enabled = false;

        myListBout = FindObjectsOfType<BoutLabyrinthDrag>();
    }

    public void finishLabyrinth()
    {
        foreach (BoutLabyrinthDrag item in myListBout)
        {
            item.GetComponent<BoxCollider>().enabled = false;
            item.state = -1;
        }
        
        receptableClefLabyrinth.SetActive(true);
        receptableClefLabyrinth.GetComponent<Receptacle>().enabled = true;
        receptableClefLabyrinth.GetComponent<BoxCollider>().enabled = true;
        AntiHitbox.transform.position = new Vector3(1000,1000,1000);
        LabyrinthRetour.SetActive(false);
        LabyrinthRetour.GetComponent<BoxCollider>().enabled = false;

        

        gameObject.transform.SetParent(porteDessus.transform);
        print("clef rentré");
    }
}
