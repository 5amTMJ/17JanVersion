using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LightBaker : MonoBehaviour
{
#if UNITY_EDITOR
    void Start()
    {
        if (!Application.isPlaying)
        {
            Lightmapping.giWorkflowMode = Lightmapping.GIWorkflowMode.Iterative;
            Lightmapping.bakeCompleted += OnBakeCompleted;

            Debug.Log("Starting light baking...");
        }
    }

    // Update is called once per frame
    void OnBakeCompleted()
    {
        Debug.Log("Light baking completed successfully.");
        Lightmapping.bakeCompleted -= OnBakeCompleted;
    }
#endif 
}
