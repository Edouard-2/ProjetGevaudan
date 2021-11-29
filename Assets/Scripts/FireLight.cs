using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLight : MonoBehaviour
{
    public float minFlicker = 1f;
    public float maxFlicker = 1.2f;
    public float nextLight = 0f;

    public float speed;
    public float speedIntensity;

    Light myLight;

    private void Start()
    {
        myLight = gameObject.GetComponent<Light>();
        InvokeRepeating("startLight", 0f, speed);
        nextLight = myLight.intensity;
    }

    private void Update()
    {
        myLight.intensity = Mathf.Lerp(myLight.intensity, nextLight, speedIntensity);
    }

    void startLight()
    {
        StartCoroutine(lightEffect());
    }

    IEnumerator lightEffect()
    {
        yield return new WaitForSeconds(speed);
        nextLight = Random.Range(minFlicker, maxFlicker);
        StartCoroutine(lightEffect());
    }
}