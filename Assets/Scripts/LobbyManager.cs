using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

//all based on @tevfikufuk's Udemy Course, "Building Multiplayer AR Games with Photon"

public class LobbyManager : MonoBehaviourPunCallbacks
{
    
    [Header("Button UI")]
    public GameObject roomNameInputField;
    public GameObject buttonPanel;
    public GameObject roomNamePanel;
    public GameObject headlinePanel;
    private string roomName;

    void Start()
    {
        // Debug.Log(PhotonNetwork.IsConnectedAndReady);
        roomNamePanel.SetActive(false);
        buttonPanel.SetActive(true);

    }

    void Update()
    {
        
    }

    public void OnBlindDate()
    {
        PhotonNetwork.JoinRandomRoom(); //built in callbacks OnJoined/Failed
    }

    public void OnCreateRoom()
    {
        //photon.realtime
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        roomOptions.IsOpen = true;

        //Creating the room
        PhotonNetwork.CreateRoom(PhotonNetwork.LocalPlayer.NickName,roomOptions);
    }
    
    public void OnJoinRoom()
    {
        buttonPanel.SetActive(false);
        roomNamePanel.SetActive(true);
    }

    public void OnJoinRoomWithName()
    {
        roomName = roomNameInputField.GetComponent<TextMeshProUGUI>().text;
        PhotonNetwork.JoinRoom(roomName);
    }


    #region PHOTON Callback Methods
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
      
        Debug.Log(message);
        // uI_InformText.text = message;
        CreateAndJoinRoom();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log(message);

        headlinePanel.GetComponentInChildren<TextMeshProUGUI>().text = "Error, invalid room name";
    }


    public override void OnJoinedRoom()
    {
        // adjust_button.SetActive(false);
        // raycastCenter_Image.SetActive(false);
        
        // if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        // {
        //     uI_InformText.text = "Joined to " + PhotonNetwork.CurrentRoom.Name + ". Waiting for other players...";


        // }
        // else
        // {
        //     uI_InformText.text = "Joined to " + PhotonNetwork.CurrentRoom.Name;
        //     StartCoroutine(DeactivateAfterSeconds(uI_InformPanelGameobject, 2.0f));
        // }

        // Debug.Log( " joined to "+ PhotonNetwork.CurrentRoom.Name);

        SceneLoader.Instance.LoadScene("Scene_Gameplay");
        Debug.Log("after joined/scene");
    }
    #endregion

    #region PRIVATE Methods
    void CreateAndJoinRoom()
    {
        string randomRoomName = "Room" + Random.Range(0,1000);

        //photon.realtime
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        roomOptions.IsOpen = true;

        //Creating the room
        PhotonNetwork.CreateRoom(randomRoomName,roomOptions);
    }

    // IEnumerator DeactivateAfterSeconds(GameObject _gameObject, float _seconds)
    // {
    //     yield return new WaitForSeconds(_seconds);
    //     _gameObject.SetActive(false);

    // }


    #endregion


}
