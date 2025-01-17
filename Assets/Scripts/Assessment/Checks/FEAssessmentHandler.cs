using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class FEAssessmentHandler : MonoBehaviour
{
    public GameObject fireExtinguisherCanvas; // Assign in Inspector
    public TextMeshProUGUI dateText; // Assign in Inspector
    public string fireExtinguisherErrorKey = "Fire Extinguisher Error";

    private DateTime inspectionDate;
    void Start()
    {
        // Ensure the error is initialized in the PersistentDataStore
        if (!PersistentDataStore.errorStatuses.ContainsKey(fireExtinguisherErrorKey))
        {
            PersistentDataStore.errorStatuses[fireExtinguisherErrorKey] = new ErrorStatusData
            {
                status = ErrorStatus.NotCorrected
            };
        }

        fireExtinguisherCanvas.SetActive(false); // Ensure the canvas is initially inactive
    }

    public void OnInteract()
    {
        GenerateRandomDate();
        fireExtinguisherCanvas.SetActive(true);
    }

    private void GenerateRandomDate()
    {
        // Generate a random date between the current date and 1.5 years ago
        DateTime currentDate = DateTime.Now;
        TimeSpan randomSpan = TimeSpan.FromDays(UnityEngine.Random.Range(0, 730)); // 1.5 years in days
        inspectionDate = currentDate - randomSpan;

        // Update the canvas text to show the generated date
        dateText.text = $"{inspectionDate:yyyy-MM-dd}";
    }

    public void OnServiceableButton()
    {
        EvaluateSelection(isServiceableSelected: true);
    }

    public void OnUnserviceableButton()
    {
        EvaluateSelection(isServiceableSelected: false);
    }

    private void EvaluateSelection(bool isServiceableSelected)
    {
        DateTime oneYearAgo = DateTime.Now.AddYears(-1);

        // Determine if the fire extinguisher is serviceable
        bool isServiceable = inspectionDate >= oneYearAgo;

        // Check if the player's selection is correct
        bool correctSelection = (isServiceable && isServiceableSelected) || (!isServiceable && !isServiceableSelected);

        // Update the error status based on the player's selection
        PersistentDataStore.errorStatuses[fireExtinguisherErrorKey].status = correctSelection
            ? ErrorStatus.Corrected
            : ErrorStatus.NotCorrected;

        // Debug for validation
        Debug.Log($"Inspection Date: {inspectionDate:yyyy-MM-dd}");
        Debug.Log($"One Year Ago: {oneYearAgo:yyyy-MM-dd}");
        Debug.Log($"Is Serviceable: {isServiceable}");
        Debug.Log($"Player Selection Correct: {correctSelection}");
        Debug.Log($"Fire Extinguisher Status: {PersistentDataStore.errorStatuses[fireExtinguisherErrorKey].status}");

        // Hide the canvas after the selection
        fireExtinguisherCanvas.SetActive(false);
    }
}
