using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    [Range(0.01f, 1f)][SerializeField] float rotateSpeed = .1f;
    
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        gameObject.transform.eulerAngles += new Vector3(0f, -rotateSpeed, 0f);
    }
}
