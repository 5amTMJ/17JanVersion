using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundingRodInteractionHandler : MonoBehaviour
{
    // Assign these in the Unity Inspector
    public GameObject groundingRodUndeployed;
    public GameObject groundingRodDeployed;
    public void OnInteract()
    {
        // Check if objects are assigned
        if (groundingRodUndeployed == null || groundingRodDeployed == null)
        {
            Debug.LogError("Grounding Rod GameObjects are not assigned in the Inspector!");
            return;
        }

        // Toggle visibility
        bool isUndeployedActive = groundingRodUndeployed.activeSelf;

        groundingRodUndeployed.SetActive(!isUndeployedActive);
        groundingRodDeployed.SetActive(isUndeployedActive);
    }
}
