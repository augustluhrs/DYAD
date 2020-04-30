using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayLTDemoManager : MonoBehaviour
{
    ObjectiveTest m_ObjectiveTest;
    ColliderManager m_ColliderManager;
    BasicSpawnManager m_BasicSpawnManager;
    ManualManager m_ManualManager;
    // JoystickManager m_JoystickManager;
    // [SerializeField] GameObject wheelSelector;

    
    [Header("UI Elements")] //should maybe merge with ARPlacementandSceneDetection or w/e
    [SerializeField] Slider argumentMeter;
    [SerializeField] GameObject manualScroll;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] TextMeshProUGUI roundOverText;
    public float personalSatisfaction = 0;
    public float lifeSatisfaction = 0;
    [Range(1f, 1000f)][SerializeField] float satisfactionBonus = 100f;
    [Range(1f, 1000f)][SerializeField] float satisfactionPenalty = 50f;
    public float manualSectionsUnlocked = 0;
    public bool hasRoundStarted;
    private float roundNum = 1;
    //argument timer stuff
    private float countdownTimer; //evalTimer
    // private float countdownMax; //timerMax
    [Range(1f, 200f)][SerializeField] float roundTime = 60f; //timerFadeout

    void Start()
    {
        m_ObjectiveTest = GetComponent<ObjectiveTest>();
        m_ColliderManager = GetComponent<ColliderManager>();
        m_BasicSpawnManager = GetComponent<BasicSpawnManager>();
        // m_JoystickManager = wheelSelector.GetComponent<JoystickManager>();
        m_ManualManager = manualScroll.GetComponent<ManualManager>();
        // roundNum = 0;
        argumentMeter.maxValue = roundTime;
        hasRoundStarted = false;
        gameOverUI.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        // countdownTimer += Time.deltaTime;

        if(hasRoundStarted)
        {
            if (countdownTimer < roundTime) //still going
            {
                countdownTimer += Time.deltaTime;
                // argumentMeter.value = (countdownMax - countdownTimer) / roundTime;
                argumentMeter.value =  (countdownTimer / roundTime) * roundTime;
                Debug.Log("count: " + countdownTimer + " time: " + Time.deltaTime + " round time: " + roundTime);
        
            }
            else //round over
            {
                //reset everything
                countdownTimer = 0;
                hasRoundStarted = false;
                argumentMeter.value = 0; 
                m_BasicSpawnManager.canSlappa = false;
                
                //show round over screen
                gameOverUI.SetActive(true);
                roundOverText.text = "Round: " + roundNum + "\nManual Sections Unlocked: " + manualSectionsUnlocked + "\nLife Satisfaction: " + lifeSatisfaction + "\nLet's see what you thought of that...";

                /*
                personalSatisfaction = m_ObjectiveTest.personalSatisfaction;
                if (personalSatisfaction >= .5f)
                {
                    roundOverText.text = "Round: " + roundNum + "\nYou like this layout!\nPersonal Satisfaction: " + personalSatisfaction;
                }
                else 
                {
                    roundOverText.text = "Round: " + roundNum + "\nUgh. You do not like this layout.\nPersonal Satisfaction: " + personalSatisfaction;
                }
                */
            }
        }
    }
    public void OnManualUnlock()
    {
        manualSectionsUnlocked++;
        lifeSatisfaction -= satisfactionPenalty;
        UpdateRoundText();
    }

    public void OnSatisfactionUp()
    {
        lifeSatisfaction += satisfactionBonus;
        UpdateRoundText(); //excessive but okay
    }

    public void UpdateRoundText()
    {
        roundOverText.text = "Round: " + roundNum + "\nManual Sections Unlocked: " + manualSectionsUnlocked + "\nLife Satisfaction: " + lifeSatisfaction + "\nLet's see what you thought of that...";
    }

    public void OnNextRound() //advance (manual until arcade events)
    {
        roundNum++;
        hasRoundStarted = true;
        gameOverUI.SetActive(false);
        // m_ObjectiveTest.AssignObjectivesBasic();
        m_ColliderManager.ResetFloorPlan();
        m_BasicSpawnManager.canSlappa = true;

    }
    /*
    public void OnStartOver() //reset everything without placing new floorplan
    {
        //reset
        roundNum = 0;
        hasRoundStarted = true;
        gameOverUI.SetActive(false);

        m_ObjectiveTest.ResetObjectives();
        m_ColliderManager.ResetFloorPlan();
    }
    */
}
