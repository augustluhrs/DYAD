using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureShowroom : MonoBehaviour
{
    public GameObject[][] showroom = new GameObject[8][];
    public GameObject[][] tutorialShowroom = new GameObject[3][];


    public GameObject[] baccaro;
    public GameObject[] brimnes;
    public GameObject[] arkelstorp;
    public GameObject[] solsta;
    public GameObject[] tullsta;
    public GameObject[] likseleLevos;
    public GameObject[] kautsbi;
    public GameObject[] odwar;

    private void Start() 
    {
        //attempt at a nested array
        //learned about diff between jagged array [][] and 2d arrays [,]
        // showroom[0] = new GameObject[baccaro.Length];
        // showroom[1] = new GameObject[brimnes.Length];
        // showroom[2] = new GameObject[arkelstorp.Length];
        // showroom[3] = new GameObject[solsta.Length];
        // showroom[4] = new GameObject[tullsta.Length];
        // showroom[5] = new GameObject[likseleLevos.Length];
        // showroom[6] = new GameObject[kautsbi.Length];
        // showroom[7] = new GameObject[hemnes.Length];
        showroom[0] = baccaro;
        showroom[1] = brimnes;
        showroom[2] = arkelstorp;
        showroom[3] = solsta;
        showroom[4] = tullsta;
        showroom[5] = likseleLevos;
        showroom[6] = kautsbi;
        showroom[7] = odwar;

        tutorialShowroom[0] = tullsta;
        tutorialShowroom[1] = brimnes;
        tutorialShowroom[2] = kautsbi;
        
    }

}
