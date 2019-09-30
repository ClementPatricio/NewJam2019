using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScript : MonoBehaviour
{

    public Canvas canvas;

    public Image fond;

    public Image logo;

    public Text credits1;
    public Text credits2;

    public byte i = 0;
    public int j = 0;
    private bool fondOk = false;
    private bool logoOk = false;
    private bool logoFadeOut = false;
    private bool creditsOk = false;

    // Update is called once per frame
    void Update()
    {
        if (creditsOk)
        {
            return;
        }

        if (!fondOk)
        {
            this.i++;
            this.canvas.gameObject.SetActive(true);
            this.fond.color = new Color32(255, 255, 255, this.i);
            if (this.i >= 255)
            {
                this.i = 0;
                fondOk = true;
            }
        }
        if (fondOk && !logoOk)
        {
            if (this.j < 355)
            {
                j++;
            }
            else
            {
                this.i++;
                this.logo.color = new Color32(255, 255, 255, this.i);
                if (this.i >= 255)
                {
                    logoOk = true;
                    this.j = 0;
                }
            }

        }
        if (logoOk && !creditsOk)
        {
            if (this.j < 80)
            {
                j++;
            }
            else
            {
                if (!logoFadeOut) {
                    this.i--;
                    this.logo.color = new Color32(255, 255, 255, this.i);
                }
                if (this.i <= 0)
                {
                    logoFadeOut = true;
                }
                if (logoFadeOut)
                {
                    this.credits1.color = new Color32(0, 0, 0, this.i);
                    this.credits2.color = new Color32(0, 0, 0, this.i);
                    i++;
                }
                if(i >=255 && logoFadeOut)
                {
                    creditsOk = true;
                }
                
            }
        }

    }
}