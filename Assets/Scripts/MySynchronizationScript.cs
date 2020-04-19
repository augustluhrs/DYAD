using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MySynchronizationScript : MonoBehaviour, IPunObservable
{
    PhotonView photonView;

    Vector3 networkedPosition;
    Quaternion networkedRotation;

    private float distance;
    private float angle;


    //Photon View Options
    public bool isTeleportEnabled = true;
    public float teleportIfDistanceGreaterThan = 1.0f;

    private GameObject floorPlan;

    private void Awake() 
    {
        photonView = GetComponent<PhotonView>();
        networkedPosition = new Vector3();
        networkedRotation = new Quaternion();
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
            transform.rotation = Quaternion.RotateTowards(transform.rotation, networkedRotation, angle*(1.0f/ PhotonNetwork.SerializationRate));
        }

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //Then, photonView is mine and I am the one who controls this player.
            //should send position, rotation data to the other players
            stream.SendNext(transform.position - floorPlan.transform.position);
            // stream.SendNext(transform.rotation - floorPlan.transform.rotation); 
            //is this all that's needed for players to have the same map synced up?
            Quaternion relativeQuat = Quaternion.Inverse(transform.rotation) * floorPlan.transform.rotation; //thanks to u/Mike 3 on unity forums
            stream.SendNext(relativeQuat);
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

            //Called on my player's gameobjects that exist in remote player's game

            networkedPosition = (Vector3)stream.ReceiveNext() + floorPlan.transform.position;
            networkedRotation = (Quaternion)stream.ReceiveNext();

            // transform.rotation = networkedRotation; //is that it? oh no, already doing it in fixedupdate

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
