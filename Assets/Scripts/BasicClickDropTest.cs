using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicClickDropTest : MonoBehaviour
{
    [SerializeField] GameObject furniture;

    void Start()
    {
    }

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.fingerId == 0) //first press
            {
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    Instantiate(furniture, transform.position, Quaternion.identity);
                }
            }
        }
    }

    public void Drop()
    {
        Instantiate(furniture, transform.position, Quaternion.identity);
    }
}
