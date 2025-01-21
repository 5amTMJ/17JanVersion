using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI timerText;

    public float currentTime = 0f;
    public bool isRunning = false;

    private void Start()
    {
        // Initialize display (shows 00:00 at start)
        UpdateTimeDisplay();
    }

    private void Update()
    {
        // If timer is running, accumulate time
        if (isRunning)
        {
            currentTime += Time.deltaTime;
            UpdateTimeDisplay();
        }
    }

    /// <summary>
    /// Start the timer from zero.
    /// </summary>
    public void StartTimer()
    {
        currentTime = 0f;   // reset to 0
        isRunning = true;   // begin counting up
    }

    /// <summary>
    /// Stop the timer and store the time in a persistent variable.
    /// Call this when the user clicks "Exit Assessment" or similar.
    /// </summary>
    public void StopTimer()
    {
        isRunning = false;

        // Store the final time so it can be accessed in another scene
        PersistentDataStore.assessmentTime = currentTime;

        Debug.Log($"Timer stopped at: {FormatTime(currentTime)}");
    }

    /// <summary>
    /// Optional: Reset the timer to 0 and pause it.
    /// </summary>
    public void ResetTimer()
    {
        isRunning = false;
        currentTime = 0f;
        UpdateTimeDisplay();
    }

    /// <summary>
    /// Updates the on-screen text to show MM:SS
    /// </summary>
    private void UpdateTimeDisplay()
    {
        timerText.text = FormatTime(currentTime);
    }

    /// <summary>
    /// Helper method to format a float time as MM:SS
    /// </summary>
    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
