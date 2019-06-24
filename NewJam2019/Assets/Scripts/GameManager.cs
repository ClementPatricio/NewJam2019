using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;

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
    public bool finalCombi = false;
    public GameObject eyes;
    

    public AudioClip[] sounds = new AudioClip[7];

    public GameObject[] prefabs = new GameObject[4];

    public AudioSource combiAudioSource;

    public Canvas baseUI;
    public Canvas shiftUI;

    static Color love = new Color32(202, 152, 199, 255);
    static Color anger = new Color32(212, 137, 137, 255);
    static Color despair = new Color32(163, 199, 207, 255);
    static Color solitude = new Color32(185, 217, 204, 255);
    static Color life = new Color32(255, 255, 255, 255);




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
        if (finalCombi)
        {
            this.light.range++; // = this.light.range + 10;
            return;
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
                    Destroy(this.eyes.GetComponent<SimpleSmoothMouseLook>());
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    itemObserverMode = false;
                    GameManager.gameManager.shiftUI.gameObject.SetActive(false);
                    GameManager.gameManager.baseUI.enabled = true;
                    this.eyes.AddComponent<SimpleSmoothMouseLook>().lockCursor = true;
                }

            }
            else
            {
                if (itemObserverMode)
                {
                    itemObserverMode = false;
                    GameManager.gameManager.shiftUI.gameObject.SetActive(false);
                    GameManager.gameManager.baseUI.enabled = true;
                    this.eyes.AddComponent<SimpleSmoothMouseLook>().lockCursor = true;
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
        this.leftHandOccupied = false;
        this.rightHandOccupied = false;
        this.leftObject = null;
        this.rightObject = null;
        this.light.range++;
        switch (mood)
        {
            case "Anger":
                this.changeMood(GameManager.anger, 0);
                this.leftObject = Instantiate(this.prefabs[0], leftCenter.transform.position, Quaternion.identity);
                this.leftObject.AddComponent<ObjectToHandScript>();
                this.leftObject.GetComponent<ObjectToHandScript>().leftSnapped = true;
                this.leftObject.GetComponent<ObjectToHandScript>().centered = true;
                this.leftObject.transform.SetParent(GameManager.gameManager.leftHand.transform.parent);
                Destroy(this.leftObject.GetComponent<Rigidbody>());
                break;
            case "Love":
                this.changeMood(GameManager.love, 2);
                this.leftObject = Instantiate(this.prefabs[2], leftCenter.transform.position, Quaternion.identity);
                this.leftObject.AddComponent<ObjectToHandScript>();
                this.leftObject.GetComponent<ObjectToHandScript>().leftSnapped = true;
                this.leftObject.GetComponent<ObjectToHandScript>().centered = true;
                this.leftObject.transform.SetParent(GameManager.gameManager.leftHand.transform.parent);
                Destroy(this.leftObject.GetComponent<Rigidbody>());
                break;
            case "Despair":
                this.changeMood(GameManager.despair, 1);
                this.leftObject = Instantiate(this.prefabs[1], leftCenter.transform.position, Quaternion.identity);
                this.leftObject.AddComponent<ObjectToHandScript>();
                this.leftObject.GetComponent<ObjectToHandScript>().leftSnapped = true;
                this.leftObject.GetComponent<ObjectToHandScript>().centered = true;
                this.leftObject.transform.SetParent(GameManager.gameManager.leftHand.transform.parent);
                Destroy(this.leftObject.GetComponent<Rigidbody>());
                break;
            case "Solitude":
                this.changeMood(GameManager.solitude, 4);
                this.leftObject = Instantiate(this.prefabs[3], leftCenter.transform.position, Quaternion.identity);
                this.leftObject.AddComponent<ObjectToHandScript>();
                this.leftObject.GetComponent<ObjectToHandScript>().leftSnapped = true;
                this.leftObject.GetComponent<ObjectToHandScript>().centered = true;
                this.leftObject.transform.SetParent(GameManager.gameManager.leftHand.transform.parent);
                Destroy(this.leftObject.GetComponent<Rigidbody>());
                break;
            case "Life":
                this.changeMood(GameManager.life, 3);
                this.gameObject.GetComponent<FinalScript>().enabled = true;
                this.gameObject.GetComponent<AudioSource>().loop = false;
                this.finalCombi = true;
                this.baseUI.gameObject.SetActive(false);
                this.shiftUI.gameObject.SetActive(false);
                break;
            default:
                break;
        }
        

    }


}
