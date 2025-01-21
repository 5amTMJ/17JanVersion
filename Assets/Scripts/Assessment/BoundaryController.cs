using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryController : MonoBehaviour
{
    public BoxCollider boundary;
    public AssessmentManager assessmentManager;
    public Timer timer;  // Your Timer script from the previous example

    void Update()
    {
        // If the timer is NOT running, and the boundary is already disabled,
        // we clamp the player’s position to keep them in last known area.
        if (!timer.isRunning && !boundary.enabled)
        {
            Vector3 playerPosition = Camera.main.transform.position;

            // If out of bounds, clamp position
            if (playerPosition.x < boundary.bounds.min.x || playerPosition.x > boundary.bounds.max.x ||
                playerPosition.z < boundary.bounds.min.z || playerPosition.z > boundary.bounds.max.z)
            {
                playerPosition.x = Mathf.Clamp(playerPosition.x, boundary.bounds.min.x, boundary.bounds.max.x);
                playerPosition.z = Mathf.Clamp(playerPosition.z, boundary.bounds.min.z, boundary.bounds.max.z);
                Camera.main.transform.position = playerPosition;
            }
        }
    }

    /// <summary>
    /// Called when the user selects "Start Assessment."
    /// Disables the boundary box collider and starts the timer.
    /// </summary>
    public void StartAssessment()
    {
        // Only start the timer if it’s currently not running, and boundary is enabled
        if (!timer.isRunning && boundary.enabled)
        {
            boundary.enabled = false;   // Hide/disable the boundary
            timer.StartTimer();         // Begin counting up
        }
    }
}
