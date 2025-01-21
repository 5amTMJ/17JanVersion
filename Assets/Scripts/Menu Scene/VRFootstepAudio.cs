using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Plays footstep sounds at a regular interval whenever the player is moving.
/// Attach this to your VR player rig or a relevant movement object.
/// </summary>
public class VRFootstepAudio : MonoBehaviour
{
    [Header("Footstep Settings")]
    public AudioSource footstepSource;   // An AudioSource component with your footstep clip(s)
    public float stepInterval = 0.5f;    // Time between footsteps while moving
    public float movementThreshold = 0.01f; // Min distance to move between frames to count as "walking"

    private float stepTimer = 0f;
    private Vector3 lastPosition;

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        // Calculate distance moved since last frame
        float distanceMoved = Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;

        // If we moved more than the threshold, we're "walking"
        if (distanceMoved > movementThreshold)
        {
            stepTimer += Time.deltaTime;
            // Once the timer surpasses stepInterval, play a footstep
            if (stepTimer >= stepInterval)
            {
                stepTimer = 0f;
                PlayFootstep();
            }
        }
        else
        {
            // Reset the timer if we're not moving
            stepTimer = 0f;
        }
    }

    private void PlayFootstep()
    {
        if (footstepSource != null)
        {
            footstepSource.Play();
        }
    }
}
