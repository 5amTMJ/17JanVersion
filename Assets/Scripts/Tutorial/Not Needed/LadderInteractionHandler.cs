using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderInteractionHandler : MonoBehaviour
{
    // Assign these in the Unity Inspector
    public GameObject ladderUndeployed;
    public GameObject ladderDeployed;
    public void OnInteract()
    {
        // Check if objects are assigned
        if (ladderUndeployed == null || ladderDeployed == null)
        {
            Debug.LogError("Ladder GameObjects are not assigned in the Inspector!");
            return;
        }

        // Toggle visibility
        bool isUndeployedActive = ladderUndeployed.activeSelf;

        ladderUndeployed.SetActive(!isUndeployedActive);
        ladderDeployed.SetActive(isUndeployedActive);
    }
}
