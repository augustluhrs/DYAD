using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
// using Photon.Chat;

public class TutorialManager : MonoBehaviour
{ //TODO rename this to some sort of manager
    //the preMatch Gameplay script

    ARPlaneManager m_ARPlaneManager;
    ARPlacementManager m_ARPlacementManager;
    BasicClickDropTest m_basicClickDropTest;
    BasicSpawnManager m_basicSpawnManager;
    JoystickManager m_JoystickManager;
    [SerializeField] GameObject wheelSelector;
    [SerializeField] GameObject ARCam;
    [SerializeField] GameObject floorPlan;
    public bool canSlappa = false;

    [Header("Spawning Furniture")]
    public string selectedFurniture;
    [SerializeField] GameObject odwar;
    [SerializeField] GameObject tullsta;
    [SerializeField] GameObject brimnes;
    [SerializeField] GameObject ektorp;

    [Header("UI Elements")]
    public GameObject preMatchUI;
    public GameObject matchUI;
    // public GameObject quitCheckUI;
    public GameObject placeButton;
    public GameObject adjustButton;
    public GameObject readyButton;
    //search for games button
    public TextMeshProUGUI roomText;
    public TextMeshProUGUI instructionsText;
    public GameObject scaleSlider;

    private bool isAlone;


    private void Awake() {
        m_ARPlacementManager = GetComponent<ARPlacementManager>();
        m_ARPlaneManager = GetComponent<ARPlaneManager>();
        m_basicClickDropTest = GetComponent<BasicClickDropTest>();
        m_basicSpawnManager = GetComponent<BasicSpawnManager>();
        m_JoystickManager = wheelSelector.GetComponent<JoystickManager>();

    }
    void Start()
    {
        preMatchUI.SetActive(true);
        matchUI.SetActive(false);
        // quitCheckUI.SetActive(false);
        placeButton.SetActive(false);
        adjustButton.SetActive(false);
        scaleSlider.SetActive(true);
        readyButton.SetActive(false);
        m_basicClickDropTest.enabled = false;
        // m_basicClickDropTest.enabled = true;

        // roomText.text = "Room: " + PhotonNetwork.CurrentRoom.Name;

        // if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
        // {
        //     instructionsText.text = "Waiting for other player";
        //     isAlone = true;
        // } 
        // else
        // {
        //     instructionsText.text = "Move phone around to detect planes and choose where to place your floorplan. Try to match size/orientation with your partner.";
        //     isAlone = false;
        //     placeButton.SetActive(true);
        //     // string partnerName;
        //     // foreach (PhotonPlayer p in PhotonNetwork.player)
        //     roomText.text = "Partner: " + PhotonNetwork.PlayerListOthers[0].NickName;
        // }

        // instructionsText.text = "Move phone around to detect planes and choose where to place your floorplan. Try to match size/orientation with your partner.";
        instructionsText.text = "Point your camera down to the floor and move your phone around to find your floorplan. Once it appears, use the slider and scale it until you can see the whole apartment. Press place when ready.";
        
        isAlone = false;
        placeButton.SetActive(true);
        // roomText.text = "Partner: " + PhotonNetwork.PlayerListOthers[0].NickName;
            
    }

    void Update() //this is all just for spawning
    {
        if(canSlappa)
        {
            selectedFurniture = m_basicSpawnManager.selectedFurniture;

            foreach (Touch touch in Input.touches)
            {
                if (touch.fingerId == 0) //first press
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    //to prevent wheel selector from spawning, nope, doesn't work because by the time touch ended, the joystick defaults
                    // if (Input.GetTouch(0).phase == TouchPhase.Ended && m_JoystickManager.fixedJoystick.Direction.y == 0f)
                    {
                        GameObject newSpawn = new GameObject();
                        if (selectedFurniture == "AR_Odwar_brown")
                        {
                            newSpawn = Instantiate(odwar, ARCam.transform.position, Quaternion.identity); 
                        }
                        if (selectedFurniture == "AR_Tullsta_white")
                        {
                            newSpawn = Instantiate(tullsta, ARCam.transform.position, Quaternion.identity); 
                        }
                        if (selectedFurniture == "AR_Brimnes_red")
                        {
                            newSpawn = Instantiate(brimnes, ARCam.transform.position, Quaternion.identity); 
                        }
                        if (selectedFurniture == "AR_Biggio_black")
                        {
                            newSpawn = Instantiate(ektorp, ARCam.transform.position, Quaternion.identity); 
                        }
                        // newSpawn = Instantiate(selectedFurniture, ARCam.transform.position, Quaternion.identity); //duh i need to do rotation here not below, oh wait cant because not prefab technically? fine b/c network updates before it's an issue hopefully
                        // newSpawn.transform.eulerAngles = new Vector3(newSpawn.transform.eulerAngles.x, ARCam.transform.eulerAngles.y, newSpawn.transform.eulerAngles.z);
                        // new Quaternion rotation?
                        newSpawn.transform.rotation = new Quaternion(newSpawn.transform.rotation.x, ARCam.transform.rotation.y, newSpawn.transform.rotation.z, newSpawn.transform.rotation.w); //hope this works
                        newSpawn.transform.parent = floorPlan.transform; //will this affect theirs if my floorplan moves??
                    }
                }
            }
        }
    }

    public void OnReady()
    {
        // adjustButton.SetActive(false);
        // readyButton.SetActive(false);
        preMatchUI.SetActive(false);
        matchUI.SetActive(true);
        // m_basicClickDropTest.enabled = true;
        // m_basicSpawnManager.canSlappa = true;
        canSlappa = true;
        // instructionsText.text = "Keep the floorplan in view or else it might float away and relocate!";

    }
    
    // public void OnQuitCheck()
    // {
    //     quitCheckUI.SetActive(true);
    // }

    // public void OnBackToGame() //after back in quit check menu
    // {
    //     quitCheckUI.SetActive(false);
    // }
    // public void OnQuitMatch()
    // {
    //     // SceneLoader.Instance.LoadScene("Scene_Lobby");
    //     PhotonNetwork.LeaveRoom();
    //     Debug.Log("attempting to leave");
    // }

    public void OnBackToMenu() //only in tutorial
    {
        //TODO
        SceneLoader.Instance.LoadScene("Scene_PlayerProfile");
    }

    // public override void OnLeftRoom()
    // {
    //     Debug.Log("left room");
    //     SceneLoader.Instance.LoadScene("Scene_Lobby");
    // }

    public void DisableARPlacementAndPlaneDetection()
    {
        m_ARPlaneManager.enabled = false;
        m_ARPlacementManager.enabled = false;
        SetAllPlanesActiveOrDeactive(false);

        placeButton.SetActive(false);
        scaleSlider.SetActive(false);
        adjustButton.SetActive(true);
        readyButton.SetActive(true);

        instructionsText.text = "Keep the floorplan in view or else it might float away and relocate! Press ready to begin";
        
        //should have one player's press trigger both, to highlight communicating
    }

    public void EnableARPlacementAndPlaneDetection()
    {
        m_ARPlaneManager.enabled = true;
        m_ARPlacementManager.enabled = true;
        SetAllPlanesActiveOrDeactive(true);

        placeButton.SetActive(true);
        scaleSlider.SetActive(true);
        adjustButton.SetActive(false);
        readyButton.SetActive(false);

        instructionsText.text = "Move phone around to detect planes and choose where to place your floorplan. Try to match size/orientation with your partner.";

    }

    public void SetAllPlanesActiveOrDeactive(bool value)
    {
        foreach(var plane in m_ARPlaneManager.trackables)
        {
            plane.gameObject.SetActive(value);
        }
    }
}

