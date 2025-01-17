using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntennaInteractionHandler : MonoBehaviour
{
    // Assign these in the Unity Inspector
    public GameObject antennaUndeployed;
    public GameObject antennaDeployed;

    public void OnInteract()
    {
        // Check if objects are assigned
        if (antennaUndeployed == null || antennaDeployed == null)
        {
            Debug.LogError("Antenna GameObjects are not assigned in the Inspector!");
            return;
        }

        // Toggle visibility
        bool isUndeployedActive = antennaUndeployed.activeSelf;

        antennaUndeployed.SetActive(!isUndeployedActive);
        antennaDeployed.SetActive(isUndeployedActive);
    }
}

