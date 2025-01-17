using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FODsAssessmentHandler : MonoBehaviour
{
    [Header("FOD Objects")]
    public GameObject firstAidKit;
    public GameObject aluminiumCan;
    public GameObject wrench;
    public GameObject brokenBottle;
    public GameObject bucket;
    public GameObject lunchBox;
    public GameObject paperBox;
    public GameObject newspaper;
    public GameObject axe;
    public GameObject hammer;

    // Internally track which FODs are currently active in the scene
    private List<GameObject> activeFODs = new List<GameObject>();

    private void Start()
    {
        // Randomize and ensure at least 5 FOD objects are active
        RandomizeFODs();

        // After randomizing FODs, initialize the "FOD Error" status
        InitializeFODErrorStatus();
    }

    /// <summary>
    /// Randomly activate or deactivate each FOD. Ensures at least 5 are active.
    /// </summary>
    private void RandomizeFODs()
    {
        List<GameObject> fodObjects = new List<GameObject>
        {
            firstAidKit, aluminiumCan, wrench, brokenBottle,
            bucket, lunchBox, paperBox, newspaper, axe, hammer
        };

        int activeCount = 0;

        // Randomly activate or deactivate each FOD
        foreach (var fod in fodObjects)
        {
            if (fod == null) continue;

            bool isActive = Random.value > 0.5f; // ~50% chance
            fod.SetActive(isActive);
            if (isActive) activeCount++;
        }

        // Ensure at least 5 FOD objects are active
        while (activeCount < 5)
        {
            foreach (var fod in fodObjects)
            {
                // If this FOD is not active, activate it
                if (!fod.activeSelf && activeCount < 5)
                {
                    fod.SetActive(true);
                    activeCount++;
                }
            }
        }
    }

    /// <summary>
    /// Gathers all currently active FODs, and sets the 
    /// "FOD Error" in PersistentDataStore accordingly.
    /// </summary>
    private void InitializeFODErrorStatus()
    {
        // Clear our tracking list
        activeFODs.Clear();

        // Re-gather references to all FOD objects in the scene
        GameObject[] fodObjects = {
            firstAidKit, aluminiumCan, wrench, brokenBottle, bucket,
            lunchBox, paperBox, newspaper, axe, hammer
        };

        // Collect the ones currently active
        foreach (GameObject fod in fodObjects)
        {
            if (fod != null && fod.activeSelf)
            {
                activeFODs.Add(fod);
            }
        }

        // Ensure "FOD Error" key exists in the persistent store
        if (!PersistentDataStore.errorStatuses.ContainsKey("FOD Error"))
        {
            PersistentDataStore.errorStatuses["FOD Error"] = new ErrorStatusData();
        }

        // If at least one FOD is active, set "NotCorrected"; otherwise "Corrected"
        if (activeFODs.Count > 0)
        {
            PersistentDataStore.errorStatuses["FOD Error"].status = ErrorStatus.NotCorrected;
        }
        else
        {
            PersistentDataStore.errorStatuses["FOD Error"].status = ErrorStatus.Corrected;
        }

        Debug.Log($"FODs initialized. Active FOD count = {activeFODs.Count}. " +
                  $"FOD Error status = {PersistentDataStore.errorStatuses["FOD Error"].status}");
    }

    /// <summary>
    /// Called when the player "picks up" or "corrects" a FOD.
    /// </summary>
    /// <param name="fod">FOD object that was clicked/interacted with.</param>
    public void InteractWithFOD(GameObject fod)
    {
        // Make sure the FOD is valid and in the active list
        if (fod == null || !activeFODs.Contains(fod))
        {
            Debug.LogWarning("Invalid FOD or FOD not in active list.");
            return;
        }

        // Deactivate the FOD game object
        fod.SetActive(false);

        // Remove from activeFODs
        activeFODs.Remove(fod);

        Debug.Log($"FOD corrected: {fod.name}. Remaining active FODs: {activeFODs.Count}");

        // Check if all FODs have been corrected
        CheckFODCompletion();
    }

    /// <summary>
    /// If no FODs remain active, mark status as Corrected; otherwise, NotCorrected.
    /// </summary>
    private void CheckFODCompletion()
    {
        if (activeFODs.Count == 0)
        {
            PersistentDataStore.errorStatuses["FOD Error"].status = ErrorStatus.Corrected;
        }
        else
        {
            PersistentDataStore.errorStatuses["FOD Error"].status = ErrorStatus.NotCorrected;
        }

        Debug.Log($"FOD Error status updated: {PersistentDataStore.errorStatuses["FOD Error"].status}");
    }
}
