using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerboardInteractionHandler : MonoBehaviour
{
    // Assign these in the Unity Inspector
    public GameObject cornerboardUndeployed;
    public GameObject cornerboardDeployed;
    public void OnInteract()
    {
        // Check if objects are assigned
        if (cornerboardUndeployed == null || cornerboardDeployed == null)
        {
            Debug.LogError("Antenna GameObjects are not assigned in the Inspector!");
            return;
        }

        // Toggle visibility
        bool isUndeployedActive = cornerboardUndeployed.activeSelf;

        cornerboardUndeployed.SetActive(!isUndeployedActive);
        cornerboardDeployed.SetActive(isUndeployedActive);
    }
}
