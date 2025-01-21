using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndAssessment : MonoBehaviour
{
    public Timer timer;
    public Button exitAssessmentButton; // Assign in Inspector
    public string summarySceneName = "SummaryScene";

    private void Start()
    {
        if (exitAssessmentButton == null)
        {
            Debug.LogError("Exit Assessment Button is not assigned in the Inspector!");
            return;
        }

        exitAssessmentButton.onClick.AddListener(ExitAssessment);
    }

    public void ExitAssessment()
    {

        AntennaError();
        CableError();
        RightCornerboardError();
        LeftCornerboardError();
        LeftLadderError();
        RightLadderError();
        GroundingRodError();
        FireExtinguisherError();
        FODError();

        timer.StopTimer();

        // Load the summary scene
        SceneManager.LoadScene(summarySceneName);
    }

    private void AntennaError()
    {
        if (PersistentDataStore.errorStatuses.TryGetValue("Antenna Error", out var antennaErrorData))
        {
            Debug.Log($"Antenna Error Status: {antennaErrorData.status}");
        }
        else
        {
            Debug.LogError("Antenna Error key not found in PersistentDataStore!");
        }
    }

    private void CableError()
    {
        if (PersistentDataStore.errorStatuses.TryGetValue("Cable Error", out var cableErrorData))
        {
            Debug.Log($"Cable Error Status: {cableErrorData.status}");
        }
        else
        {
            Debug.LogError("Cable Error key not found in PersistentDataStore!");
        }
    }

    private void RightCornerboardError()
    {
        if (PersistentDataStore.errorStatuses.TryGetValue("Right Cornerboard Error", out var rightCornerboardErrorData))
        {
            Debug.Log($"Right Cornerboard Error Status: {rightCornerboardErrorData.status}");
        }
        else
        {
            Debug.LogError("Right Cornerboard Error key not found in PersistentDataStore!");
        }
    }

    private void LeftCornerboardError()
    {
        if (PersistentDataStore.errorStatuses.TryGetValue("Left Cornerboard Error", out var leftCornerboardErrorData))
        {
            Debug.Log($"Left Cornerboard Error Status: {leftCornerboardErrorData.status}");
        }
        else
        {
            Debug.LogError("Left Cornerboard Error key not found in PersistentDataStore!");
        }
    }

    private void LeftLadderError()
    {
        if (PersistentDataStore.errorStatuses.TryGetValue("Left Ladder Error", out var leftLadderErrorData))
        {
            Debug.Log($"Left Ladder Error Status: {leftLadderErrorData.status}");
        }
        else
        {
            Debug.LogError("Left Ladder Error key not found in PersistentDataStore!");
        }
    }
    private void RightLadderError()
    {
        if (PersistentDataStore.errorStatuses.TryGetValue("Right Ladder Error", out var rightLadderErrorData))
        {
            Debug.Log($"Right Ladder Error Status: {rightLadderErrorData.status}");
        }
        else
        {
            Debug.LogError("Right Ladder Error key not found in PersistentDataStore!");
        }
    }
    private void GroundingRodError()
    {
        if (PersistentDataStore.errorStatuses.TryGetValue("Grounding Rod Error", out var groundingRodErrorData))
        {
            Debug.Log($"Grounding Rod Error Status: {groundingRodErrorData.status}");
        }
        else
        {
            Debug.LogError("Grounding Rod Error key not found in PersistentDataStore!");
        }
    }

    private void FireExtinguisherError()
    {
        if (PersistentDataStore.errorStatuses.TryGetValue("Fire Extinguisher Error", out var fireExtinguisherErrorData))
        {
            Debug.Log($"Fire Extinguisher Error Status: {fireExtinguisherErrorData.status}");
        }
        else
        {
            Debug.LogError("Fire Extinguisher Error key not found in PersistentDataStore!");
        }
    }

    private void FODError()
    {
        if (PersistentDataStore.errorStatuses.TryGetValue("FOD Error", out var fodErrorData))
        {
            Debug.Log($"FOD Error Status: {fodErrorData.status}");
        }
        else
        {
            Debug.LogError("FOD Error key not found in PersistentDataStore!");
        }
    }
}

