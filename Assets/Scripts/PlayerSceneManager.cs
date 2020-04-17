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
    private string playerName;
    // public TextMeshPro playerNameTMP;
    //still don't really get the diff between SerializeField and public

    [Header("Connection Status UI")]
    [SerializeField] GameObject connectionPanel;
    [SerializeField] GameObject connectionText;
    public bool showConnectionStatus = false; 

    [Header("GameMode UI")]
    [SerializeField] GameObject gameModeUI;

    void Start()
    {
        // Debug.Log("player name:" + playerName);
        // Debug.Log("bool: " + string.IsNullOrEmpty(playerName));

        //if coming back from sandbox, don't redo name
        if(!string.IsNullOrEmpty(playerName)) //already has a name
        {
            Debug.Log("returning player");
            playerPanel.SetActive(false);
            connectionPanel.SetActive(false);
            gameModeUI.SetActive(true);
        }
        else //first time here
        {
            Debug.Log("new player");
            playerPanel.SetActive(true); //TODO change this to the UI convention
            connectionPanel.SetActive(false);
            gameModeUI.SetActive(false);
        }
        // playerPanel.SetActive(true); //TODO change this to the UI convention
        // connectionPanel.SetActive(false);
        // gameModeUI.SetActive(false);
        // playerNameTMP = playerNameInputField.GetComponent<TextMeshPro>();
        // playerNameInputField
    }

    void Update()
    {
        // if (PhotonNetwork.IsConnectedAndReady) //this is fine, just wish there was more time spent on the text...
        // {
        //     //connected, go to next scene
            
        //     SceneLoader.Instance.LoadScene("Scene_Lobby");
        //     // connectionText.GetComponent<TextMeshProUGUI>().text = "Connection Status: " + PhotonNetwork.NetworkClientState;

        // }
        // else if (showConnectionStatus) // pressed button, connecting
        
        if (showConnectionStatus) // pressed button, connecting
        {
            connectionText.GetComponent<TextMeshProUGUI>().text = "Connection Status: " + PhotonNetwork.NetworkClientState;
            // text = "Connection Status: " + PhotonNetwork.NetworkClientState;
        }
        // else //hasn't pressed yet
        // {
        //     
        // }
    }

    public void OnNameInput()
    {
        // string playerName = playerNameText.text;
        // Debug.Log(playerNameTMP.text);
        // Debug.Log(playerNameInputField.TryGetComponent<TextMeshPro>(out TextMeshPro tmp));
        playerName = playerNameInputField.GetComponent<TextMeshProUGUI>().text;

        if (!string.IsNullOrEmpty(playerName))
        {
            playerPanel.SetActive(false);
            PhotonNetwork.LocalPlayer.NickName = playerName;
            gameModeUI.SetActive(true);
            // connectionPanel.SetActive(true);
            // showConnectionStatus = true;

            // if (!PhotonNetwork.IsConnected)
            // {
            //     PhotonNetwork.LocalPlayer.NickName = playerName;

            //     PhotonNetwork.ConnectUsingSettings();
            //     // PhotonNetwork.ConnectToMaster();
            // }
        }
        else
            Debug.Log("Player name invalid or empty!");
    }

    public void OnFindRoom()
    {
        gameModeUI.SetActive(false);
        connectionPanel.SetActive(true);
        showConnectionStatus = true;

        if (!PhotonNetwork.IsConnected)
        {
            // PhotonNetwork.LocalPlayer.NickName = playerName;

            PhotonNetwork.ConnectUsingSettings();
            // PhotonNetwork.ConnectToMaster();
        }
    }

    public void OnTutorialSandbox()
    {
        SceneLoader.Instance.LoadScene("Scene_TutorialSandbox");
    }

    #region Photon Callbacks
    public override void OnConnectedToMaster()
    {
        Debug.Log("joined lobby");
        SceneLoader.Instance.LoadScene("Scene_Lobby");
    }
    #endregion
}
