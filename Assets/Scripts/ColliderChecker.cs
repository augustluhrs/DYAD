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
    ColliderManager m_ColliderManager;
    [SerializeField] bool isBathroom;
    [SerializeField] bool isMainRoom;
    // private float colliderCount = 0;
    // public float mainRoomCount = 0;
    // public float bathroomCount = 0;

    void Start()
    {
        // m_ColliderTest = gameManager.GetComponent<ColliderTest>();
        m_ColliderManager = gameManager.GetComponent<ColliderManager>();
    }

    private void OnTriggerEnter(Collider other) //need to see if is too expensive...
    {
        m_ColliderManager.floorPlanPile.Add(other.gameObject);
        if (isBathroom)
            m_ColliderManager.bathroomPile.Add(other.gameObject);
        // can't do this because not a discrete collider...
        if (isMainRoom)
            m_ColliderManager.mainRoomPile.Add(other.gameObject);
        
        
        // colliderCount++;
        // if (isBathroom)
        //     m_ColliderTest.bathroomCount = colliderCount;
        // if (isMainRoom)
        //     m_ColliderTest.mainRoomCount = colliderCount;
    }

    private void OnTriggerExit(Collider other) 
    {
        m_ColliderManager.floorPlanPile.Remove(other.gameObject);
        if (isBathroom)
            m_ColliderManager.bathroomPile.Remove(other.gameObject);
        if (isMainRoom)
        {
           if(m_ColliderManager.mainRoomPile.Contains(other.gameObject)) //to prevent error if it's been removed
                m_ColliderManager.mainRoomPile.Remove(other.gameObject); 
        }
            
        
        // colliderCount--;
        // if (isBathroom)
        //     m_ColliderTest.bathroomCount = colliderCount;
        // if (isMainRoom)
        //     m_ColliderTest.mainRoomCount = colliderCount;
    }

    void Update()
    {
    }
}

