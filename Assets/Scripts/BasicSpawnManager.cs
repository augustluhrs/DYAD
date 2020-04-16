using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BasicSpawnManager : MonoBehaviour
{
    public GameObject ARCam;
    // public GameObject avatarPrefab; //resources
    public GameObject floorPlan;
    public bool canSlappa = false;
    public bool wasFirst = false;
    public string selectedFurniture;

    void Start()
    {
        GameObject avatar = PhotonNetwork.Instantiate("AR_Avatar_BasicCube", ARCam.transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
        avatar.transform.parent = ARCam.transform.parent;
        //not getting the floor plan offset...
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            wasFirst = true;
        else
            Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        // Debug.Log(selectedFurniture);
    }

    // Update is called once per frame
    void Update()
    {
        if(canSlappa)
        {
            // Debug.Log(selectedFurniture);

            foreach (Touch touch in Input.touches)
            {
                if (touch.fingerId == 0) //first press
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        // GameObject otherSpawn;
                        // if (wasFirst)
                        // {
                        //     otherSpawn = PhotonNetwork.Instantiate("AR_Tullsta_white", ARCam.transform.position + (Vector3.one * 2), Quaternion.identity);
                        // }
                        // else
                        // {
                        //     otherSpawn = PhotonNetwork.Instantiate("AR_Odwar_brown", ARCam.transform.position + (Vector3.one * 2), Quaternion.identity);
                        // }
                        GameObject newSpawn = PhotonNetwork.Instantiate(selectedFurniture, ARCam.transform.position, Quaternion.identity);
                        newSpawn.transform.eulerAngles = new Vector3(newSpawn.transform.eulerAngles.x, ARCam.transform.eulerAngles.y, newSpawn.transform.eulerAngles.z);
                        newSpawn.transform.parent = floorPlan.transform;
                        // otherSpawn.transform.parent = floorPlan.transform;

                    }
                }
            }
        }
    }
}
