using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Ikea Sans font thanks to http://www.onlinewebfonts.com, licensed by CC BY 3.0

public class LevelManager : MonoBehaviour
{
    //should I have these in an array or list of some sort?
    //game object over canvas because .setActive()
    [SerializeField] GameObject startScreen;
    [SerializeField] GameObject tutorialScreen;
    private GameObject tutorialCanvas;
    [SerializeField] GameObject level1Screen;

    [SerializeField] GameObject tutorialLevel;
    [SerializeField] GameObject firstLevel; //studio
    private string level;
    private string tutorialStage;

    private int testInt = 0;
    private int testLimit = 500;


    void Start()
    {
        startScreen.SetActive(true);
        tutorialScreen.SetActive(false);
        level1Screen.SetActive(false);
        tutorialLevel.SetActive(false);
        firstLevel.SetActive(false);
        level = "start";
        tutorialCanvas = tutorialScreen.transform.GetChild(0).gameObject;
    }

    void Update()
    {
        //what are enums and should i use them? using strings to try and not be abstract about the switch cases
        switch (level)
        {
            case "start":
                break;
            case "tutorial":
                foreach (Transform panel in tutorialCanvas.transform)
                {
                    // Debug.Log(panel.gameObject.name);
                    if(panel.gameObject.name == tutorialStage)
                        panel.gameObject.SetActive(true);
                    else
                        panel.gameObject.SetActive(false);
                }
                switch (tutorialStage)
                {
                    case "ReadyPanel":
                        if (testInt > testLimit)
                        {
                            tutorialStage = "WelcomePanel";
                            testInt = 0;
                        }
                        else
                            testInt++;
                        break;
                    case "WelcomePanel":
                        if (testInt > testLimit)
                        {
                            tutorialStage = "SlappaPanel";
                            testInt = 0;
                        }
                        else
                            testInt++;
                        break;
                    case "SlappaPanel":
                        if (testInt > testLimit)
                        {
                            tutorialStage = "VinkelPanel";
                            testInt = 0;
                        }
                        else
                            testInt++;
                        break;
                    case "VinkelPanel":
                        if (testInt > testLimit)
                        {
                            tutorialStage = "BeskrivningPanel";
                            testInt = 0;
                        }
                        else
                            testInt++;
                        break;
                    case "BeskrivningPanel":
                        if (testInt > testLimit)
                        {
                            tutorialStage = "SkalaPanel";
                            testInt = 0;
                        }
                        else
                            testInt++;
                        break;
                    case "SkalaPanel":
                        if (testInt > testLimit)
                        {
                            tutorialStage = "PlatsPanel";
                            testInt = 0;
                        }
                        else
                            testInt++;
                        break;
                    case "PlatsPanel":
                        if (testInt > testLimit)
                        {
                            tutorialStage = "FargPanel";
                            testInt = 0;
                        }
                        else
                            testInt++;
                        break;
                    case "FargPanel":
                        if (testInt > testLimit)
                        {
                            tutorialStage = "LastPanel";
                            testInt = 0;
                        }
                        else
                            testInt++;
                        break;
                    case "LastPanel":
                        if (testInt > testLimit)
                        {
                            tutorialStage = "ReadyPanel";
                            testInt = 0;
                        }
                        else
                            testInt++;
                        break;
                    default:
                        Debug.Log("somethings wrong in the tutorial stage switch");
                        break;

                }
                break;
            case "first level":
                break;
            default:
                Debug.Log("somethings wrong in the level switch");
                break;

        }
    }

    public void StartTutorial()
    {
        startScreen.SetActive(false);
        tutorialScreen.SetActive(true);
        level1Screen.SetActive(false);
        tutorialLevel.SetActive(true);
        firstLevel.SetActive(false);
        level = "tutorial";
        tutorialStage = "ReadyPanel";
    }

    public void StartFirstLevel()
    {
        startScreen.SetActive(false);
        tutorialScreen.SetActive(false);
        level1Screen.SetActive(true);
        tutorialLevel.SetActive(false);
        firstLevel.SetActive(true);
        level = "first level";
    }
}
