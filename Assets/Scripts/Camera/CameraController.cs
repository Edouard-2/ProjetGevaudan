using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 initPosition;

    private void Start()
    {
        initPosition = gameObject.transform.position;
    }
    public void moveCamera(Transform _hit, bool _switch)
    {
        if (_switch)
        {
            gameObject.transform.position = _hit.GetComponent<ScrollingManager>().cameraPosition;
        }
        else
        {
            gameObject.transform.position = initPosition;
        }

    }
}
