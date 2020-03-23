using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MidiJack {
    public class IkeaTest : MonoBehaviour
    {
        //player1 furniture
        [SerializeField] GameObject p1f1;
        [SerializeField] GameObject p1f2;
        [SerializeField] GameObject p1f3;
        [SerializeField] GameObject p1f4;
        //player 2 furniture
        [SerializeField] GameObject p2f1;
        [SerializeField] GameObject p2f2;
        [SerializeField] GameObject p2f3;
        [SerializeField] GameObject p2f4;
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (MidiMaster.GetKeyDown(20))
            {
                // GameObject drop1 = 
                GameObject drop1 = Instantiate(p1f1, new Vector3(1, 10, 0), Quaternion.identity);
            }
            if (MidiMaster.GetKeyDown(21))
            {
                print("65 pressed");
                Instantiate(p1f2, new Vector3(-1, 10, 0), Quaternion.identity);
            }
            if (MidiMaster.GetKeyDown(22))
            {
                // print("60 pressed");
                Instantiate(p1f3, new Vector3(0, 10, 1), Quaternion.identity);
            }
            if (MidiMaster.GetKeyDown(23))
            {
                Instantiate(p1f4, new Vector3(0, 10, -1), Quaternion.identity);
            }
            //p2
            if (MidiMaster.GetKeyDown(32))
            {
                Instantiate(p2f1, new Vector3(1, 10, 0), Quaternion.identity);
            }
            if (MidiMaster.GetKeyDown(33))
            {
                Instantiate(p2f2, new Vector3(-1, 10, 0), Quaternion.identity);
            }
            if (MidiMaster.GetKeyDown(34))
            {
                Instantiate(p2f3, new Vector3(0, 10, 1), Quaternion.identity);
            }
            if (MidiMaster.GetKeyDown(35))
            {
                Instantiate(p2f4, new Vector3(0, 10, -1), Quaternion.identity);
            }
        }
    }
}