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
    public InputMaster controls;
    public bool leftHandOccupied;
    public bool rightHandOccupied;

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
        this.light.color = newColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            this.changeLightColor(Color.white);
        }
    }
}
