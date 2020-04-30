using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManualManager : MonoBehaviour
{
    TrioJoystickManager m_TrioJoystickManager;
    private bool firstPlayer;

    [SerializeField] GameObject trioSelector;
    [SerializeField] GameObject manualSectionPrefab;
    [SerializeField] Transform content;

    private List<GameObject> manual = new List<GameObject>();
    private List<string> sections = new List<string>();

    void Start()
    {
        m_TrioJoystickManager = trioSelector.GetComponent<TrioJoystickManager>();
        firstPlayer = m_TrioJoystickManager.firstPlayer;

        if (firstPlayer)
        {
            sections.Add("You want at least 4 ODWARS");
            sections.Add("You want one BRIMNES in the BATHROOM");
            sections.Add("You want at least 3 pieces of BLUE furniture");
            sections.Add("You want more TULLSTAS than LISKELES");
            sections.Add("You want more furniture in the BATHROOM than the MAIN ROOM");
            sections.Add("You want the same amount of BLACK and WHITE furniture");
        }
        else
        {
            sections.Add("You want at least 4 TULLSTAS");
            sections.Add("You want two BRIMNES in the MAIN ROOM");
            sections.Add("You want more ODWAR in the MAIN ROOM than the BATHROOM");
            sections.Add("You want more WHITE furniture than BLUE furniture");
            sections.Add("You want two different colored ODWARS perfectly stacked");
            sections.Add("You don't want any BRIMNES in the BATHROOM");
        }
    }

    public void AddNewManualSection()
    {
        GameObject newSection = Instantiate(manualSectionPrefab, content);
        string unlockedSection = UnlockSection();
        newSection.GetComponent<TextMeshProUGUI>().text = unlockedSection;
        manual.Add(newSection); 
    }

    public string UnlockSection()
    {
        string unlockedSection = "";
        // int insightIndex = Mathf.FloorToInt(Random.Range(0f, sections.Count)
        if(sections.Count > 0) //just in case
        {
            int insightIndex = Random.Range(0, sections.Count); //returns int? wowwww i've been doing floortoint...
            unlockedSection = sections[insightIndex];
            sections.RemoveAt(insightIndex);
        }
        Debug.Log(unlockedSection);
        return unlockedSection;
    }
}
