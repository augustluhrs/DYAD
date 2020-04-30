using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
// using Photon.Chat;

public class ARPlacementAndPlaneDetectionController : MonoBehaviourPunCallbacks
{ //TODO rename this to some sort of manager
    //the preMatch Gameplay script

    ARPlaneManager m_ARPlaneManager;
    ARPlacementManager m_ARPlacementManager;
    BasicClickDropTest m_basicClickDropTest;
    BasicSpawnManager m_basicSpawnManager;
    GameplayManager m_GameplayManager;
    GameplayLTDemoManager m_GameplayLTDemoManager;

    [SerializeField] GameObject floorPlan;
    [SerializeField] GameObject ARCam;

    [Header("UI Elements")]
    public GameObject preMatchUI;
    public GameObject matchUI;
    public GameObject quitCheckUI;
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
        m_GameplayManager = GetComponent<GameplayManager>();
        m_GameplayLTDemoManager = GetComponent<GameplayLTDemoManager>();

    }
    void Start()
    {
        preMatchUI.SetActive(true);
        matchUI.SetActive(false);
        quitCheckUI.SetActive(false);
        placeButton.SetActive(false);
        adjustButton.SetActive(false);
        scaleSlider.SetActive(true);
        readyButton.SetActive(false);
        m_basicClickDropTest.enabled = false;
        // m_basicClickDropTest.enabled = true;

        roomText.text = "Room: " + PhotonNetwork.CurrentRoom.Name;

        if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            instructionsText.text = "Waiting for other player";
            // isAlone = true;
        } 
        else
        {
            //all this happens in update, redundant:

            // instructionsText.text = "Move phone around to detect planes and choose where to place your floorplan. Try to match size/orientation with your partner.";
            // isAlone = false;
            // placeButton.SetActive(true);
            // string partnerName;
            // foreach (PhotonPlayer p in PhotonNetwork.player)
            // roomText.text = "Partner: " + PhotonNetwork.PlayerListOthers[0].NickName;
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        //hacky, should fix later [TODO] -- use OnPlayerEnter()?
        if(isAlone && PhotonNetwork.CurrentRoom.PlayerCount != 1)
        {
            instructionsText.text = "Point your camera down to the floor and move your phone around to find your floorplan. Once it appears, use the slider and scale it until you can see the whole apartment. Press place when ready.";
            isAlone = false;
            placeButton.SetActive(true);
            roomText.text = "Partner: " + PhotonNetwork.PlayerListOthers[0].NickName;
        }

    }

    public void OnReady()
    {
        // adjustButton.SetActive(false);
        // readyButton.SetActive(false);
        preMatchUI.SetActive(false);
        matchUI.SetActive(true);
        // m_basicClickDropTest.enabled = true;
        m_basicSpawnManager.canSlappa = true;
        m_GameplayManager.hasRoundStarted = true;
        m_GameplayLTDemoManager.hasRoundStarted = true;
    }
    
    public void OnQuitCheck()
    {
        quitCheckUI.SetActive(true);
    }

    public void OnBackToGame() //after back in quit check menu
    {
        quitCheckUI.SetActive(false);
    }
    public void OnQuitMatch()
    {
        // SceneLoader.Instance.LoadScene("Scene_Lobby");
        PhotonNetwork.LeaveRoom();
        Debug.Log("attempting to leave");
    }

    public void OnBackToMenu() //only in tutorial
    {
        //TODO
        SceneLoader.Instance.LoadScene("Scene_PlayerProfile");
    }

    public override void OnLeftRoom()
    {
        Debug.Log("left room");
        SceneLoader.Instance.LoadScene("Scene_Lobby");
    }

    public void DisableARPlacementAndPlaneDetection()
    {
        //now should only place if in valid spot
        if (floorPlan.transform.position.y < ARCam.transform.position.y)
        {
            m_ARPlaneManager.enabled = false;
            m_ARPlacementManager.enabled = false;
            SetAllPlanesActiveOrDeactive(false);

            placeButton.SetActive(false);
            scaleSlider.SetActive(false);
            adjustButton.SetActive(true);
            readyButton.SetActive(true);

            instructionsText.text = "To start, press READY at the same time as your partner!";
        }
        // m_ARPlaneManager.enabled = false;
        // m_ARPlacementManager.enabled = false;
        // SetAllPlanesActiveOrDeactive(false);

        // placeButton.SetActive(false);
        // scaleSlider.SetActive(false);
        // adjustButton.SetActive(true);
        // readyButton.SetActive(true);

        // instructionsText.text = "Press ready to begin";
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

        // instructionsText.text = "Move phone around to detect planes and choose where to place your floorplan. Try to match size/orientation with your partner.";
        instructionsText.text = "Point your camera down to the floor and move your phone around to find your floorplan. Once it appears, use the slider and scale it until you can see the whole apartment. Press place when ready.";

    }

    public void SetAllPlanesActiveOrDeactive(bool value)
    {
        foreach(var plane in m_ARPlaneManager.trackables)
        {
            plane.gameObject.SetActive(value);
        }
    }
}
