using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class ObjectToHandScript : MonoBehaviour
{
    [SerializeField]
    public InputMaster controls;


    private void Awake()
    {
        controls.Player.RightClick.performed += ctx => SnapObjectToRightHand();
        controls.Player.LeftClick.performed += ctx => SnapObjectToLeftHand();
    }

    

    private void SnapObjectToRightHand()
    {
        Transform handPosition = GameManager.gameManager.rightHand.transform;
        this.transform.position = handPosition.position;
    }

    private void SnapObjectToLeftHand()
    {
        Transform handPosition = GameManager.gameManager.leftHand.transform;
        this.transform.position = handPosition.position;
    }
}
