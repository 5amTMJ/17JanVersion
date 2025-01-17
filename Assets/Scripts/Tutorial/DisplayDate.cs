using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;
using System;

public class DisplayDate : MonoBehaviour
{
    // Reference to the UI Text or TextMeshPro component
    public TextMeshProUGUI uiText; // For Unity UI Text
    // public TextMeshProUGUI uiText; // Uncomment if using TextMeshPro

    void Start()
    {
        // Get the current date minus one month
        DateTime oneMonthAgo = DateTime.Now.AddMonths(-12);

        // Format the date (e.g., "02 Dec 2024")
        string formattedDate = oneMonthAgo.ToString("dd MMM yyyy");

        // Display the formatted date on the UI
        if (uiText != null)
        {
            uiText.text = formattedDate;
        }
        else
        {
            Debug.LogWarning("UI Text component is not assigned!");
        }
    }
}
