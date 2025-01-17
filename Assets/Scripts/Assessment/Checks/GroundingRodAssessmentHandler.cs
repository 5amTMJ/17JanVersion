using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundingRodAssessmentHandler : MonoBehaviour
{
    public GameObject groundingRodUndeployed;
    public GameObject groundingRodDeployed;

    public string groundingRodErrorKey = "Grounding Rod Error";

    private bool hasInteracted = false;

    public void OnInteract()
    {
        if (groundingRodUndeployed == null || groundingRodDeployed == null)
        {
            Debug.LogError("Grounding Rod GameObjects are not assigned!");
            return;
        }

        // Toggle states
        bool wasUndeployed = groundingRodUndeployed.activeSelf;

        groundingRodUndeployed.SetActive(!wasUndeployed);
        groundingRodDeployed.SetActive(wasUndeployed);

        // Update error status
        UpdateErrorStatus(isDeployed: wasUndeployed);

        hasInteracted = true;
        Debug.Log($"Player interacted. Status: {PersistentDataStore.errorStatuses[groundingRodErrorKey].status}");
    }

    private void UpdateErrorStatus(bool isDeployed)
    {
        if (!PersistentDataStore.errorStatuses.TryGetValue(groundingRodErrorKey, out var errorStatusData))
        {
            errorStatusData = new ErrorStatusData();
            PersistentDataStore.errorStatuses[groundingRodErrorKey] = errorStatusData;
        }

        errorStatusData.status = isDeployed ? ErrorStatus.Corrected : ErrorStatus.NotCorrected;
    }

    private void Start()
    {
        if (!PersistentDataStore.errorStatuses.ContainsKey(groundingRodErrorKey))
        {
            PersistentDataStore.errorStatuses[groundingRodErrorKey] = new ErrorStatusData();
        }

        var initialStatus = PersistentDataStore.errorStatuses[groundingRodErrorKey].status;

        // Log current state for debugging
        Debug.Log($"Initial status for {groundingRodErrorKey}: {initialStatus}");
    }

    private void OnDestroy()
    {
        if (!hasInteracted && groundingRodUndeployed.activeSelf)
        {
            PersistentDataStore.errorStatuses[groundingRodErrorKey].status = ErrorStatus.NotCorrected;
        }
        Debug.Log($"Final status on destroy: {PersistentDataStore.errorStatuses[groundingRodErrorKey].status}");
    }
}
