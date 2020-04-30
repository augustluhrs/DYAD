using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrioJoystickManager : MonoBehaviour
{
    // public VariableJoystick variableJoystick;
    public FixedJoystick fixedJoystick;
    public bool firstPlayer;
    private string beskrivning;
    public TextMeshProUGUI valueText;
    public TextMeshProUGUI typeText; //beskrivning below wheel
    public TextMeshProUGUI topTypeText; //beskrivning above wheel
    [SerializeField] GameObject beskrivningTextBottom;
    [SerializeField] GameObject beskrivningTextTop;
    public Image background;
    public Sprite[] axisSprites;

    [Header("Wheel Icons")]
    [Range(.5f, 2f)] public float highlightScale = 1f;
    public float defaultScale = 0.5f;
    // public GameObject odwarIcon;
    // public GameObject tullstaIcon;
    // public GameObject bedIcon; //brimnes
    // public GameObject ektorpIcon;
    public GameObject leftIcon;
    public GameObject topIcon;
    public GameObject rightIcon;
    public GameObject wheelIcon;
    public Sprite[] icons;

    //icon settings
    public string selectedIcon;

    BasicSpawnManager m_BasicSpawnManager;

    void Start()
    {
        if (firstPlayer)
        {
            selectedIcon = "AR_Tullsta_white";
            rightIcon.GetComponent<Image>().sprite = icons[2];
        }
        else
        {
            selectedIcon = "AR_Tullsta_black";
            rightIcon.GetComponent<Image>().sprite = icons[3];
        }
        beskrivning = "tullsta";
        m_BasicSpawnManager = GameObject.Find("AR Session Origin").GetComponent<BasicSpawnManager>();
        beskrivningTextBottom.SetActive(true);
        beskrivningTextTop.SetActive(false);
    }

    private void Update()
    {
        //disabled, but can debug with
        valueText.text = "Current Value: " + fixedJoystick.Direction;

        //if moving the joystick selector, update the icons
        float joyX = fixedJoystick.Direction.x;
        //now have a bit of a spacer to make it less sensitive/jumpy
        if(joyX <= -.4f) // left
        {
            if (firstPlayer)
            {
                selectedIcon = "AR_Odwar_brown";
            }
            else
            {
                selectedIcon = "AR_Odwar_grey";
            }
            beskrivning = "odwar"; 

            leftIcon.transform.localScale = Vector3.one * highlightScale;
            topIcon.transform.localScale = Vector3.one * defaultScale;
            rightIcon.transform.localScale = Vector3.one * defaultScale;
        }
        if(joyX > -.3f && joyX < .3f && joyX != 0) // up
        {
            if (firstPlayer)
            {
                selectedIcon = "AR_Tullsta_white";
            }
            else
            {
                selectedIcon = "AR_Tullsta_black";
            }
            beskrivning = "tullsta"; 
                     
            leftIcon.transform.localScale = Vector3.one * defaultScale;
            topIcon.transform.localScale = Vector3.one * highlightScale;
            rightIcon.transform.localScale = Vector3.one * defaultScale;
        }
        if(joyX >= .4f) //right
        {
            if (firstPlayer)
            {
                selectedIcon = "AR_Brimnes_red";
                beskrivning = "brimnes"; 
            }
            else
            {
                selectedIcon = "AR_Liskele_blue";
                beskrivning = "liskele";     
            }

            leftIcon.transform.localScale = Vector3.one * defaultScale;
            topIcon.transform.localScale = Vector3.one * defaultScale;
            rightIcon.transform.localScale = Vector3.one * highlightScale;       
        }
        /*
        if(joyX > .5f) //far right
        {
            // Debug.Log("far right");
            selectedIcon = "AR_Biggio_black";
            // typeText.text = "ektorp"; 
            beskrivning = "ektorp";            

            // odwarIcon.transform.localScale = Vector3.one * defaultScale;
            // tullstaIcon.transform.localScale = Vector3.one * defaultScale;
            // bedIcon.transform.localScale = Vector3.one * defaultScale;
            // ektorpIcon.transform.localScale = Vector3.one * highlightScale;
        }
        */
        if(joyX == 0f) //default
        {
            //selectedIcon should stay what it was
            leftIcon.transform.localScale = Vector3.one * defaultScale;
            topIcon.transform.localScale = Vector3.one * defaultScale;
            rightIcon.transform.localScale = Vector3.one * defaultScale;
        
            beskrivningTextBottom.SetActive(true);
            typeText.text = beskrivning;
            beskrivningTextTop.SetActive(false);  
        }
        else //if not at rest, move text up
        {
            beskrivningTextTop.SetActive(true);
            topTypeText.text = beskrivning;
            beskrivningTextBottom.SetActive(false);  
        }



        //change the wheel image and the spawn manager setting
        m_BasicSpawnManager.selectedFurniture = selectedIcon;
        
        // switch(selectedIcon)
        switch(beskrivning)
        {
            /*case "AR_Odwar_brown":
                wheelIcon.GetComponent<Image>().sprite = icons[0];
                break;
            case "AR_Tullsta_white":
                wheelIcon.GetComponent<Image>().sprite = icons[1];
                break;
            case "AR_Brimnes_red":
                wheelIcon.GetComponent<Image>().sprite = icons[2];
                break;
            case "AR_Biggio_black":
                wheelIcon.GetComponent<Image>().sprite = icons[3];
                break;
                */
            case "odwar":
                wheelIcon.GetComponent<Image>().sprite = icons[0];
                break;
            case "tullsta":
                wheelIcon.GetComponent<Image>().sprite = icons[1];
                break;
            case "brimnes":
                wheelIcon.GetComponent<Image>().sprite = icons[2];
                break;
            case "liskele":
                wheelIcon.GetComponent<Image>().sprite = icons[3];
                break;
            default:
                Debug.Log("default error in icon switch");
                break;
        }
    }

    

    // Old

    public void ModeChanged(int index)
    {
        // switch(index)
        // {
        //     case 0:
        //         variableJoystick.SetMode(JoystickType.Fixed);
        //         break;
        //     case 1:
        //         variableJoystick.SetMode(JoystickType.Floating);
        //         break;
        //     case 2:
        //         variableJoystick.SetMode(JoystickType.Dynamic);
        //         break;
        //     default:
        //         break;
        // }     
    }

    public void AxisChanged(int index)
    {
        // switch (index)
        // {
        //     case 0:
        //         variableJoystick.AxisOptions = AxisOptions.Both;
        //         background.sprite = axisSprites[index];
        //         break;
        //     case 1:
        //         variableJoystick.AxisOptions = AxisOptions.Horizontal;
        //         background.sprite = axisSprites[index];
        //         break;
        //     case 2:
        //         variableJoystick.AxisOptions = AxisOptions.Vertical;
        //         background.sprite = axisSprites[index];
        //         break;
        //     default:
        //         break;
        // }
    }

     public void SnapX(bool value)
    {
        fixedJoystick.SnapX = value;
    }

    public void SnapY(bool value)
    {
        fixedJoystick.SnapY = value;
    }
}
