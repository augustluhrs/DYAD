using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_Bathroom : MonoBehaviour
{
    private int p1StudioCount = 0;
    private int p2StudioCount = 0;

    private int p1BathroomCount = 0;

    
    private int p2BathroomCount = 0;

    public int p1BathroomScore = 0;
    public int p2BathroomScore = 0;

    private int scoreTimer = 500;
    private int frameCount = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        frameCount++;
        if (frameCount % scoreTimer == 0)
        {
            if(p1BathroomCount > p2BathroomCount)
            {
                p1BathroomScore++;
            }
            if(p2BathroomCount > p1BathroomCount)
            {
                p2BathroomScore++;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "player1")
        {
            p1BathroomCount++;
        }
        if (other.tag == "player2")
        {
            p2BathroomCount++;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.tag == "player1")
        {
            p1BathroomCount--;
        }
        if (other.tag == "player2")
        {
            p2BathroomCount--;
        }
    }
}
