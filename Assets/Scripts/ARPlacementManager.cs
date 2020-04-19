using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacementManager : MonoBehaviour
{
    ARRaycastManager m_ARRaycastManager;
    static List<ARRaycastHit> raycast_Hits = new List<ARRaycastHit>();
    public Camera arCamera;
    public GameObject floorPlan;

    private void Awake() 
    {
        m_ARRaycastManager = GetComponent<ARRaycastManager>();
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 centerOfScreen = new Vector3(Screen.width/2, Screen.height/2);
        Ray ray = arCamera.ScreenPointToRay(centerOfScreen);
        
        if (m_ARRaycastManager.Raycast(ray, raycast_Hits, TrackableType.PlaneWithinPolygon))
        {
            //successful intersection between center of screen and plane detected
            Pose hitPose = raycast_Hits[0].pose;
            Vector3 positionToBePlaced = hitPose.position;

            floorPlan.transform.position = positionToBePlaced;
            //keep position and rotate on y along with player:
            // floorPlan.transform.eulerAngles = new Vector3(floorPlan.transform.eulerAngles.x, arCamera.transform.eulerAngles.y, floorPlan.transform.eulerAngles.z);
            //TODO: Quaternion rotation
            // floorPlan.transform.rotation = new Quaternion(floorPlan.transform.rotation.x, arCamera.transform.rotation.y, floorPlan.transform.rotation.z, floorPlan.transform.rotation.w);
            //trying random b/c didn't work
            floorPlan.transform.rotation = new Quaternion(floorPlan.transform.rotation.x, arCamera.transform.rotation.y, floorPlan.transform.rotation.z, 1);

        
        }
    
    }
}
