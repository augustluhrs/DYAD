using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
    namespace MidiJack {
    public class MainInterface : MonoBehaviour
    {
         //Furniture arrays -- set in inspector
        [SerializeField] GameObject[] ariesFurniture;
        [SerializeField] GameObject[] taurusFurniture;
        [SerializeField] GameObject[] geminiFurniture;
        [SerializeField] GameObject[] cancerFurniture;
        [SerializeField] GameObject[] leoFurniture;
        [SerializeField] GameObject[] virgoFurniture;
        [SerializeField] GameObject[] libraFurniture;
        [SerializeField] GameObject[] scorpioFurniture;
        [SerializeField] GameObject[] saggitariusFurniture;
        [SerializeField] GameObject[] capricornFurniture;
        [SerializeField] GameObject[] aquariusFurniture;
        [SerializeField] GameObject[] piscesFurniture;
        // [SerializeField] Material[] fireColors;
        // [SerializeField] Material[] airColors;
        // [SerializeField] Material[] earthColors;
        // [SerializeField] Material[] waterColors;

        // player types -- assigned in UI dropdown
        private string p1Sign = "aries";
        private string p2Sign = "taurus";


        // furniture type -- cycles through furniture array
        private int p1Type = 0;
        private int p2Type = 0;
        //furniture count
        private int p1Count = 0;
        private int p2Count = 0;

        // material/color -- cycles through colors array
        // private int p1Color = 0;
        // private Material[] p1Colors;
        // private int p2Color = 0;
        // private Material[] p2Colors;

        //rotation
        private float p1Rotate = 0.1f;
        private bool p1Floating = false;
        private float p2Rotate = 0.1f;
        private bool p2Floating = false;

        //locations -- 
        [SerializeField] Vector3[] studioRooms; //set in inspector
        [SerializeField] Vector3[] studioBounds; //set in inspector

        private int p1Room = 0;
        private float p1Speed = 0.001f;
        private Vector3 p1Pos;
        // private bool p1Dir = true;
        private int p2Room = 2; //confusing because starting in Pos 2, which is Room 1
        private float p2Speed = 0.001f;
        private Vector3 p2Pos;
        // private bool p2Dir = true;
        // private float p2Dir = 1.0f;




        //misc
        // private bool colorTest = false;
        private float verticalOffset_p1 = 0.0f; //to gradually increase height of spawning (and camera?)
        private float verticalOffset_p2 = 0.0f; //made separate one to allow for groundhogs

        //camera stuff
        [SerializeField] Transform camTarget;
        [SerializeField] Camera cam;
        private Vector3 camPos;
        private int camSpot = 0;
        [SerializeField] Vector3[] camBounds;
        private float camSpeed = 0.0005f;

        //score field trigger stuff
        // [SerializeField] Collider studioCollider;
        // [SerializeField] Collider bathroomCollider;
        public int p1Score = 0;
        public int p2Score = 0;
        [SerializeField] Text p1Text;
        [SerializeField] Text p2Text;

        // [SerializeField] GameObject p2Text;

        // [SerializeField] GameObject studioScript;
        // [SerializeField] GameObject bathroomScript;




        void Start()
        {
            //starting postions
            p1Pos = studioRooms[0];
            p2Pos = studioRooms[1];
            camPos = cam.transform.position;
            //assign color palettes
            // if (p1Sign == "aries")
            // {
            //     p1Colors = fireColors;
            // }

            // if (p2Sign == "taurus")
            // {
            //     p2Colors = earthColors;
            // }
        }

        
        void Update()
        {
            //score update
            // bathroomScript.p1BathroomScore + Score
            int p1BathroomUpdate = GameObject.Find("BathroomCollider").GetComponent<Score_Bathroom>().p1BathroomScore;
            int p2BathroomUpdate = GameObject.Find("BathroomCollider").GetComponent<Score_Bathroom>().p2BathroomScore;
            int p1StudioUpdate = GameObject.Find("StudioCollider").GetComponent<Score_Studio>().p1StudioScore;
            int p2StudioUpdate = GameObject.Find("StudioCollider").GetComponent<Score_Studio>().p2StudioScore;
            p1Score = p1BathroomUpdate + p1StudioUpdate;
            p2Score = p2BathroomUpdate + p2StudioUpdate;
            print("p1: " + p1Score);
            print("p2: " + p2Score);
            p1Text.text = "Kraftan: " + p1Score;
            p2Text.text = "Fiskarna: " + p2Score;


            // update height offset
            verticalOffset_p1 = p1Count / 20.0f;
            verticalOffset_p2 = p2Count / 20.0f;


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
            cam.transform.position = camPos + new Vector3(0, (verticalOffset_p1 + verticalOffset_p2) / 8.0f, 0); //average height
            cam.transform.LookAt(camTarget);


            //player 1 MIDI button input
            if (MidiMaster.GetKeyDown(20) || Input.GetKeyDown("q")) //left (slappa/drop) -- when pressed down (held)
            {
                // GameObject drop1 = Instantiate(ariesFurniture[p1Type], studioRooms[p1Room] + new Vector3(0, verticalOffset_p1, 0), Quaternion.identity);
                GameObject drop1 = Instantiate(ariesFurniture[p1Type], p1Pos, Quaternion.identity);
                drop1.tag = "player1";
                drop1.name = "p1_" + p1Count;
                drop1.GetComponent<Rigidbody>().useGravity = false;
                p1Floating = true;
            }
            if (MidiMaster.GetKeyUp(20)|| Input.GetKeyUp("q"))
            {
                GameObject drop1 = GameObject.Find("p1_" + p1Count);
                drop1.GetComponent<Rigidbody>().useGravity = true;
                p1Count++;
                p1Floating = false;
            }

            if (MidiMaster.GetKeyDown(21) || Input.GetKeyDown("w")) //mid left (beskrivning/type)
            {
                if (p1Type < 3)
                {
                    p1Type++;
                } else 
                {
                    p1Type = 0;
                }
            }
            if (MidiMaster.GetKey(22) > 0 || Input.GetKey("e")) //mid right (vinkel/angle) -- was (Farg/color) -- GetKey b/c needs to be held down
            {
                if (p1Floating)
                {
                    GameObject drop1 = GameObject.Find("p1_"+ p1Count);
                    drop1.transform.Rotate(0,p1Rotate,0, Space.Self);
                    p1Rotate += (p1Rotate/50.0f);
                }
                // print(MidiMaster.GetKey(22));
                // colorTest = !colorTest;
                // if (colorTest)
                // {
                //    Renderer furnitureShader = ariesFurniture[p1Type].GetComponent<Renderer>();
                //    furnitureShader.material.color = Color.red;

                //    ariesFurniture[p1Type].GetComponent<Renderer>();
                // } else 
                // {
                //     Renderer furnitureShader = ariesFurniture[p1Type].GetComponent<Renderer>();
                //     furnitureShader.sharedMaterial.color = Color.yellow;
                // }
            }
            if (MidiMaster.GetKeyUp(22) || Input.GetKeyUp("e"))
            {
                p1Rotate = 0.1f;
            }
            if (MidiMaster.GetKey(23) > 0 || Input.GetKey("r")) //right (plats/room)
            {
                // print(p1Speed);
                //move along the bounds of the level
                if (p1Floating)
                {
                    p1Speed += (p1Speed / 200.0f);
                    // p1Speed *= p1Dir;
                    /*
                    if (!p1Dir) //backwards
                    {
                        p1Speed -= (p1Speed / 200.0f);
                    } else //forwards
                    {
                        p1Speed += (p1Speed / 200.0f);
                    }
                    */
                    GameObject drop1 = GameObject.Find("p1_"+ p1Count);
                    if(p1Room == 0)
                    {
                        if (p1Pos.x <= studioBounds[1].x)
                        {
                            p1Pos = studioBounds[1] + new Vector3(0, verticalOffset_p1, 0);
                            p1Room = 1;
                        } else 
                        {
                            p1Pos += new Vector3(-p1Speed, 0, 0);
                            drop1.transform.position = p1Pos;
                        }
                    } else if (p1Room == 1)
                    {
                        if (p1Pos.z >= studioBounds[2].z)
                        {
                            p1Pos = studioBounds[2] + new Vector3(0, verticalOffset_p1, 0);
                            p1Room = 2;
                        } else 
                        {
                            p1Pos += new Vector3(0, 0, p1Speed);
                            drop1.transform.position = p1Pos;
                        }
                    } else if (p1Room == 2)
                    {
                        if (p1Pos.x >= studioBounds[3].x)
                        {
                            p1Pos = studioBounds[3] + new Vector3(0, verticalOffset_p1, 0);
                            p1Room = 3;
                        } else 
                        {
                            p1Pos += new Vector3(p1Speed, 0, 0);
                            drop1.transform.position = p1Pos;
                        }
                    } else if (p1Room == 3)
                    {
                        if (p1Pos.z <= studioBounds[0].z)
                        {
                            p1Pos = studioBounds[0] + new Vector3(0, verticalOffset_p1, 0);
                            p1Room = 0;
                        } else 
                        {
                            p1Pos += new Vector3(0, 0, -p1Speed);
                            drop1.transform.position = p1Pos;
                        }
                    }
                }
                
                
                //just teleporting
                // if (p1Room < studioRooms.Length - 1)
                // {
                //     p1Room++;
                // } else 
                // {
                //     p1Room = 0;
                // }
            }
            if (MidiMaster.GetKeyUp(23) || Input.GetKeyUp("r"))
            {
                p1Speed = 0.001f; //resets speed
                /*
                p1Dir = !p1Dir;
                if (p1Dir) //forwrards
                {
                    p1Speed = 0.001f;
                } else //backwards
                {
                    p1Speed = -0.001f;
                }
                */
                // p1Dir *= -1.0f; //flips direciton
                // print(p1Dir);
            }
            //player 2 MIDI button input (reversed)
            if (MidiMaster.GetKeyDown(35) || Input.GetKeyDown("o")) //left (slappa/drop) 
            {
                // GameObject drop2 = Instantiate(taurusFurniture[p2Type], p2Pos + new Vector3(0, verticalOffset_p2, 0), Quaternion.identity);
                GameObject drop2 = Instantiate(taurusFurniture[p2Type], p2Pos, Quaternion.identity);

                drop2.tag = "player2";
                drop2.name = "p2_" + p2Count;
                drop2.GetComponent<Rigidbody>().useGravity = false;
                p2Floating = true;
            }
            if (MidiMaster.GetKeyUp(35) || Input.GetKeyUp("o"))
            {
                GameObject drop2 = GameObject.Find("p2_" + p2Count);
                drop2.GetComponent<Rigidbody>().useGravity = true;
                p2Count++;
                p2Floating = false;
            }

            if (MidiMaster.GetKeyDown(34) || Input.GetKeyDown("p")) //mid left (beskrivning/type)
            {
                if (p2Type < 3)
                {
                    p2Type++;
                } else 
                {
                    p2Type = 0;
                }
            }
            if (MidiMaster.GetKey(33) > 0 || Input.GetKey("[")) //mid right (vinkel/angle) -- was (Farg/color) -- GetKey b/c needs to be held down
            {
                if (p2Floating)
                {
                    GameObject drop2 = GameObject.Find("p2_"+ p2Count);
                    drop2.transform.Rotate(0,p2Rotate,0, Space.Self);
                    p2Rotate += (p2Rotate/50.0f);
                }
            }
            if (MidiMaster.GetKeyUp(33) || Input.GetKeyUp("["))
            {
                p2Rotate = 0.1f;
            }
            if (MidiMaster.GetKey(32) > 0 || Input.GetKey("]")) //right (plats/room)
            {
                //move along the bounds of the level
                if (p2Floating)
                {
                    p2Speed += (p2Speed / 200.0f);
                    GameObject drop2 = GameObject.Find("p2_"+ p2Count);
                    if(p2Room == 0)
                    {
                        if (p2Pos.x <= studioBounds[1].x)
                        {
                            p2Pos = studioBounds[1] + new Vector3 (0, verticalOffset_p2, 0);
                            p2Room = 1;
                        } else 
                        {
                            p2Pos += new Vector3(-p2Speed, 0, 0);
                            drop2.transform.position = p2Pos;
                        }
                    } else if (p2Room == 1)
                    {
                        if (p2Pos.z >= studioBounds[2].z)
                        {
                            p2Pos = studioBounds[2] + new Vector3 (0, verticalOffset_p2, 0);
                            p2Room = 2;
                        } else 
                        {
                            p2Pos += new Vector3(0, 0, p2Speed);
                            drop2.transform.position = p2Pos;
                        }
                    } else if (p2Room == 2)
                    {
                        if (p2Pos.x >= studioBounds[3].x)
                        {
                            p2Pos = studioBounds[3] + new Vector3 (0, verticalOffset_p2, 0);
                            p2Room = 3;
                        } else 
                        {
                            p2Pos += new Vector3(p2Speed, 0, 0);
                            drop2.transform.position = p2Pos;
                        }
                    } else if (p2Room == 3)
                    {
                        if (p2Pos.z <= studioBounds[0].z)
                        {
                            p2Pos = studioBounds[0] + new Vector3 (0, verticalOffset_p2, 0);
                            p2Room = 0;
                        } else 
                        {
                            p2Pos += new Vector3(0, 0, -p2Speed);
                            drop2.transform.position = p2Pos;
                        }
                    }
                }
            }
            if (MidiMaster.GetKeyUp(32) || Input.GetKeyDown("]"))
            {
                p2Speed = 0.001f; //resets speed
                /*
                p1Dir = !p1Dir;
                if (p1Dir) //forwrards
                {
                    p1Speed = 0.001f;
                } else //backwards
                {
                    p1Speed = -0.001f;
                }
                */
                // p1Dir *= -1.0f; //flips direciton
                // print(p1Dir);
            }
            /*
            if (MidiMaster.GetKeyDown(32)) //right (plats/room)
            {
                if (p2Room < studioRooms.Length - 1)
                {
                    p2Room++;
                } else 
                {
                    p2Room = 0;
                }
            }
            */
        }
    }
}
