using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    [Header("UI Elements")] //should maybe merge with ARPlacementandSceneDetection or w/e
    [SerializeField] Slider argumentMeter;
    [SerializeField] GameObject gameOverUI;
    public bool hasRoundStarted;
    private float countdownTimer; //evalTimer
    private float countdownMax; //timerMax
    [Range(20f, 200f)][SerializeField] float roundTime = 60f; //timerFadeout

    void Start()
    {
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
                gameOverUI.SetActive(true);
            }
        }
        // else
            // countdownMax = countdownTimer + roundTime;
    }
}
