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
    public bool changingLight;
    public float speed;
    public Color baseColor;
    public Color nextColor;
    public Light light;

    public AudioClip[] sounds = new AudioClip[7];

    public AudioSource combiAudioSource;

    static Color love = new Color();
    static Color anger = new Color();
    static Color despair = new Color();
    static Color solitude = new Color();
    static Color life = new Color();




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
    }

    void changeMood(Color color, int soundIndex)
    {
        this.changeLightColor(color);
        this.GetComponent<AudioSource>().clip = this.sounds[soundIndex];
        this.GetComponent<AudioSource>().Play();
    }


}
