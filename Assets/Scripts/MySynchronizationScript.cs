using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MySynchronizationScript : MonoBehaviour, IPunObservable
{
    PhotonView photonView;

    Vector3 networkedPosition;
    private float distance;

    //Photon View Options
    public bool isTeleportEnabled = true;
    public float teleportIfDistanceGreaterThan = 1.0f;

    private GameObject floorPlan;

    private void Awake() 
    {
        photonView = GetComponent<PhotonView>();
        networkedPosition = new Vector3();
        floorPlan = GameObject.Find("FloorPlan");
    }

    void Start()
    {  
    }

    void Update()
    { 
    }

    private void FixedUpdate()
    {

        if (!photonView.IsMine)
        {
            transform.position = Vector3.MoveTowards(transform.position, networkedPosition, distance*(1.0f/ PhotonNetwork.SerializationRate));
            // rb.rotation = Quaternion.RotateTowards(rb.rotation, networkedRotation, angle*(1.0f/ PhotonNetwork.SerializationRate));
        }

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //Then, photonView is mine and I am the one who controls this player.
            //should send position, velocity etc. data to the other players
            stream.SendNext(transform.position - floorPlan.transform.position);
            // stream.SendNext(rb.rotation);

            // if (synchronizeVelocity)
            // {
            //     stream.SendNext(rb.velocity);
            // }

            // if (synchronizeAngularVelocity)
            // {
            //     stream.SendNext(rb.angularVelocity);
            // }
        }
        else
        {

            //Called on my player gameobject that exists in remote player's game

            networkedPosition = (Vector3)stream.ReceiveNext() + floorPlan.transform.position;
            // networkedRotation = (Quaternion)stream.ReceiveNext();

            if (isTeleportEnabled)
            {
                if (Vector3.Distance(transform.position, networkedPosition) > teleportIfDistanceGreaterThan)
                {
                    transform.position = networkedPosition;
                }
            }

        //     if (synchronizeVelocity || synchronizeAngularVelocity)
        //     {
        //         float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));

        //         if (synchronizeVelocity)
        //         {
        //             rb.velocity = (Vector3)stream.ReceiveNext();

        //             networkedPosition += rb.velocity * lag;

        //             distance = Vector3.Distance(rb.position, networkedPosition);
        //         }

        //         if (synchronizeAngularVelocity)
        //         {
        //             rb.angularVelocity = (Vector3)stream.ReceiveNext();

        //             networkedRotation = Quaternion.Euler(rb.angularVelocity*lag)*networkedRotation;

        //             angle = Quaternion.Angle(rb.rotation, networkedRotation);
        //         }
        //     }
        }
    }
}
