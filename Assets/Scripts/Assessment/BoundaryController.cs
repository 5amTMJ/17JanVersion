using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryController : MonoBehaviour
{
    public BoxCollider boundary;
    public AssessmentManager assessmentManager;
    public Timer timer;

    void Update()
    {
        if (!timer.isRunning && !boundary.enabled)
        {
            Vector3 playerPosition = Camera.main.transform.position;
            if (playerPosition.x < boundary.bounds.min.x || playerPosition.x > boundary.bounds.max.x ||
                playerPosition.z < boundary.bounds.min.z || playerPosition.z > boundary.bounds.max.z)
            {
                playerPosition.x = Mathf.Clamp(playerPosition.x, boundary.bounds.min.x, boundary.bounds.max.x);
                playerPosition.z = Mathf.Clamp(playerPosition.z, boundary.bounds.min.z, boundary.bounds.max.z);
                Camera.main.transform.position = playerPosition;
            }
        }
    }

    public void StartAssessment()
    {
        if(!timer.isRunning&&!boundary.enabled)
        {
            timer.StartTimer();
        }
    }
}
