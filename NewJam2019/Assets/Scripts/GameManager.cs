using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class GameManager : MonoBehaviour
{

    public static GameManager gameManager;

    public GameObject rightHand;
    public GameObject leftHand;
    public GameObject rightCenter;
    public GameObject leftCenter;
    public InputMaster controls;
    public bool leftHandOccupied;
    public bool rightHandOccupied;
    public bool changingLight;
    public bool itemObserverMode;
    public float speed;
    public Color baseColor;
    public Color nextColor;

    static Color love;
    static Color anger;
    static Color despair;
    static Color solitude;
    static Color Life;

    void Awake()
    {
        GameManager.gameManager = this;
    }



    private void onEnable()
    {
        controls = new InputMaster();
        controls.Enable();
        //controls.Player.RightClick.performed += ctx => SnapObjectToRightHand();
        //controls.Player.LeftClick.performed += ctx => SnapObjectToLeftHand();
    }

    public Light light;


    public void changeLightColor(Color newColor)
    {
        this.baseColor = this.light.color;
        this.changingLight = true;
        this.nextColor = newColor;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            this.changeLightColor(Color.white);
        }

        if (changingLight)
        {
            this.light.color = Color.Lerp(this.light.color, nextColor, .01f);
        }
        if(this.light.color.Equals(nextColor))
        {
            changingLight = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            if (leftHandOccupied && rightHandOccupied) {
                if (!itemObserverMode) {
                    itemObserverMode = true;
                    //TODO: Hide Cursor, Blur Background
                } else {
                    itemObserverMode = false;
                    //TODO: Show Cursor, unblur Background
                }

            }
        }
    }
}
