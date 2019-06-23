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
    private bool firstframe;


    /*private void onEnable()
    {
        controls = new InputMaster();
        controls.Enable();
        controls.Player.RightClick.performed += ctx => SnapObjectToRightHand();
        controls.Player.LeftClick.performed += ctx => SnapObjectToLeftHand();
    }*/

    private void OnMouseOver()
    {
        
        if (Input.GetMouseButtonDown(0)&& !leftSnapped && !GameManager.gameManager.leftHandOccupied)
        {
            this.SnapObjectToLeftHand();
            this.leftSnapped = true;
            this.firstframe = true;
            GameManager.gameManager.leftHandOccupied = true;
            return;
        }
        if (Input.GetMouseButtonDown(1) && !rightSnapped && !GameManager.gameManager.rightHandOccupied)
        {
            this.SnapObjectToRightHand();
            this.rightSnapped = true;
            this.firstframe = true;
            GameManager.gameManager.rightHandOccupied = true;
            return;
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && leftSnapped && !firstframe)
        {
            this.ReturnObjectFromLeftHand();
            this.leftSnapped = false;
            GameManager.gameManager.leftHandOccupied = false;
            return;
        }
        if (Input.GetMouseButtonDown(1) && rightSnapped && !firstframe)
        {
            this.ReturnObjectFromRightHand();
            this.rightSnapped = false;
            GameManager.gameManager.rightHandOccupied = false;
            return;
        }
        if (firstframe)
        {
            firstframe = false;
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
        //this.GetComponent<Renderer>().materials = new Material[3];
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
        this.transform.position = getPosToLand();
        this.gameObject.AddComponent<Rigidbody>();
        this.GetComponent<Rigidbody>().WakeUp();
        this.transform.parent = null;
    }

    void ReturnObjectFromLeftHand()
    {
        this.transform.position = getPosToLand();
        this.gameObject.AddComponent<Rigidbody>();
        this.GetComponent<Rigidbody>().useGravity = true;
        this.transform.parent = null;
    }

    Vector3 getPosToLand()
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
        float posZ = hit.distance;
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, posZ));
        return pos;
    }
    
}
