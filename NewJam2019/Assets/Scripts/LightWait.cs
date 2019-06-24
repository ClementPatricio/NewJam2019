using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightWait : MonoBehaviour
{
    public float sec = 1f;
    private Light myLight;
    public GameObject UI;
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

        myLight.enabled = !myLight.enabled;
        this.GetComponent<AudioSource>().Play();
        UI.SetActive(true);
    }
}
