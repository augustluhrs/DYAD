﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Photon.Chat;

public class ARPlacementAndPlaneDetectionController : MonoBehaviourPunCallbacks
{

    ARPlaneManager m_ARPlaneManager;
    ARPlacementManager m_ARPlacementManager;

    [Header("UI Elements")]
    public GameObject preMatchUI;
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

    }
    void Start()
    {
        preMatchUI.SetActive(true);
        placeButton.SetActive(false);
        adjustButton.SetActive(false);
        scaleSlider.SetActive(true);
        readyButton.SetActive(false);

        roomText.text = "Room: " + PhotonNetwork.CurrentRoom.Name;

        if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            instructionsText.text = "Waiting for other player";
            isAlone = true;
        } 
        else
        {
            instructionsText.text = "Move phone around to detect planes and choose where to place your floorplan. Try to match size/orientation with your partner.";
            isAlone = false;
            placeButton.SetActive(true);
            // string partnerName;
            // foreach (PhotonPlayer p in PhotonNetwork.player)
            roomText.text = "Partner: " + PhotonNetwork.PlayerListOthers[0].NickName;
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        //hacky, should fix later [TODO]
        if(!isAlone && PhotonNetwork.CurrentRoom.PlayerCount != 1)
        {
            instructionsText.text = "Move phone around to detect planes and choose where to place your floorplan. Try to match size/orientation with your partner.";
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
    }

    public void OnQuitMatch()
    {
        SceneLoader.Instance.LoadScene("Scene_Lobby");
    }

    public void DisableARPlacementAndPlaneDetection()
    {
        m_ARPlaneManager.enabled = false;
        m_ARPlacementManager.enabled = false;
        SetAllPlanesActiveOrDeactive(false);

        placeButton.SetActive(false);
        scaleSlider.SetActive(false);
        adjustButton.SetActive(true);
        readyButton.SetActive(true);

        instructionsText.text = "Press ready to begin";
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
