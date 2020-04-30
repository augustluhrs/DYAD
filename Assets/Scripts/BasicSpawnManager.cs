using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BasicSpawnManager : MonoBehaviour
{
    // JoystickManager m_JoystickManager;
    TrioJoystickManager m_TrioJoystickManager;
    [SerializeField] GameObject wheelSelector;
    [SerializeField] GameObject trioWheelSelector;

    
    public GameObject ARCam;
    // public GameObject avatarPrefab; //resources
    public GameObject floorPlan;
    public bool canSlappa = false;
    // public bool wasFirst = false;
    public string selectedFurniture;

    void Start()
    {
        m_TrioJoystickManager = trioWheelSelector.GetComponent<TrioJoystickManager>();
        
        // m_JoystickManager = wheelSelector.GetComponent<JoystickManager>();
        
        // GameObject avatar = PhotonNetwork.Instantiate("AR_Avatar_BasicCube", ARCam.transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
        // avatar.transform.parent = ARCam.transform.parent;
        //not getting the floor plan offset... //i dont need to? why did i write that? ah well i don't need to if they have the same floor plan location/rotation... i think
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            m_TrioJoystickManager.firstPlayer = true;
        else
            m_TrioJoystickManager.firstPlayer = false;
            // Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
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
                if (touch.fingerId == 0 && 
                    (touch.position.x >= Screen.width/3f || touch.position.y >= Screen.height/2f))
                    //first press and not over furn interface
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    //to prevent wheel selector from spawning, nope, doesn't work because by the time touch ended, the joystick defaults
                    // if (Input.GetTouch(0).phase == TouchPhase.Ended && m_JoystickManager.fixedJoystick.Direction.y == 0f)
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
                        GameObject newSpawn = PhotonNetwork.Instantiate(selectedFurniture, ARCam.transform.position, Quaternion.identity); //duh i need to do rotation here not below, oh wait cant because not prefab technically? fine b/c network updates before it's an issue hopefully
                        newSpawn.transform.eulerAngles = new Vector3(newSpawn.transform.eulerAngles.x, ARCam.transform.eulerAngles.y, newSpawn.transform.eulerAngles.z);
                        //new Quaternion rotation?
                        // newSpawn.transform.rotation = new Quaternion(newSpawn.transform.rotation.x, ARCam.transform.rotation.y, newSpawn.transform.rotation.z, newSpawn.transform.rotation.w); //hope this works
                        newSpawn.transform.parent = floorPlan.transform; //will this affect theirs if my floorplan moves??
                    }
                }
            }
        }
    }
}
