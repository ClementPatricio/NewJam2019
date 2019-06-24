using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScript : MonoBehaviour
{

    public Canvas canvas;

    public Image fond;

    public Image logo;

    public Text credits;

    public byte i = 0;
    public int j = 0;
    private bool fondOk = false;
    private bool logoOk = false;
    private bool creditsOk = false;

    // Update is called once per frame
    void Update()
    {
        
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
        if(fondOk && !logoOk)
        {
            if(this.j < 355)
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
        if (logoOk && !credits)
        {
            if (this.j < 355)
            {
                j++;
            }
            else
            {
                this.i--;
                this.logo.color = new Color32(255, 255, 255, this.i);
                if (this.i <= 0)
                {
                    i++;
                    this.credits.color = new Color32(255, 255, 255, this.i);
                    i++;
                }
            }
        }

    }
}
