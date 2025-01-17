using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssessmentStartButton : MonoBehaviour
{
    public AssessmentManager assessmentManager;

    public void StartAssessment()
    {
        assessmentManager.StartAssessment();
    }
}
