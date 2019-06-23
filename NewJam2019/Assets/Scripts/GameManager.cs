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
    public GameObject leftObject;
    public GameObject rightObject;
    public bool leftHandOccupied;
    public bool rightHandOccupied;
    public bool changingLight;
    public bool itemObserverMode;
    public float speed;
    public Color baseColor;
    public Color nextColor;
    public Light light;
    

    public AudioClip[] sounds = new AudioClip[7];

    public AudioSource combiAudioSource;

    public Canvas baseUI;
    public Canvas shiftUI;

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

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            if (leftHandOccupied && rightHandOccupied) {
                if (!itemObserverMode) {
                    itemObserverMode = true;
                    this.baseUI.enabled = false;
                    this.shiftUI.gameObject.SetActive(true);
                    //TODO: Hide Cursor, Blur Background
                } else {
                    itemObserverMode = false;
                    GameManager.gameManager.shiftUI.gameObject.SetActive(false);
                    GameManager.gameManager.baseUI.enabled = true;
                    //TODO: Show Cursor, unblur Background
                }

            }
        }

        if (this.itemObserverMode && Input.GetKeyDown(KeyCode.E))
        {
            if(this.leftObject.tag == this.rightObject.tag)
            {
                this.combiAudioSource.clip = this.sounds[5];
                this.combiAudioSource.Play();
                this.combine(this.leftObject.tag);
            }
            else
            {
                this.combiAudioSource.clip = this.sounds[6];
                this.combiAudioSource.Play();
            }
        }
    }

    void changeMood(Color color, int soundIndex)
    {
        this.changeLightColor(color);
        this.GetComponent<AudioSource>().clip = this.sounds[soundIndex];
        this.GetComponent<AudioSource>().Play();
    }

    void combine(string mood)
    {
        Destroy(this.leftObject);
        Destroy(this.rightObject);
        switch (mood)
        {
            case "anger":
                this.changeMood(GameManager.anger, 0);
                
                break;
            case "love":
                this.changeMood(GameManager.love, 2);

                break;
            case "despair":
                this.changeMood(GameManager.despair, 1);

                break;
            case "solitude":
                this.changeMood(GameManager.solitude, 4);

                break;
            case "life":
                this.changeMood(GameManager.life, 3);

                break;
            default:
                break;
        }
    }


}
