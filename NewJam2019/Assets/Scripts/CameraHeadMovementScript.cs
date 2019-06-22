using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHeadMovementScript : MonoBehaviour
{
    [SerializeField]
    private GameObject head;
    [Range(1,100)][SerializeField]
    private int mouseSensitivity;


    // Update is called once per frame
    void Update()
    {
        Vector3 headAxis;
        Vector3 eyesAxis;

        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * this.mouseSensitivity;
        float mouseY = -Input.GetAxis("Mouse Y") * Time.deltaTime * this.mouseSensitivity;

        //head.transform.Rotate(new Vector3(0, mouseX, 0));
        //transform.Rotate(new Vector3(mouseY, 0, 0));

        headAxis = head.transform.localEulerAngles;
        eyesAxis = this.transform.localEulerAngles;

        headAxis.y = clampHeadAxis(headAxis.y + mouseX);
        eyesAxis.x = clampHeadAxis(eyesAxis.x + mouseY);

        head.transform.localEulerAngles = headAxis;
        this.transform.localEulerAngles = new Vector3(eyesAxis.x, 0, 0);
    }

    float clampHeadAxis(float value)
    {
        return Mathf.Clamp(value, 30f, 150f);
    }

    float clampEyesAxis(float value)
    {
        return Mathf.Clamp(value, 30f, 150f);
    }
}
