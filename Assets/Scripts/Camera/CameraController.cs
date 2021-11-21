using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 initPosition;
    Vector3 NextPosition;

    public float cameraSpeed = 0.1f;

    private void Start()
    {
        initPosition = gameObject.transform.position;
        NextPosition = Vector3.zero;
    }

    private void Update()
    {
        if(NextPosition != Vector3.zero && gameObject.transform.position != NextPosition)
        {
            Debug.Log("zefojn");
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, NextPosition, cameraSpeed);
        }
    }

    public void moveCamera(Transform _hit, bool _switch)
    {
        if (_switch)
        {
            NextPosition = _hit.GetComponent<ScrollingManager>().cameraPosition;
        }
        else
        {
            NextPosition = initPosition;
        }
    }
}
