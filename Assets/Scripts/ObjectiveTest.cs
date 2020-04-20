using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTest : MonoBehaviour
{
    [SerializeField] GameObject floorPlan;
    [SerializeField] GameObject ARCam;
    ColliderManager m_ColliderManager;

    //evaluation variables
    [Header("Evaluation Variables")]
    [Range(1f, 50f)] [SerializeField] float evaluationDistance = 10f;
    private float evaluationTimer = 0f;
    [Range(1f, 1000f)] [SerializeField] float timerFadeout = 500f;
    private float timerMax;
    private bool showEvaluation = false;
    private Color evaluationColor;
    private float personalSatisfaction = 0; //on scale of 100? no 1 so can use in color
    public List<int[]> objectives = new List<int[]>();
    private Dictionary<int, string> furnitureTypes = new Dictionary<int, string>();

    // enum FurnitureTypes 
    // {
    //     AR_Tullsta
    // }

    private void Awake() 
    {
        m_ColliderManager = gameObject.GetComponent<ColliderManager>();

        furnitureTypes.Add(0, "odwar");
        furnitureTypes.Add(1, "tullsta");
        furnitureTypes.Add(2, "brimnes");
        furnitureTypes.Add(3, "ektorp");
    }

    void Start()
    {
        //assign personal objectives
        // AssignObjectives();
        AssignObjectivesBasic();
    }

    void Update()
    {
        evaluationTimer += Time.deltaTime;

        //check for distance
        if (Vector3.Distance(ARCam.transform.position, floorPlan.transform.position) >= evaluationDistance)
            OnStepBackEvaluate();

        //just to test objective minus stepping back
        // OnStepBackEvaluate();


        //if evaluating, show and fade
        if (showEvaluation && evaluationTimer <= timerMax)
        {
            //show and fade
            evaluationColor.a = (timerMax - evaluationTimer) / timerMax;
            floorPlan.GetComponentInChildren<MeshRenderer>().material.SetColor("_Color", evaluationColor); //issues with text?
        }
        else if (showEvaluation && evaluationTimer >= timerMax) //done
            showEvaluation = false; //guess i don't need this if it fades enough? could use transparency
        
    }

    private void OnStepBackEvaluate() //when they step away from the floor plan
    {
        //reset fade
        timerMax = evaluationTimer + timerFadeout;
        showEvaluation = true;

        //change the color depending on evaluation
        personalSatisfaction = CheckSatisfaction();
        // if (personalSatisfaction >= 80f) //green
        //     evaluationColor = new Color(0f, 1f, 0f);
        // else if (personalSatisfaction >= 40f) //yellow
        evaluationColor = new Color (1f - personalSatisfaction, personalSatisfaction, 0f); //if 0, red, if 1, green, if half, brownish
    }

    private float CheckSatisfaction() //compare to personal goals
    {
        //check and give score for each category
        float scoreTotal = 0f; //starts ambivalent? how should compare to arg meter?
        foreach(int[] goal in objectives) //switching to goal b/c obj confusing with object
        {
            if (goal[0] == 0) //furniture comparison
            {
                float furnCount = 0;
                float targetCount = 0;
                // float thisGoalScore = 0;
                foreach(GameObject furn in m_ColliderManager.floorPlanPile)
                {
                    Debug.Log("tag = " + furn.tag + " furn type: " + furnitureTypes[goal[1]]);
                    if(furn.tag == furnitureTypes[goal[1]])
                    {
                        Debug.Log("my furn match!");
                        furnCount++;
                    }
                    if(furn.tag == furnitureTypes[goal[3]])
                    {
                        Debug.Log("target match");
                        targetCount++;
                    }
                }
                if (goal[2] == 0) //equals
                {
                    if (furnCount == targetCount)
                        scoreTotal += 1f;
                        // thisGoalScore = 1f;
                    // else
                        // thisGoalScore = 0f;
                }
                if (goal[2] == 1) //less than
                {
                    if (furnCount < targetCount)
                        scoreTotal += 1f;
                }
                if (goal[2] == 2) //greater than
                {
                    if (furnCount > targetCount)
                        scoreTotal += 1f;
                }
            }
        }
        scoreTotal /= objectives.Count;
        return scoreTotal;
    }

    private void AssignObjectives()
    {
        //Random number of objectives
        int numObjectives = Mathf.FloorToInt(Random.Range(1f, 3.99f)); //between 1 and 3 objc
        Debug.Log("num of objectives = " + numObjectives);
        for (int i = 0; i < numObjectives; i++)
        {
            int[] newObj = new int[4];
            newObj[0] = Mathf.FloorToInt(Random.Range(0f, 2.99f)); //objective type (out of 3)
            newObj[1] = Mathf.FloorToInt(Random.Range(0f, 3.99f)); //furniture type (out of 4)
            newObj[2] = Mathf.FloorToInt(Random.Range(0f, 2.99f)); //relative (out of 3)
            // newObj[3] = Mathf.FloorToInt(Random.Range(0f, 3.99f)); //target (out of 4)

            //TODO: clean up weird cases -- use enums and better assignment?
            //assign 4th element depending on objective type
            if (newObj[0] == 0) //generate a non-matching furn type
            {
                newObj[3] = Mathf.FloorToInt(Random.Range(0f, 3.99f));
                while(newObj[3] == newObj[1])
                    newObj[3] = Mathf.FloorToInt(Random.Range(0f, 3.99f));
            }
            if (newObj[0] == 1) // generate a random quantity to check against
                newObj[3] = Mathf.FloorToInt(Random.Range(1f, 30f));
            if (newObj[0] == 2) // generate a non-matching location
            {
                // newObj[3] = Mathf.FloorToInt(Random.Range(0f, 2.99f); //nvm, need to keep it to just 2 for now, too many weird cases if whole thing
                if(newObj[2] == 2)
                    newObj[2] = Mathf.FloorToInt(Random.Range(0f, 1.99f));
                if(newObj[2] == 0)
                    newObj[3] = 1;
                else
                    newObj[3] = 0;
            }

            objectives.Add(newObj);
        }
    }

    private void AssignObjectivesBasic()
    {
        //should be if there are more tullstas than brimnes -- lol randomly picked 0123
        int[] newObj = new int[4];
        newObj[0] = 0; //furniture comparison
        newObj[1] = 1; //tullsta
        newObj[2] = 2; //greater than
        newObj[3] = 3; //brimnes

        objectives.Add(newObj);
        
    }
}
