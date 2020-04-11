using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;


public class MainMenuManager : MonoBehaviour
{
    // [Header("Button UI")]
    
    [Header("Spinning Furniture Stuff")]
    [SerializeField] GameObject[] spinFurniture;
    [SerializeField] GameObject spawnPoint;
    private GameObject spinner;
    [Range(1.001f, 1.2f)][SerializeField] float spinFactor = 1.02f;
    // private float spinDirection = 1f;
    private bool isSpinningForward = true;
    private float spinSpeed = 1f;
    // private float spinDuration = 0f; //need to have a value to compare against since rotation doesnt work how i thought
    [SerializeField] float maxRotation = 200f;
    private int furnIndex = 0;
    
    void Start()
    {
        spinner  = Instantiate(spinFurniture[furnIndex], spawnPoint.transform.position, Quaternion.identity);
        spinner.transform.parent = spawnPoint.transform;
        spinner.GetComponent<Rigidbody>().useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        // float currentRotation = spinner.transform.eulerAngles.y;

        if (spinSpeed >= maxRotation || spinSpeed <= -maxRotation)
        { //if spinner has spun out, destroy old, instantiate next furn in array
            Destroy(spinner);
            if (furnIndex < spinFurniture.Length - 1)
                furnIndex++;
            else
                furnIndex = 0;
            //should make new GameObject if destroyed? or fine?
            spinner = Instantiate(spinFurniture[furnIndex], spawnPoint.transform.position, Quaternion.identity);
            spinner.transform.parent = spawnPoint.transform;
            //have to adjust for rotated prefabs
            if(spinner.TryGetComponent<Rigidbody>(out Rigidbody rb))
                rb.useGravity = false;
            else
                spinner.GetComponentInChildren<Rigidbody>().useGravity = false;
            // spinner.GetComponent<Rigidbody>().useGravity = false; 

            //flip the spin
            // spinDirection *= -1f;
            spinSpeed /= -1.1f; //gotta make it a little smaller or it'll keep triggering destroy
            // isSpinningForward = !isSpinningForward;
        }
        else
        { //if spinner hasn't spun out yet
            // if (isSpinningForward && spinSpeed <= 0 || !isSpinningForward && spinSpeed >= 0) 
            // Debug.Log(spinSpeed);
            if (spinSpeed < -1f) //0 leads to Zeno's Paradox...
            { //decrease speed
                spinSpeed /= spinFactor;
            }
            else 
            {
                if (spinSpeed < 0) //gotta flip the zero gap
                    spinSpeed *= -1f;
                spinSpeed *= spinFactor;
            }

            spinner.transform.eulerAngles += new Vector3(0f, spinSpeed, 0f);
            //should I be lerping somehow?

        }
    }

    public void OnPlayGame()
    {
        // UnityEngine.SceneManagement.SceneLoader.Instance.LoadScene("Scene_PlayerProfile");
        SceneLoader.Instance.LoadScene("Scene_PlayerProfile");
        // SceneManager.LoadScene("Scene_PlayerProfile");
    }
}
