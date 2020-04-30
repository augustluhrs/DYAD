using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JoystickManager : MonoBehaviour
{
    // public VariableJoystick variableJoystick;
    public FixedJoystick fixedJoystick;
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
    public GameObject odwarIcon;
    public GameObject tullstaIcon;
    public GameObject bedIcon; //brimnes
    public GameObject ektorpIcon;
    public GameObject wheelIcon;
    public Sprite[] icons;

    //icon settings
    public string selectedIcon;

    BasicSpawnManager m_BasicSpawnManager;

    void Start()
    {
        selectedIcon = "AR_Tullsta_white"; //default
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
        if(joyX <= -.5f) //far left
        {
            // Debug.Log("far left");
            selectedIcon = "AR_Odwar_brown";
            // typeText.text = "odwar"; 
            beskrivning = "odwar";            
            odwarIcon.transform.localScale = Vector3.one * highlightScale;
            tullstaIcon.transform.localScale = Vector3.one * defaultScale;
            bedIcon.transform.localScale = Vector3.one * defaultScale;
            ektorpIcon.transform.localScale = Vector3.one * defaultScale;
        }
        if(joyX > -.5f && joyX < 0) //left up
        {
            // Debug.Log("left up");
            selectedIcon = "AR_Tullsta_white";
            // typeText.text = "tullsta";
            beskrivning = "tullsta";           

            // m_BasicSpawnManager.selectedFurniture = selectedIcon;
            odwarIcon.transform.localScale = Vector3.one * defaultScale;
            tullstaIcon.transform.localScale = Vector3.one * highlightScale;
            bedIcon.transform.localScale = Vector3.one * defaultScale;
            ektorpIcon.transform.localScale = Vector3.one * defaultScale;
        }
        if(joyX > 0f && joyX <= .5f) //right up
        {
            // Debug.Log("right up");
            selectedIcon = "AR_Brimnes_red"; 
            // typeText.text = "brimnes";
            beskrivning = "brimnes";            

            // m_BasicSpawnManager.selectedFurniture = selectedIcon;
            odwarIcon.transform.localScale = Vector3.one * defaultScale;
            tullstaIcon.transform.localScale = Vector3.one * defaultScale;
            bedIcon.transform.localScale = Vector3.one * highlightScale;
            ektorpIcon.transform.localScale = Vector3.one * defaultScale;
        }
        if(joyX > .5f) //far right
        {
            // Debug.Log("far right");
            selectedIcon = "AR_Biggio_black";
            // typeText.text = "ektorp"; 
            beskrivning = "ektorp";            

            odwarIcon.transform.localScale = Vector3.one * defaultScale;
            tullstaIcon.transform.localScale = Vector3.one * defaultScale;
            bedIcon.transform.localScale = Vector3.one * defaultScale;
            ektorpIcon.transform.localScale = Vector3.one * highlightScale;
        }
        if(joyX == 0f) //default
        {
            //selectedIcon should stay what it was
            odwarIcon.transform.localScale = Vector3.one * defaultScale;
            tullstaIcon.transform.localScale = Vector3.one * defaultScale;
            bedIcon.transform.localScale = Vector3.one * defaultScale;
            ektorpIcon.transform.localScale = Vector3.one * defaultScale;
        
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
        
        switch(selectedIcon)
        {
            case "AR_Odwar_brown":
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
