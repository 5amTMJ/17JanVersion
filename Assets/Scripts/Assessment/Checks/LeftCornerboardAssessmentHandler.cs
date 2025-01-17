using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftCornerboardAssessmentHandler : MonoBehaviour
{
    public GameObject leftCornerboardUndeployed;
    public GameObject leftCornerboardDeployed;

    public string leftCornerboardErrorKey = "Left Cornerboard Error";

    private bool hasInteracted = false;

    public void OnInteract()
    {
        if (leftCornerboardUndeployed == null || leftCornerboardDeployed == null)
        {
            Debug.LogError("Left Cornerboard GameObjects are not assigned!");
            return;
        }

        // Toggle states
        bool wasUndeployed = leftCornerboardUndeployed.activeSelf;

        leftCornerboardUndeployed.SetActive(!wasUndeployed);
        leftCornerboardDeployed.SetActive(wasUndeployed);

        // Update error status
        UpdateErrorStatus(isDeployed: wasUndeployed);

        hasInteracted = true;
        Debug.Log($"Player interacted. Status: {PersistentDataStore.errorStatuses[leftCornerboardErrorKey].status}");
    }

    private void UpdateErrorStatus(bool isDeployed)
    {
        if (!PersistentDataStore.errorStatuses.TryGetValue(leftCornerboardErrorKey, out var errorStatusData))
        {
            errorStatusData = new ErrorStatusData();
            PersistentDataStore.errorStatuses[leftCornerboardErrorKey] = errorStatusData;
        }

        errorStatusData.status = isDeployed ? ErrorStatus.Corrected : ErrorStatus.NotCorrected;
    }

    private void Start()
    {
        if (!PersistentDataStore.errorStatuses.ContainsKey(leftCornerboardErrorKey))
        {
            PersistentDataStore.errorStatuses[leftCornerboardErrorKey] = new ErrorStatusData();
        }

        var initialStatus = PersistentDataStore.errorStatuses[leftCornerboardErrorKey].status;

        // Log current state for debugging
        Debug.Log($"Initial status for {leftCornerboardErrorKey}: {initialStatus}");
    }

    private void OnDestroy()
    {
        if (!hasInteracted && leftCornerboardUndeployed.activeSelf)
        {
            PersistentDataStore.errorStatuses[leftCornerboardErrorKey].status = ErrorStatus.NotCorrected;
        }
        Debug.Log($"Final status on destroy: {PersistentDataStore.errorStatuses[leftCornerboardErrorKey].status}");
    }
}

