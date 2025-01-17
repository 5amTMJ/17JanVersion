using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssessmentManager : MonoBehaviour
{
    public static AssessmentManager Instance { get; private set; }
    public bool IsAssessmentActive { get; private set; } = false;
    public Canvas assessmentCanvas; 
    public BoxCollider boundary;
    public Timer timer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartAssessment()
    {
        IsAssessmentActive = true;
        Debug.Log("Starting assessment");
        assessmentCanvas.gameObject.SetActive(false);
        boundary.enabled = false;

        timer.StartTimer();
        
        Debug.Log("Canva active: " + assessmentCanvas.gameObject.activeSelf);
        Debug.Log("Boundary enabled: " + boundary.enabled);
    }

    public void CompleteAssessment()
    {
        IsAssessmentActive = false;
        Debug.Log("Completing assessment");
        assessmentCanvas.gameObject.SetActive(true);
        boundary.enabled = true;

        timer.ResetTimer();

        Debug.Log("Canvas active: " + assessmentCanvas.gameObject.activeSelf);
        Debug.Log("Boundary enabled: " + boundary.enabled);
    }
}
