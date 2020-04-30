using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManualManager : MonoBehaviour
{
    TrioJoystickManager m_TrioJoystickManager;
    [SerializeField] GameObject trioSelector;
    [SerializeField] GameObject manualSectionPrefab;

    private bool firstPlayer;
    void Start()
    {
        m_TrioJoystickManager = trioSelector.GetComponent<TrioJoystickManager>();
        firstPlayer = m_TrioJoystickManager.firstPlayer;
    }

    void Update()
    {
        
    }

    public void NewSectionUnlocked()
    {
        
    }
}
