using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
    namespace MidiJack {
    public class ShowInterface : MonoBehaviour
    {
         //Furniture arrays -- set in inspector
        [SerializeField] GameObject[] ariesFurniture;
        [SerializeField] GameObject[] taurusFurniture;
        // [SerializeField] GameObject[] geminiFurniture;
        // [SerializeField] GameObject[] cancerFurniture;
        // [SerializeField] GameObject[] leoFurniture;
        // [SerializeField] GameObject[] virgoFurniture;
        // [SerializeField] GameObject[] libraFurniture;
        // [SerializeField] GameObject[] scorpioFurniture;
        // [SerializeField] GameObject[] saggitariusFurniture;
        // [SerializeField] GameObject[] capricornFurniture;
        // [SerializeField] GameObject[] aquariusFurniture;
        // [SerializeField] GameObject[] piscesFurniture;
        // [SerializeField] Material[] fireColors;
        // [SerializeField] Material[] airColors;
        // [SerializeField] Material[] earthColors;
        // [SerializeField] Material[] waterColors;

        // player types -- assigned in UI dropdown
        // private string p1Sign = "aries";
        // private string p2Sign = "taurus";


        // furniture type -- cycles through furniture array
        [SerializeField] GameObject defaultFurniture; //meatball?
        private int p1Type = 0;
        private int p2Type = 0;
        //furniture count
        private int furnCount = 0;
        private bool isFloating = false; //if spawned
        private float vertOffset = 0; //variable height as number increases
        private GameObject lastFurn;
        //rotation
        private float p1Rotate = 90.0f;
        private float p2Rotate = -90.0f;

        //locations -- 
        [SerializeField] Vector3 startPos; //set in inspector
        private Vector3 furnPos;
        [SerializeField] float moveSpeed = .01f; 

        [SerializeField] float scaleFactor = 1.1f;

        //misc
        
        //camera stuff
        [SerializeField] Transform camTarget;
        [SerializeField] Camera cam;
        private Vector3 camPos;
        private int camSpot = 0;
        [SerializeField] Vector3[] camBounds;
        private float camSpeed = 0.0005f;

        //score field trigger stuff
        public int p1Score = 0;
        public int p2Score = 0;
        [SerializeField] Text p1Text;
        [SerializeField] Text p2Text;




        void Start()
        {
            //starting postions
            furnPos = startPos;
            camPos = cam.transform.position;

            //first spawn
            GameObject furnFloater = Instantiate(defaultFurniture, furnPos, Quaternion.identity);
            furnFloater.name = "furn_" + furnCount;
            furnFloater.layer = 9;
            furnFloater.GetComponent<Rigidbody>().useGravity = false;
            // furnFloater.GetComponent<Rigidbody>().isKinematic = true;
            isFloating = true;
        }

        
        void Update()
        {
            //score update
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

            // update height offset
            vertOffset = furnCount / 20.0f;

            //camera update -- slowly rotates around counter-clockwise and raises relative to amount of furniture
            //numbered opposite to roomBounds
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

            //auto spawning now
            if (!isFloating) //so right after they slappa
            {
                //now spawns last furniture type
                GameObject furnFloater = Instantiate(lastFurn, furnPos, Quaternion.identity);
                furnFloater.name = "furn_" + furnCount;
                furnFloater.layer = 9;
                furnFloater.GetComponent<Rigidbody>().useGravity = false;
                // furnFloater.GetComponent<Rigidbody>().isKinematic = true;
                isFloating = true;
            }

            //player 1 MIDI button input
            if (MidiMaster.GetKeyDown(32) || Input.GetKeyDown("q")) //left (slappa/drop) -- when pressed down (held)
            {
                // GameObject drop1 = Instantiate(ariesFurniture[p1Type], studioRooms[p1Room] + new Vector3(0, verticalOffset_p1, 0), Quaternion.identity);
                // GameObject drop1 = Instantiate(ariesFurniture[p1Type], p1Pos, Quaternion.identity);
                // drop1.tag = "player1";
                // drop1.name = "p1_" + p1Count;
                // drop1.GetComponent<Rigidbody>().useGravity = false;
                // p1Floating = true;
                GameObject droppedFurn = GameObject.Find("furn_" + furnCount);
                lastFurn = droppedFurn;
                //why rotation/scale not staying?
                print(lastFurn.transform.rotation);
                droppedFurn.layer = 0;
                droppedFurn.GetComponent<Rigidbody>().useGravity = true;
                furnCount++;
                isFloating = false;
            }
            if (MidiMaster.GetKeyDown(28) || Input.GetKeyDown("w")) //mid left (beskrivning/type)
            {
                if (p1Type < 3)
                {
                    p1Type++;
                } else 
                {
                    p1Type = 0;
                }
                GameObject destroyedFurn = GameObject.Find("furn_" + furnCount);
                Destroy(destroyedFurn);
                GameObject newFurn = Instantiate(ariesFurniture[p1Type], furnPos, Quaternion.identity);
                newFurn.name = ("furn_" + furnCount);
                newFurn.layer = 9;
                newFurn.GetComponent<Rigidbody>().useGravity = false;
            }
            if (MidiMaster.GetKeyDown(24)|| Input.GetKeyDown("e")) //mid right (vinkel/angle) -- was (Farg/color) -- GetKey b/c needs to be held down
            {
                GameObject rotatedFurn = GameObject.Find("furn_"+ furnCount);
                rotatedFurn.transform.Rotate(0,p1Rotate,0, Space.Self);
            }
            if (MidiMaster.GetKey(20) > 0 || Input.GetKey("r")) //right 
            {
                GameObject scaledFurn = GameObject.Find("furn_" + furnCount);
                scaledFurn.transform.localScale *= scaleFactor;
            }
            if (MidiMaster.GetKey(4) > 0 || Input.GetKey("t")) //up plats
            {
                //move along Z axis (up)
                GameObject movingFurn = GameObject.Find("furn_" + furnCount);
                movingFurn.transform.position += new Vector3(0, 0, moveSpeed);
                furnPos = movingFurn.transform.position;
            }
            if (MidiMaster.GetKey(7) > 0 || Input.GetKey("g")) //down plats
            {
                //move along Z axis (down)
                GameObject movingFurn = GameObject.Find("furn_" + furnCount);
                movingFurn.transform.position += new Vector3(0, 0, -moveSpeed);
                furnPos = movingFurn.transform.position;
            } 
            
            //player 2 MIDI button input (reversed)
            if (MidiMaster.GetKeyDown(23) || Input.GetKeyDown("o")) //left (slappa/drop) 
            {
                GameObject droppedFurn = GameObject.Find("furn_" + furnCount);
                lastFurn = droppedFurn;
                droppedFurn.layer = 0;
                droppedFurn.GetComponent<Rigidbody>().useGravity = true;
                furnCount++;
                isFloating = false;
            }
            if (MidiMaster.GetKeyDown(27) || Input.GetKeyDown("p")) //mid left (beskrivning/type)
            {
                if (p2Type < 3)
                {
                    p2Type++;
                } else 
                {
                    p2Type = 0;
                }
                GameObject destroyedFurn = GameObject.Find("furn_" + furnCount);
                Destroy(destroyedFurn);
                GameObject newFurn = Instantiate(taurusFurniture[p2Type], furnPos, Quaternion.identity);
                newFurn.name = ("furn_" + furnCount);
                newFurn.layer = 9;
                newFurn.GetComponent<Rigidbody>().useGravity = false;
            }
            if (MidiMaster.GetKeyDown(31) || Input.GetKeyDown("[")) //mid right (vinkel/angle) -- was (Farg/color) -- GetKey b/c needs to be held down
            {
                GameObject rotatedFurn = GameObject.Find("furn_"+ furnCount);
                rotatedFurn.transform.Rotate(p2Rotate,0,0, Space.Self);
            }
            if (MidiMaster.GetKey(35) > 0 || Input.GetKey("]")) //right (plats/room)
            {
                GameObject scaledFurn = GameObject.Find("furn_" + furnCount);
                scaledFurn.transform.localScale /= scaleFactor;
            }
            if (MidiMaster.GetKey(5) > 0 || Input.GetKey("\\")) //up plats
            {
                //move along X axis (left)
                GameObject movingFurn = GameObject.Find("furn_" + furnCount);
                movingFurn.transform.position += new Vector3(-moveSpeed, 0, 0);
                furnPos = movingFurn.transform.position;
            }
            if (MidiMaster.GetKey(6) > 0 || Input.GetKey("return")) //down plats
            {
                //move along X axis (right)
                GameObject movingFurn = GameObject.Find("furn_" + furnCount);
                movingFurn.transform.position += new Vector3(moveSpeed, 0, 0);
                furnPos = movingFurn.transform.position;
            } 
        }
    }
}
