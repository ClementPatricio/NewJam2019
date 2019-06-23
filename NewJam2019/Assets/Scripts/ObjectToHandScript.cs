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
    private bool centered;
    private float rotSpeed = 150;


    /*private void onEnable()
    {
        controls = new InputMaster();
        controls.Enable();
        controls.Player.RightClick.performed += ctx => SnapObjectToRightHand();
        controls.Player.LeftClick.performed += ctx => SnapObjectToLeftHand();
    }*/

    private void OnMouseOver()
    {
        if (!GameManager.gameManager.itemObserverMode) {
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
    }

    private void OnMouseDrag() {
        if (this.centered) {
            float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
            float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;

            this.transform.Rotate(Vector3.up, -rotX);
            this.transform.Rotate(Vector3.right, rotY);
        }
    }

    private void Update()
    {
        if (!GameManager.gameManager.itemObserverMode) {

            if (this.centered) {
                if (this.rightSnapped) {
                    this.UnSnapObjectFromRightCenter();
                }
                else if (this.leftSnapped) {
                    this.UnSnapObjectFromLeftCenter();
                }
                this.centered = false;
            }

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
        } else {
            if (!this.centered) {
                if (this.rightSnapped) {
                    this.SnapObjectToRightCenter();
                }
                else if (this.leftSnapped) {
                    this.SnapObjectToLeftCenter();
                }
                this.centered = true;
            }
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
        this.GetComponent<Renderer>().materials = new Material[3];
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

    void SnapObjectToRightCenter() {
        Transform centerPosition = GameManager.gameManager.rightCenter.transform;
        this.transform.position = centerPosition.position;
    }

    void SnapObjectToLeftCenter() {
        Transform centerPosition = GameManager.gameManager.leftCenter.transform;
        this.transform.position = centerPosition.position;
    }

    void UnSnapObjectFromRightCenter() {
        Transform handPosition = GameManager.gameManager.rightHand.transform;
        this.transform.position = handPosition.position;
    }

    void UnSnapObjectFromLeftCenter() {
        Transform handPosition = GameManager.gameManager.leftHand.transform;
        this.transform.position = handPosition.position;
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
