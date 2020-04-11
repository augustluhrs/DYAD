using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class SpawnManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab; //now just for avatar / ar cam location
    public GameObject floorPlan;
    public Camera arCamera;
    
    public enum RaiseEventCodes
    {
        PlayerSpawnEventCode = 0  //this is the event name?
    }
    
    void Start()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
        // arCamera = GetComponent<
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }

    #region Photon Callback Methods
    void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == (byte)RaiseEventCodes.PlayerSpawnEventCode)
        {

            object[] data = (object[])photonEvent.CustomData;
            Vector3 receivedPosition = (Vector3)data[0];
            Quaternion receivedRotation = (Quaternion)data[1];
            // int receivedPlayerSelectionData = (int)data[3];

            GameObject player = Instantiate(playerPrefab, receivedPosition + floorPlan.transform.position, receivedRotation);
            PhotonView _photonView = player.GetComponent<PhotonView>();
            _photonView.ViewID = (int)data[2];

        }
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            SpawnPlayer();
        }
    }
    #endregion


    #region Private Methods
    private void SpawnPlayer()
    {
        // object playerSelectionNumber;
        // if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(MultiplayerARSpinnerTopGame.PLAYER_SELECTION_NUMBER, out playerSelectionNumber))
        // {
            // Debug.Log("Player selection number is " + (int)playerSelectionNumber);

            // int randomSpawnPoint = Random.Range(0, spawnPositions.Length - 1);
            Vector3 instantiatePosition = arCamera.transform.position;

            GameObject playerGameobject = Instantiate(playerPrefab, instantiatePosition, Quaternion.identity);

            PhotonView _photonView = playerGameobject.GetComponent<PhotonView>();

            if (PhotonNetwork.AllocateViewID(_photonView))
            {
                object[] data = new object[]
                {

                    playerGameobject.transform.position - floorPlan.transform.position, 
                    playerGameobject.transform.rotation, 
                    _photonView.ViewID
                };

                RaiseEventOptions raiseEventOptions = new RaiseEventOptions
                {
                   Receivers = ReceiverGroup.Others,
                   CachingOption = EventCaching.AddToRoomCache

                };

                SendOptions sendOptions = new SendOptions
                {
                    Reliability = true
                };

                //Raise Events!
                PhotonNetwork.RaiseEvent((byte)RaiseEventCodes.PlayerSpawnEventCode, data, raiseEventOptions, sendOptions);


            }
            else
            {

                Debug.Log("Failed to allocate a viewID");
                Destroy(playerGameobject);
            }
        // }
    }



    #endregion

}
