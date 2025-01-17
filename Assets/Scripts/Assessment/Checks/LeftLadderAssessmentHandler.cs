using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftLadderAssessmentHandler : MonoBehaviour
{
    public GameObject leftLadderUndeployed;
    public GameObject leftLadderDeployed;

    public string leftLadderErrorKey = "Left Ladder Error";

    private bool hasInteracted = false;

    public void OnInteract()
    {
        if (leftLadderUndeployed == null || leftLadderDeployed == null)
        {
            Debug.LogError("Left Ladder GameObjects are not assigned!");
            return;
        }

        // Toggle states
        bool wasUndeployed = leftLadderUndeployed.activeSelf;

        leftLadderUndeployed.SetActive(!wasUndeployed);
        leftLadderDeployed.SetActive(wasUndeployed);

        // Update error status
        UpdateErrorStatus(isDeployed: wasUndeployed);

        hasInteracted = true;
        Debug.Log($"Player interacted. Status: {PersistentDataStore.errorStatuses[leftLadderErrorKey].status}");
    }

    private void UpdateErrorStatus(bool isDeployed)
    {
        if (!PersistentDataStore.errorStatuses.TryGetValue(leftLadderErrorKey, out var errorStatusData))
        {
            errorStatusData = new ErrorStatusData();
            PersistentDataStore.errorStatuses[leftLadderErrorKey] = errorStatusData;
        }

        errorStatusData.status = isDeployed ? ErrorStatus.Corrected : ErrorStatus.NotCorrected;
    }

    private void Start()
    {
        if (!PersistentDataStore.errorStatuses.ContainsKey(leftLadderErrorKey))
        {
            PersistentDataStore.errorStatuses[leftLadderErrorKey] = new ErrorStatusData();
        }

        var initialStatus = PersistentDataStore.errorStatuses[leftLadderErrorKey].status;

        // Log current state for debugging
        Debug.Log($"Initial status for {leftLadderErrorKey}: {initialStatus}");
    }

    private void OnDestroy()
    {
        if (!hasInteracted && leftLadderUndeployed.activeSelf)
        {
            PersistentDataStore.errorStatuses[leftLadderErrorKey].status = ErrorStatus.NotCorrected;
        }
        Debug.Log($"Final status on destroy: {PersistentDataStore.errorStatuses[leftLadderErrorKey].status}");
    }
}
