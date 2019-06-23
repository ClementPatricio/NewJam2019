using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectToHandScript : MonoBehaviour
{
    public InputMaster controls;
    private bool rightSnapped;
    private bool leftSnapped;
    private Vector3 lastPos;


    /*private void onEnable()
    {
        controls = new InputMaster();
        controls.Enable();
        controls.Player.RightClick.performed += ctx => SnapObjectToRightHand();
        controls.Player.LeftClick.performed += ctx => SnapObjectToLeftHand();
    }*/

    private void OnMouseOver()
    {
        
        if (Input.GetMouseButtonDown(0)&& !leftSnapped)
        {
            this.SnapObjectToLeftHand();
            this.leftSnapped = true;
            return;
        }
        if (Input.GetMouseButtonDown(1) && !rightSnapped)
        {
            this.SnapObjectToRightHand();
            this.rightSnapped = true;
            return;
        }
        if (Input.GetMouseButtonDown(0) && leftSnapped)
        {
            this.ReturnObjectFromLeftHand();
            this.leftSnapped = false;
            return;
        }
        if (Input.GetMouseButtonDown(1) && rightSnapped)
        {
            this.ReturnObjectFromRightHand();
            this.rightSnapped = false;
            return;
        }

    }

    void SnapObjectToRightHand()
    {
        Transform handPosition = GameManager.gameManager.rightHand.transform;
        this.lastPos = this.transform.position;
        this.transform.position = handPosition.position;
        this.transform.SetParent(handPosition.parent);
        this.GetComponent<Rigidbody>().useGravity = false;
        Destroy(this.GetComponent<Rigidbody>());
    }

    void SnapObjectToLeftHand()
    {
        Transform handPosition = GameManager.gameManager.leftHand.transform;
        this.lastPos = this.transform.position;
        this.transform.position = handPosition.position;
        this.transform.SetParent(handPosition.parent);
        this.GetComponent<Rigidbody>().useGravity = false;
        Destroy(this.GetComponent<Rigidbody>());
    }

    void ReturnObjectFromRightHand()
    {
        //this.transform.position = lastPos;
        this.gameObject.AddComponent<Rigidbody>();
        this.GetComponent<Rigidbody>().WakeUp();
        this.transform.parent = null;
    }

    void ReturnObjectFromLeftHand()
    {
        //this.transform.position = lastPos;
        this.gameObject.AddComponent<Rigidbody>();
        this.GetComponent<Rigidbody>().useGravity = true;
        this.transform.parent = null;
    }
}
