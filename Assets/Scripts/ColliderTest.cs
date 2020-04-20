using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColliderTest : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI colliderText;
    // [SerializeField] GameObject mainRoom;
    // [SerializeField] GameObject bathroom;
    public float mainRoomCount = 0;
    public float bathroomCount = 0;

    // public GameObject[] floorPlanPile; //total heap of furn


    void Start()
    {
        colliderText.text = "Main Room: " + mainRoomCount + " -- Bathroom: " + bathroomCount;
    }

    // Update is called once per frame
    void Update()
    {
        // mainRoom.GetComponent<BoxCollider>().OnTr/
        colliderText.text = "Main Room: " + mainRoomCount + " -- Bathroom: " + bathroomCount;
    }
}
