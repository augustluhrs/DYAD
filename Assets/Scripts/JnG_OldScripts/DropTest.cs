using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MidiJack {
public class DropTest : MonoBehaviour
{
    [SerializeField] GameObject obj1;
    [SerializeField] GameObject obj2;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            print("spacebar pressed");
            GameObject drop1 = Instantiate(obj1, new Vector3(0, 10, 0), Quaternion.identity);
        }
        if (Input.GetKeyDown("up"))
        {
            print("up pressed");
            GameObject drop2 = Instantiate(obj2, new Vector3(0, 10, 0), Quaternion.identity);
        }
        //midi test - no channel
        if (MidiMaster.GetKeyDown(64))
        {
            // print("60 pressed");
            GameObject drop1 = Instantiate(obj1, new Vector3(1, 10, 0), Quaternion.identity);
        }
        if (MidiMaster.GetKeyDown(65))
        {
            // print("61 pressed");
            GameObject drop2 = Instantiate(obj1, new Vector3(-1, 10, 0), Quaternion.identity);
        }
        if (MidiMaster.GetKeyDown(66))
        {
            // print("60 pressed");
            GameObject drop1 = Instantiate(obj1, new Vector3(0, 10, 1), Quaternion.identity);
        }
        if (MidiMaster.GetKeyDown(67))
        {
            // print("61 pressed");
            GameObject drop2 = Instantiate(obj1, new Vector3(0, 10, -1), Quaternion.identity);
        }
        //p2
        if (MidiMaster.GetKeyDown(52))
        {
            // print("60 pressed");
            GameObject drop1 = Instantiate(obj2, new Vector3(1, 10, 0), Quaternion.identity);
        }
        if (MidiMaster.GetKeyDown(53))
        {
            // print("61 pressed");
            GameObject drop2 = Instantiate(obj2, new Vector3(-1, 10, 0), Quaternion.identity);
        }
        if (MidiMaster.GetKeyDown(54))
        {
            // print("60 pressed");
            GameObject drop1 = Instantiate(obj2, new Vector3(0, 10, 1), Quaternion.identity);
        }
        if (MidiMaster.GetKeyDown(55))
        {
            // print("61 pressed");
            GameObject drop2 = Instantiate(obj2, new Vector3(0, 10, -1), Quaternion.identity);
        }
    }
}
}