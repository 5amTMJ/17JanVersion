using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableAssessmentHandler : MonoBehaviour
{
    public GameObject exposedCableArea; // Assign in Inspector
    public string cableErrorKey = "Cable Error";

    private bool hasInteracted = false;

    public void OnInteract()
    {
        if (exposedCableArea == null)
        {
            Debug.LogError("Exposed Cable Area is not assigned in the Inspector!");
            return;
        }

        // Toggle cable state
        bool isCableExposed = exposedCableArea.activeSelf;
        exposedCableArea.SetActive(!isCableExposed);

        // Update error status based on player interaction
        UpdateErrorStatus(isExposed: isCableExposed);

        hasInteracted = true;
        Debug.Log($"Player interacted with Cable Check. Status updated to: {PersistentDataStore.errorStatuses[cableErrorKey].status}");
    }

    private void UpdateErrorStatus(bool isExposed)
    {
        if (!PersistentDataStore.errorStatuses.TryGetValue(cableErrorKey, out var errorStatusData))
        {
            errorStatusData = new ErrorStatusData();
            PersistentDataStore.errorStatuses[cableErrorKey] = errorStatusData;
        }

        // Set the correct status
        errorStatusData.status = isExposed ? ErrorStatus.Corrected : ErrorStatus.NotCorrected;
    }

    private void Start()
    {
        if (!PersistentDataStore.errorStatuses.ContainsKey(cableErrorKey))
        {
            PersistentDataStore.errorStatuses[cableErrorKey] = new ErrorStatusData();
        }

        var initialStatus = PersistentDataStore.errorStatuses[cableErrorKey].status;

        if (initialStatus == ErrorStatus.NotCorrected && !exposedCableArea.activeSelf)
        {
            Debug.LogError("Exposed Cable Area was expected to be active but is not!");
        }

        Debug.Log($"Initial Cable Error status: {PersistentDataStore.errorStatuses[cableErrorKey].status}");
    }

    private void OnDestroy()
    {
        if (!hasInteracted && exposedCableArea.activeSelf)
        {
            PersistentDataStore.errorStatuses[cableErrorKey].status = ErrorStatus.NotCorrected;
        }

        Debug.Log($"Final Cable Error status on destroy: {PersistentDataStore.errorStatuses[cableErrorKey].status}");
    }
}
