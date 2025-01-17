using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightCornerboardAssessmentHandler : MonoBehaviour
{
    public GameObject rightCornerboardUndeployed;
    public GameObject rightCornerboardDeployed;

    public string rightCornerboardErrorKey = "Right Cornerboard Error";

    private bool hasInteracted = false;

    public void OnInteract()
    {
        if (rightCornerboardUndeployed == null || rightCornerboardDeployed == null)
        {
            Debug.LogError("Right Cornerboard GameObjects are not assigned!");
            return;
        }

        // Toggle states
        bool wasUndeployed = rightCornerboardUndeployed.activeSelf;

        rightCornerboardUndeployed.SetActive(!wasUndeployed);
        rightCornerboardDeployed.SetActive(wasUndeployed);

        // Update error status
        UpdateErrorStatus(isDeployed: wasUndeployed);

        hasInteracted = true;
        Debug.Log($"Player interacted. Status: {PersistentDataStore.errorStatuses[rightCornerboardErrorKey].status}");
    }

    private void UpdateErrorStatus(bool isDeployed)
    {
        if (!PersistentDataStore.errorStatuses.TryGetValue(rightCornerboardErrorKey, out var errorStatusData))
        {
            errorStatusData = new ErrorStatusData();
            PersistentDataStore.errorStatuses[rightCornerboardErrorKey] = errorStatusData;
        }

        errorStatusData.status = isDeployed ? ErrorStatus.Corrected : ErrorStatus.NotCorrected;
    }

    private void Start()
    {
        if (!PersistentDataStore.errorStatuses.ContainsKey(rightCornerboardErrorKey))
        {
            PersistentDataStore.errorStatuses[rightCornerboardErrorKey] = new ErrorStatusData();
        }

        var initialStatus = PersistentDataStore.errorStatuses[rightCornerboardErrorKey].status;

        // Log current state for debugging
        Debug.Log($"Initial status for {rightCornerboardErrorKey}: {initialStatus}");
    }

    private void OnDestroy()
    {
        if (!hasInteracted && rightCornerboardUndeployed.activeSelf)
        {
            PersistentDataStore.errorStatuses[rightCornerboardErrorKey].status = ErrorStatus.NotCorrected;
        }
        Debug.Log($"Final status on destroy: {PersistentDataStore.errorStatuses[rightCornerboardErrorKey].status}");
    }
}
