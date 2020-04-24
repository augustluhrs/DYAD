using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayManager : MonoBehaviour
{
    ObjectiveTest m_ObjectiveTest;
    ColliderManager m_ColliderManager;
    
    [Header("UI Elements")] //should maybe merge with ARPlacementandSceneDetection or w/e
    [SerializeField] Slider argumentMeter;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] TextMeshProUGUI roundOverText;
    public float personalSatisfaction;
    public bool hasRoundStarted;
    private float roundNum;
    //argument timer stuff
    private float countdownTimer; //evalTimer
    // private float countdownMax; //timerMax
    [Range(20f, 200f)][SerializeField] float roundTime = 60f; //timerFadeout

    void Start()
    {
        m_ObjectiveTest = GetComponent<ObjectiveTest>();
        m_ColliderManager = GetComponent<ColliderManager>();

        roundNum = 0;
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
                
                //show round over screen
                gameOverUI.SetActive(true);
                personalSatisfaction = m_ObjectiveTest.personalSatisfaction;
                if (personalSatisfaction >= .5f)
                {
                    roundOverText.text = "Round: " + roundNum + "\nYou like this layout!\nPersonal Satisfaction: " + personalSatisfaction;
                }
                else 
                {
                    roundOverText.text = "Round: " + roundNum + "\nUgh. You do not like this layout.\nPersonal Satisfaction: " + personalSatisfaction;
                }
            }
        }
    }

    public void OnStartOver() //reset everything without placing new floorplan
    {
        //reset
        roundNum = 0;
        hasRoundStarted = true;
        gameOverUI.SetActive(false);

        m_ObjectiveTest.ResetObjectives();
        m_ColliderManager.ResetFloorPlan();
    }

    public void OnNextRound() //advance (manual until arcade events)
    {
        roundNum++;
        hasRoundStarted = true;
        gameOverUI.SetActive(false);

        m_ObjectiveTest.AssignObjectivesBasic();
        m_ColliderManager.ResetFloorPlan();

    }
}
