using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class ColliderManager : MonoBehaviour
{
    // [SerializeField] TextMeshProUGUI colliderText;
    // [SerializeField] GameObject mainRoom;
    // [SerializeField] GameObject bathroom;
    // public float mainRoomCount = 0;
    // public float bathroomCount = 0;

    public List<GameObject> floorPlanPile = new List<GameObject>(); //total heap of furn
    // public List<GameObject> bathroomPile = new List<GameObject>(); //total heap of furn
    // public List<GameObject> mainRoomPile = new List<GameObject>(); //total heap of furn
    //for now just storing the collider's furniture

    void Start()
    {
        // colliderText.text = "Main Room: " + mainRoomCount + " -- Bathroom: " + bathroomCount;
    }

    // Update is called once per frame
    void Update()
    {
        //going to have issues if network removes when client leaves.. but maybe that's not an issue
        /* //enumeration issue, need to resolve later
        foreach (GameObject furn in mainRoomPile) //too expensive?
        {
            if (bathroomPile.Contains(furn)) //this is annoying because exclusive...
                mainRoomPile.Remove(furn);
        }
        */
        // mainRoom.GetComponent<BoxCollider>().OnTr/
        // colliderText.text = "Main Room: " + mainRoomCount + " -- Bathroom: " + bathroomCount;
    }

    public void ResetFloorPlan()
    {
        // if(PhotonNetwork.IsMasterClient)
        // {
            foreach(GameObject furn in floorPlanPile)
            {
                // PhotonNetwork.DestroyPlayerObjects //would destory ARCam too?
                PhotonNetwork.Destroy(furn);
            }
        // }
    }
}
