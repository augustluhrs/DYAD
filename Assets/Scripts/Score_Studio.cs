using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_Studio : MonoBehaviour
{
    private int p1StudioCount = 0;
    private int p2StudioCount = 0;
    public int p1StudioScore = 0;
    public int p2StudioScore = 0;

    private int scoreTimer = 200; // little faster than bathroom b/c weighted more
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
            if(p1StudioCount > p2StudioCount)
            {
                p1StudioScore++;
            }
            if(p2StudioCount > p1StudioCount)
            {
                p2StudioScore++;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "player1")
        {
            p1StudioCount++;
        }
        if (other.tag == "player2")
        {
            p2StudioCount++;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.tag == "player1")
        {
            p1StudioCount--;
        }
        if (other.tag == "player2")
        {
            p2StudioCount--;
        }
    }
}
