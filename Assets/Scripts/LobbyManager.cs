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
    
    // [Header("Button UI")]
    
    void Start()
    {
        Debug.Log(PhotonNetwork.IsConnectedAndReady);
    }

    void Update()
    {
        
    }

    public void OnBlindDate()
    {

    }

    public void OnCreateRoom()
    {

    }
    
    public void OnJoinRoom()
    {
        PhotonNetwork.JoinRandomRoom(); //built in callbacks OnJoin/Failed
    }


    #region PHOTON Callback Methods
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
      
        Debug.Log(message);
        // uI_InformText.text = message;
        CreateAndJoinRoom();
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

        Debug.Log( " joined to "+ PhotonNetwork.CurrentRoom.Name);

        SceneLoader.Instance.LoadScene("Scene_Gameplay");
    }
    #endregion

    #region PRIVATE Methods
    void CreateAndJoinRoom()
    {
        string randomRoomName = "Room" + Random.Range(0,1000);

        //photon.realtime
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;

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
