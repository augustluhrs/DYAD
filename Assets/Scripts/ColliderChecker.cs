using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColliderChecker : MonoBehaviour
{
    // [SerializeField] TextMeshProUGUI colliderText;
    [SerializeField] GameObject gameManager; //AR session Origin
    ColliderTest m_ColliderTest;
    [SerializeField] bool isBathroom;
    [SerializeField] bool isMainRoom;
    private float colliderCount = 0;
    // public float mainRoomCount = 0;
    // public float bathroomCount = 0;

    void Start()
    {
        m_ColliderTest = gameManager.GetComponent<ColliderTest>();
    }

    private void OnTriggerEnter(Collider other) //need to see if is too expensive...
    {
        colliderCount++;
        if (isBathroom)
            m_ColliderTest.bathroomCount = colliderCount;
        if (isMainRoom)
            m_ColliderTest.mainRoomCount = colliderCount;
    }

    private void OnTriggerExit(Collider other) 
    {
        colliderCount--;
        if (isBathroom)
            m_ColliderTest.bathroomCount = colliderCount;
        if (isMainRoom)
            m_ColliderTest.mainRoomCount = colliderCount;
    }

    

    void Update()
    {
    }
}

