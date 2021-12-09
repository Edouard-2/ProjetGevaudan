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

        StartCoroutine(activeSound());
        StartCoroutine(activeMovementCamera());
        GetComponent<BoxCollider>().enabled = false;
    }

    IEnumerator activeSound()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.3f);
        GetComponent<Animator>().SetTrigger("open");
    }
    IEnumerator activeMovementCamera()
    {
        yield return new WaitForSeconds(1.3f);
        readyTransition = true;
    }
}