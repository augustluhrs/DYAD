using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class PlayerSceneManager : MonoBehaviourPunCallbacks
{

    [Header("Player Panel UI")]
    [SerializeField] GameObject playerPanel;
    [SerializeField] public GameObject playerNameInputField;
    // public TextMeshPro playerNameTMP;
    //still don't really get the diff between SerializeField and public

    [Header("Connection Status UI")]
    [SerializeField] GameObject connectionPanel;
    [SerializeField] GameObject connectionText;
    public bool showConnectionStatus = false; 

    void Start()
    {
        //need to do this? just to be safe?
        playerPanel.SetActive(true);
        connectionPanel.SetActive(false);
        // playerNameTMP = playerNameInputField.GetComponent<TextMeshPro>();
        // playerNameInputField
    }

    void Update()
    {
        if (PhotonNetwork.IsConnectedAndReady) //this is fine, just wish there was more time spent on the text...
        {
            //connected, go to next scene
            SceneLoader.Instance.LoadScene("Scene_Lobby");
            // connectionText.GetComponent<TextMeshProUGUI>().text = "Connection Status: " + PhotonNetwork.NetworkClientState;

        }
        else if (showConnectionStatus) // pressed button, connecting
        {
            connectionText.GetComponent<TextMeshProUGUI>().text = "Connection Status: " + PhotonNetwork.NetworkClientState;
            // text = "Connection Status: " + PhotonNetwork.NetworkClientState;
        }
        // else //hasn't pressed yet
        // {
        //     
        // }
    }

    public void OnFindRoom()
    {
        // string playerName = playerNameText.text;
        // Debug.Log(playerNameTMP.text);
        // Debug.Log(playerNameInputField.TryGetComponent<TextMeshPro>(out TextMeshPro tmp));
        string playerName = playerNameInputField.GetComponent<TextMeshProUGUI>().text;

        if (!string.IsNullOrEmpty(playerName))
        {
            playerPanel.SetActive(false);
            connectionPanel.SetActive(true);
            showConnectionStatus = true;

            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.LocalPlayer.NickName = playerName;

                PhotonNetwork.ConnectUsingSettings();
                // PhotonNetwork.ConnectToMaster();
            }
        }
        else
            Debug.Log("Player name invalid or empty!");
    }
}
