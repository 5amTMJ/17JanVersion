using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuToAssessment : MonoBehaviour
{
    public void GoToAssessmentScene()
    {
        SceneManager.LoadScene("AssessmentScene");
    }
}