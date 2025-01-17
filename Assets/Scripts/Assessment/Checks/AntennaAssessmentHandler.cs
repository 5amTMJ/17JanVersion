using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntennaAssessmentHandler : MonoBehaviour
{
    public GameObject antennaUndeployed; // Assign in Inspector
    public GameObject antennaDeployed;   // Assign in Inspector

    public string antennaErrorKey = "Antenna Error";

    private bool hasInteracted = false;

    public void OnInteract()
    {
        if (antennaUndeployed == null || antennaDeployed == null)
        {
            Debug.LogError("Antenna GameObjects are not assigned in the Inspector!");
            return;
        }

        // Toggle antenna states
        bool isUndeployedActive = antennaUndeployed.activeSelf;

        antennaUndeployed.SetActive(!isUndeployedActive);
        antennaDeployed.SetActive(isUndeployedActive);

        // Update error status based on player interaction
        UpdateErrorStatus(isDeployed: isUndeployedActive);

        hasInteracted = true;
        Debug.Log($"Player interacted. Status updated to: {PersistentDataStore.errorStatuses[antennaErrorKey].status}");
    }

    private void UpdateErrorStatus(bool isDeployed)
    {
        if (!PersistentDataStore.errorStatuses.TryGetValue(antennaErrorKey, out var errorStatusData))
        {
            errorStatusData = new ErrorStatusData();
            PersistentDataStore.errorStatuses[antennaErrorKey] = errorStatusData;
        }

        // Set the correct status
        errorStatusData.status = isDeployed ? ErrorStatus.Corrected : ErrorStatus.NotCorrected;
    }

    private void Start()
    {
        if (!PersistentDataStore.errorStatuses.ContainsKey(antennaErrorKey))
        {
            PersistentDataStore.errorStatuses[antennaErrorKey] = new ErrorStatusData();
        }

        // Do not change the status set by the ErrorRandomiser during initialization
        var initialStatus = PersistentDataStore.errorStatuses[antennaErrorKey].status;

        if (initialStatus == ErrorStatus.NotCorrected && !antennaUndeployed.activeSelf)
        {
            Debug.LogError("Antenna Undeployed was expected to be active but is not!");
        }

        Debug.Log($"Initial status: {PersistentDataStore.errorStatuses[antennaErrorKey].status}");
    }

    private void OnDestroy()
    {
        if (!hasInteracted && antennaUndeployed.activeSelf)
        {
            PersistentDataStore.errorStatuses[antennaErrorKey].status = ErrorStatus.NotCorrected;
        }

        Debug.Log($"Final status on destroy: {PersistentDataStore.errorStatuses[antennaErrorKey].status}");
    }
}
