using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiretteScript : MonoBehaviour
{
    private Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        GameManager.gameManager.light.enabled = !GameManager.gameManager.light.enabled;
        //this.GetComponent<AudioSource>().Play();
        anim.Play("Tirette", 0, 0.25f);
    }
}
