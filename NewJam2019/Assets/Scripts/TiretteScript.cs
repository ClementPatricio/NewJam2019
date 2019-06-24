using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiretteScript : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameManager.gameManager.light.enabled = !GameManager.gameManager.light.enabled;
        this.GetComponent<AudioSource>().Play();
    }
}
