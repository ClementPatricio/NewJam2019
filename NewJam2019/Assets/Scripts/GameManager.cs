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

    void Awake()
    {
        GameManager.gameManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
