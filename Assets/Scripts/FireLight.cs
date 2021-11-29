using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLight : MonoBehaviour
{
    public float minFlicker = 1f;
    public float maxFlicker = 1.2f;

    public float speed;

    Light myLight;

    private void Start()
    {
        myLight = gameObject.GetComponent<Light>();
        InvokeRepeating("startLight", 0f, speed);
        
    }

    void startLight()
    {
        StartCoroutine(lightEffect());
    }

    IEnumerator lightEffect()
    {
        yield return new WaitForSeconds(speed);

        myLight.intensity = Random.Range(minFlicker, maxFlicker);
        StartCoroutine(lightEffect());
    }
}