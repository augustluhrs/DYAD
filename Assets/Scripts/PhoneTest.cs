using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIO;

public class PhoneTest : MonoBehaviour
{
    private SocketIOComponent socket;

    //furniture models
    // [SerializeField] GameObject[] furniture_player1 = new GameObject[4];
    // [SerializeField] GameObject[] furniture_player2 = new GameObject[4];
    [SerializeField] GameObject[] flatpackArray = new GameObject[8];
    private int furniturePerPlayer = 4;
    private int flatpackIndex = 0;
    
    //interface variables
    private int p1Type = 0;
    private int p2Type = 0;
    private float p1Rotate = 90.0f;
    private float p2Rotate = -90.0f;
    private bool isFloating = false;
    private float vertOffset = 0;
    private int furnitureCount = 0;
    private GameObject lastFurniture;

    //locations & movement
    [SerializeField] Vector3 startPos; //set in inspector
    private Vector3 furniturePos;
    [SerializeField] float moveSpeed = .01f; 
    [SerializeField] float scaleFactor = 1.1f;

    //camera stuff
    /*
    [SerializeField] Transform camTarget;
    [SerializeField] Camera cam;
    private Vector3 camPos;
    private int camSpot = 0;
    [SerializeField] Vector3[] camBounds;
    private float camSpeed = 0.0005f;
    */

    //score field trigger stuff
    /*
    public int p1Score = 0;
    public int p2Score = 0;
    [SerializeField] Text p1Text;
    [SerializeField] Text p2Text;
    */

    void Start()
    {
        //socket stuff
        GameObject go = GameObject.Find("SocketIO");
		socket = go.GetComponent<SocketIOComponent>();

		socket.On("open", TestOpen);
		socket.On("error", TestError);
		socket.On("close", TestClose);
		// StartCoroutine("BeepBoop");
        socket.On("drop", DropFurniture); //slappa
        socket.On("type", ChangeType); //beskrivning
        socket.On("rotate", ChangeRotation); //vinkel
        socket.On("scale", ChangeScale); //skalla
        socket.On("move", MoveFurniture); //plats

        

        //should I not start() until the socket is connected? might move later if so
        
        //starting postions
        furniturePos = startPos;
        // camPos = cam.transform.position;

        //first spawn
        SpawnNextFurniture(flatpackArray[flatpackIndex]);
        // GameObject spawnedFurniture = Instantiate(flatpackArray[flatpackIndex], furniturePos, Quaternion.identity);
        // spawnedFurniture.name = "furn_" + furnitureCount;
        // spawnedFurniture.layer = 9;
        // spawnedFurniture.GetComponent<Rigidbody>().useGravity = false;
        // // spawnedFurniture.GetComponent<Rigidbody>().isKinematic = true;
        // isFloating = true;

    }

    void Update()
    {
        //score update
        /*
        int p1BathroomUpdate = GameObject.Find("BathroomCollider").GetComponent<Score_Bathroom>().p1BathroomScore;
        int p2BathroomUpdate = GameObject.Find("BathroomCollider").GetComponent<Score_Bathroom>().p2BathroomScore;
        int p1StudioUpdate = GameObject.Find("StudioCollider").GetComponent<Score_Studio>().p1StudioScore;
        int p2StudioUpdate = GameObject.Find("StudioCollider").GetComponent<Score_Studio>().p2StudioScore;
        p1Score = p1BathroomUpdate + p1StudioUpdate;
        p2Score = p2BathroomUpdate + p2StudioUpdate;
        // print("p1: " + p1Score);
        // print("p2: " + p2Score);
        p1Text.text = "Kraftan: " + p1Score;
        p2Text.text = "Fiskarna: " + p2Score;
        */

        //camera update -- slowly rotates around counter-clockwise and raises relative to amount of furniture
        //numbered opposite to roomBounds
        /*
        if (camSpot == 0)
        {
            if (camPos.z >= camBounds[1].z)
            {
                camPos = camBounds[1];
                camSpot = 1;
            } else 
            {
                camPos += new Vector3 (0, 0, camSpeed);
            }
        }
        if (camSpot == 1)
        {
            if (camPos.x <= camBounds[2].x)
            {
                camPos = camBounds[2];
                camSpot = 2;
            } else 
            {
                camPos += new Vector3 (-camSpeed, 0, 0);
            }
        }
        if (camSpot == 2)
        {
            if (camPos.z <= camBounds[3].z)
            {
                camPos = camBounds[3];
                camSpot = 3;
            } else 
            {
                camPos += new Vector3 (0, 0, -camSpeed);
            }
        }
        if (camSpot == 3)
        {
            if (camPos.x >= camBounds[0].x)
            {
                camPos = camBounds[0];
                camSpot = 0;
            } else 
            {
                camPos += new Vector3 (camSpeed, 0, 0);
            }
        }
        //damn, should have done this for the room Pos too
        cam.transform.position = camPos + new Vector3(0, vertOffset / 8.0f, 0); //average height
        cam.transform.LookAt(camTarget);
        */

        // update height offset
        vertOffset = furnitureCount / 20.0f;

        //auto spawning now
        if (!isFloating) //so right after they slappa
        {
            SpawnNextFurniture(flatpackArray[flatpackIndex]);

        }
    }

    public void SpawnNextFurniture(GameObject furniture)
    {
        //now spawns last furniture type
        GameObject spawnedFurniture = Instantiate(furniture, furniturePos, Quaternion.identity);
        spawnedFurniture.name = "furn_" + furnitureCount;
        spawnedFurniture.layer = 9;
        spawnedFurniture.GetComponent<Rigidbody>().useGravity = false;
        isFloating = true;
    }

    public void DropFurniture(SocketIOEvent e) //SLAPPA
    {
        GameObject droppedFurniture = GameObject.Find("furn_" + furnitureCount);
        lastFurniture = droppedFurniture;
        droppedFurniture.layer = 0;
        droppedFurniture.GetComponent<Rigidbody>().useGravity = true;
        furnitureCount++;
        isFloating = false;
    }

    public void ChangeType(SocketIOEvent e) //BESKRIVNING
    {
        float player = e.data.GetField("player").f;
        Debug.Log(player);
        if (player == 1f)
        {
            if (p1Type < 3)
                p1Type++;
            else 
                p1Type = 0;
            flatpackIndex = p1Type;
        }
        if (player == 2f)
        {
            if (p2Type < 3)
                p2Type++;
            else 
                p2Type = 0;
            flatpackIndex = p2Type + furniturePerPlayer;
        }
        GameObject destroyedFurniture = GameObject.Find("furn_" + furnitureCount);
        Destroy(destroyedFurniture);
        SpawnNextFurniture(flatpackArray[flatpackIndex]);        
    }

    public void ChangeRotation(SocketIOEvent e) //VINKEL
    {
        float player = e.data.GetField("player").f;
        GameObject rotatedFurniture = GameObject.Find("furn_"+ furnitureCount);
        if (player == 1)
            rotatedFurniture.transform.Rotate(0,p1Rotate,0, Space.Self);
        if (player == 2)
            rotatedFurniture.transform.Rotate(0,p2Rotate,0, Space.Self);

        //should maybe try and keep rotation same after type or slappa?
    }

    public void ChangeScale(SocketIOEvent e) //SKALLA
    {
        float player = e.data.GetField("player").f;
        GameObject scaledFurniture = GameObject.Find("furn_" + furnitureCount);
        if (player == 1)
            scaledFurniture.transform.localScale *= scaleFactor;
        if (player == 2)
            scaledFurniture.transform.localScale /= scaleFactor;
    }

    public void MoveFurniture(SocketIOEvent e) //PLATS
    {
        //to add later, should maybe keep it to one axis/one button, so to change direction you have to go to the bounds of either side?
    }

    public void TestOpen(SocketIOEvent e)
	{
		Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
	}
    public void TestError(SocketIOEvent e)
	{
		Debug.Log("[SocketIO] Error received: " + e.name + " " + e.data);
	}
	public void TestClose(SocketIOEvent e)
	{	
		Debug.Log("[SocketIO] Close received: " + e.name + " " + e.data);
	}
}
