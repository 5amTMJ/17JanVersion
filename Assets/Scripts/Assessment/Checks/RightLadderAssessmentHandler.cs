using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightLadderAssessmentHandler : MonoBehaviour
{
    public GameObject rightLadderUndeployed;
    public GameObject rightLadderDeployed;

    public string rightLadderErrorKey = "Right Ladder Error";

    private bool hasInteracted = false;

    public void OnInteract()
    {
        if (rightLadderUndeployed == null || rightLadderDeployed == null)
        {
            Debug.LogError("Right Ladder GameObjects are not assigned!");
            return;
        }

        // Toggle states
        bool wasUndeployed = rightLadderUndeployed.activeSelf;

        rightLadderUndeployed.SetActive(!wasUndeployed);
        rightLadderDeployed.SetActive(wasUndeployed);

        // Update error status
        UpdateErrorStatus(isDeployed: wasUndeployed);

        hasInteracted = true;
        Debug.Log($"Player interacted. Status: {PersistentDataStore.errorStatuses[rightLadderErrorKey].status}");
    }

    private void UpdateErrorStatus(bool isDeployed)
    {
        if (!PersistentDataStore.errorStatuses.TryGetValue(rightLadderErrorKey, out var errorStatusData))
        {
            errorStatusData = new ErrorStatusData();
            PersistentDataStore.errorStatuses[rightLadderErrorKey] = errorStatusData;
        }

        errorStatusData.status = isDeployed ? ErrorStatus.Corrected : ErrorStatus.NotCorrected;
    }

    private void Start()
    {
        if (!PersistentDataStore.errorStatuses.ContainsKey(rightLadderErrorKey))
        {
            PersistentDataStore.errorStatuses[rightLadderErrorKey] = new ErrorStatusData();
        }

        var initialStatus = PersistentDataStore.errorStatuses[rightLadderErrorKey].status;

        // Log current state for debugging
        Debug.Log($"Initial status for {rightLadderErrorKey}: {initialStatus}");
    }

    private void OnDestroy()
    {
        if (!hasInteracted && rightLadderUndeployed.activeSelf)
        {
            PersistentDataStore.errorStatuses[rightLadderErrorKey].status = ErrorStatus.NotCorrected;
        }
        Debug.Log($"Final status on destroy: {PersistentDataStore.errorStatuses[rightLadderErrorKey].status}");
    }
}
