using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssessmentFEHandler : MonoBehaviour
{
    public GameObject initialCanvas;

    public void OnInteract()
    {
        if (initialCanvas != null)
            initialCanvas.SetActive(true);
    }
}
