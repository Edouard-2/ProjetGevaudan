using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCamera : MonoBehaviour
{
    public GameObject myCamera;
    public GameObject emptyCamera;
    bool readyTransition = false;

    // Update is called once per frame
    void Update()
    {
        if(readyTransition)
        {
            myCamera.transform.position = Vector3.MoveTowards(myCamera.transform.position, emptyCamera.transform.position, 4f * Time.deltaTime );
            if (myCamera.transform.position == emptyCamera.transform.position)
            {
                SceneManager.LoadScene("GamePlay");
            }
        }
    }

    private void OnMouseUpAsButton()
    {
        GetComponent<Animator>().SetTrigger("open");
        StartCoroutine(activeMovementCamera());
    }

    IEnumerator activeMovementCamera()
    {
        yield return new WaitForSeconds(1f);
        readyTransition = true;
    }
}