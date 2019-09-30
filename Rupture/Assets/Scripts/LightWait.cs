using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightWait : MonoBehaviour
{
    public float sec = 1f;
    private Light myLight;
    private GameObject UI;
    void Start()
    {
        myLight = GetComponent<Light>();
        UI = GameObject.Find("BaseUI");
        UI.SetActive(false);
        StartCoroutine(LateCall());
    }

    IEnumerator LateCall()
    {

        yield return new WaitForSeconds(sec);

        this.GetComponent<AudioSource>().Play();
        myLight.enabled = !myLight.enabled;
        UI.SetActive(true);
    }
}
