using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SummarySceneHandler : MonoBehaviour
{
    public TextMeshProUGUI antennaErrorText; // Assign in Inspector
    public TextMeshProUGUI exposedCableErrorText;
    public TextMeshProUGUI rightCornerboardErrorText; // Assign in Inspector
    public TextMeshProUGUI leftCornerboardErrorText; // Assign in Inspector
    public TextMeshProUGUI leftLadderErrorText;
    public TextMeshProUGUI rightLadderErrorText;
    public TextMeshProUGUI groundingRodErrorText;
    public TextMeshProUGUI fireExtinguisherErrorText;
    public TextMeshProUGUI fodErrorText;
    public TextMeshProUGUI finalTimeText;
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        DisplayAntennaText();
        DisplayExposedCableText();
        DisplayRightCornerboardText();
        DisplayLeftCornerboardText();
        DisplayLeftLadderText();
        DisplayRightLadderText();
        DisplayGroundingRodText();
        DisplayFireExtinguisherText();
        DisplayFODsText();
        DisplayTime();

        // Once everything is displayed, call a function to save CSV
        SaveSummaryData();
    }

    private void DisplayErrorStatus(string errorKey, TextMeshProUGUI textMesh)
    {
        if (PersistentDataStore.errorStatuses.TryGetValue(errorKey, out var errorStatusData))
        {
            string message = $"{errorKey}: {errorStatusData.status}";
            Color color = GetStatusColor(errorStatusData.status);
            SetText(textMesh, message, color);
        }
        else
        {
            SetText(textMesh, $"{errorKey}: Status Unknown", Color.black);
        }
    }

    private Color GetStatusColor(ErrorStatus status)
    {
        switch (status)
        {
            case ErrorStatus.Corrected: return Color.green;
            case ErrorStatus.NotCorrected: return Color.red;
            case ErrorStatus.NotTested: return Color.gray;
            default: return Color.black;
        }
    }

    private void DisplayAntennaText()
    {
        DisplayErrorStatus("Antenna Error", antennaErrorText);
    }

    private void DisplayExposedCableText()
    {
        DisplayErrorStatus("Cable Error", exposedCableErrorText);
    }

    private void DisplayRightCornerboardText()
    {
        DisplayErrorStatus("Right Cornerboard Error", rightCornerboardErrorText);
    }

    private void DisplayLeftCornerboardText()
    {
        DisplayErrorStatus("Left Cornerboard Error", leftCornerboardErrorText);
    }

    private void DisplayLeftLadderText()
    {
        DisplayErrorStatus("Left Ladder Error", leftLadderErrorText);
    }

    private void DisplayRightLadderText()
    {
        DisplayErrorStatus("Right Ladder Error", rightLadderErrorText);
    }

    private void DisplayGroundingRodText()
    {
        DisplayErrorStatus("Grounding Rod Error", groundingRodErrorText);
    }

    private void DisplayFireExtinguisherText()
    {
        DisplayErrorStatus("Fire Extinguisher Error", fireExtinguisherErrorText);
    }

    private void DisplayFODsText()
    {
        DisplayErrorStatus("FOD Error", fodErrorText);
    }

    //private void SetText(string message, Color color)
    //{
    //  antennaErrorText.text = message;
    //  antennaErrorText.color = color;
    // }

    private void DisplayTime()
    {
        float finalTime = PersistentDataStore.assessmentTime;
        // or PlayerPrefs.GetFloat("AssessmentTime", 0f);

        int minutes = Mathf.FloorToInt(finalTime / 60f);
        int seconds = Mathf.FloorToInt(finalTime % 60f);
        finalTimeText.text = string.Format("Final time: {0:00}:{1:00}", minutes, seconds);
    }

    private void SetText(TextMeshProUGUI textComponent, string message, Color color)
    {
        textComponent.text = message;
        textComponent.color = color;
    }

    private void SaveSummaryData()
    {
        // 1) Identify the current user
        string user = TraineeRegister.currentUsername;  // The static variable
        if (string.IsNullOrEmpty(user))
        {
            Debug.LogWarning("No current user found. Cannot save summary data.");
            return;
        }

        // 2) Gather final time
        float finalTime = PersistentDataStore.assessmentTime;

        // 3) Gather error statuses (strings for CSV)
        string antennaStatus = GetErrorStatusString("Antenna Error");
        string cableStatus = GetErrorStatusString("Cable Error");
        string rightCornerStatus = GetErrorStatusString("Right Cornerboard Error");
        string leftCornerStatus = GetErrorStatusString("Left Cornerboard Error");
        string leftLadderStatus = GetErrorStatusString("Left Ladder Error");
        string rightLadderStatus = GetErrorStatusString("Right Ladder Error");
        string groundingRodStatus = GetErrorStatusString("Grounding Rod Error");
        string fireExtinguisherStatus = GetErrorStatusString("Fire Extinguisher Error");
        string fodStatus = GetErrorStatusString("FOD Error");

        // 4) Calculate the score
        int correctedCount = 0;
        int testedCount = 0;
        // We'll treat "NotTested" as not part of testedCount, 
        // so testedCount = corrected + notCorrected

        // For convenience, let's store all statuses in a local array
        string[] allStatuses = {
        antennaStatus,
        cableStatus,
        rightCornerStatus,
        leftCornerStatus,
        leftLadderStatus,
        rightLadderStatus,
        groundingRodStatus,
        fireExtinguisherStatus,
        fodStatus
    };

        // Count how many are Corrected, how many are tested
        foreach (string status in allStatuses)
        {
            if (status == ErrorStatus.Corrected.ToString())
            {
                correctedCount++;
                testedCount++;
            }
            else if (status == ErrorStatus.NotCorrected.ToString())
            {
                testedCount++;
            }
            // If "NotTested" or "Unknown", ignore from testedCount
        }

        if (scoreText != null)
        {
            // Example: "Score: 3/5 (60%)"
            scoreText.text = $"Score: {correctedCount}/{testedCount}";
        }

        // 5) Update the CSV row
        // Pass these new parameters (correctedCount, testedCount) to UpdateTraineeRow
        TraineeRegister.UpdateTraineeRow(
            user, finalTime,
            antennaStatus, cableStatus, rightCornerStatus, leftCornerStatus,
            leftLadderStatus, rightLadderStatus, groundingRodStatus,
            fireExtinguisherStatus, fodStatus,
            correctedCount, testedCount
        );
    }

    private string GetErrorStatusString(string key)
    {
        if (PersistentDataStore.errorStatuses.TryGetValue(key, out var data))
        {
            return data.status.ToString(); // e.g., "Corrected", "NotCorrected", etc.
        }
        return "Unknown";
    }

}