using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ErrorRandomiser : MonoBehaviour
{
    private List<GameObject> activeFODs = new List<GameObject>();
    private HashSet<GameObject> interactedFODs = new HashSet<GameObject>();


    [Header("Antenna Check")]
    public GameObject antennaDeployed;
    public GameObject antennaUndeployed;

    [Header("Exposed Cable")]
    public GameObject exposedCableArea;

    [Header("Right Cornerboard Check")]
    public GameObject rightCornerboardDeployed;
    public GameObject rightCornerboardUndeployed;

    [Header("Left Cornerboard Check")]
    public GameObject leftCornerboardDeployed;
    public GameObject leftCornerboardUndeployed;

    [Header("Right Ladder Check")]
    public GameObject rightLadderUsable;
    public GameObject rightLadderCracked;

    [Header("Left Ladder Check")]
    public GameObject leftLadderUsable;
    public GameObject leftladderCracked;

    [Header("Fire Extinguisher Check")]
    public GameObject expiredFireExtinguisher;
    public GameObject validFireExtinguisher;

    [Header("Grounding Rod Check")]
    public GameObject invalidGroundingRod;
    public GameObject validGroundingRod;

    //[Header("FOD Check")]
    //public GameObject firstAidKit;
    //public GameObject aluminiumCan;
    //public GameObject wrench;
    //public GameObject brokenBottle;
    //public GameObject bucket;
    //public GameObject lunchBox;
    //public GameObject paperBox;
    //public GameObject newspaper;
    //public GameObject axe;
    //public GameObject hammer;

    private void Start()
    {
        InitializeErrors();
    }

    private void InitializeErrors()
    {
        InitializeAntenna();
        InitializeExposedCable();
        InitializeRightCornerboard();
        InitializeLeftCornerboard();
        InitializeRightLadder();
        InitializeLeftLadder();
        InitializeFireExtinguisher();
        InitializeGroundingRod();
        //InitializeFODs();
    }

    private void InitializeAntenna()
    {
        bool isErrorActive = Random.value > 0.5f;
        antennaUndeployed.SetActive(isErrorActive);
        antennaDeployed.SetActive(!isErrorActive);

        // Set initial error status
        if (!PersistentDataStore.errorStatuses.ContainsKey("Antenna Error"))
        {
            PersistentDataStore.errorStatuses["Antenna Error"] = new ErrorStatusData();
        }

        if (isErrorActive)
        {
            PersistentDataStore.errorStatuses["Antenna Error"].status = ErrorStatus.NotCorrected;
        }
        else
        {
            PersistentDataStore.errorStatuses["Antenna Error"].status = ErrorStatus.NotTested;
        }
    }

    private void InitializeExposedCable()
    {
        bool isErrorActive = Random.value > 0.5f;
        exposedCableArea.SetActive(isErrorActive);

        if (!PersistentDataStore.errorStatuses.ContainsKey("Cable Error"))
        {
            PersistentDataStore.errorStatuses["Cable Error"] = new ErrorStatusData();
        }

        PersistentDataStore.errorStatuses["Cable Error"].status = isErrorActive
            ? ErrorStatus.NotCorrected
            : ErrorStatus.NotTested;
    }

    private void InitializeRightCornerboard()
    {
        bool isErrorActive = Random.value > 0.5f;
        rightCornerboardUndeployed.SetActive(isErrorActive);
        rightCornerboardDeployed.SetActive(!isErrorActive);

        if (!PersistentDataStore.errorStatuses.ContainsKey("Right Cornerboard Error"))
        {
            PersistentDataStore.errorStatuses["Right Cornerboard Error"] = new ErrorStatusData();
        }

        PersistentDataStore.errorStatuses["Right Cornerboard Error"].status = isErrorActive
            ? ErrorStatus.NotCorrected
            : ErrorStatus.NotTested;
    }

    private void InitializeLeftCornerboard()
    {
        bool isErrorActive = Random.value > 0.5f;
        leftCornerboardUndeployed.SetActive(isErrorActive);
        leftCornerboardDeployed.SetActive(!isErrorActive);

        if (!PersistentDataStore.errorStatuses.ContainsKey("Left Cornerboard Error"))
        {
            PersistentDataStore.errorStatuses["Left Cornerboard Error"] = new ErrorStatusData();
        }

        PersistentDataStore.errorStatuses["Left Cornerboard Error"].status = isErrorActive
            ? ErrorStatus.NotCorrected
            : ErrorStatus.NotTested;
    }

    private void InitializeRightLadder()
    {
        bool isErrorActive = Random.value > 0.5f;
        rightLadderCracked.SetActive(isErrorActive);
        rightLadderUsable.SetActive(!isErrorActive);

        if (!PersistentDataStore.errorStatuses.ContainsKey("Right Ladder Error"))
        {
            PersistentDataStore.errorStatuses["Right Ladder Error"] = new ErrorStatusData();
        }

        PersistentDataStore.errorStatuses["Right Ladder Error"].status = isErrorActive
            ? ErrorStatus.NotCorrected
            : ErrorStatus.NotTested;
    }

    private void InitializeLeftLadder()
    {
        bool isErrorActive = Random.value > 0.5f;
        leftladderCracked.SetActive(isErrorActive);
        leftLadderUsable.SetActive(!isErrorActive);

        if (!PersistentDataStore.errorStatuses.ContainsKey("Left Ladder Error"))
        {
            PersistentDataStore.errorStatuses["Left Ladder Error"] = new ErrorStatusData();
        }

        PersistentDataStore.errorStatuses["Left Ladder Error"].status = isErrorActive
            ? ErrorStatus.NotCorrected
            : ErrorStatus.NotTested;
    }

    private void InitializeFireExtinguisher()
    {
        //bool isErrorActive = Random.value > 0.5f;
        //expiredFireExtinguisher.SetActive(isErrorActive);
        //validFireExtinguisher.SetActive(!isErrorActive);

        if (!PersistentDataStore.errorStatuses.ContainsKey("Fire Extinguisher Error"))
        {
            PersistentDataStore.errorStatuses["Fire Extinguisher Error"] = new ErrorStatusData();
        }

        // Set the initial status to NotCorrected
        PersistentDataStore.errorStatuses["Fire Extinguisher Error"].status = ErrorStatus.NotCorrected;
    }

    private void InitializeGroundingRod()
    {
        bool isErrorActive = Random.value > 0.5f;
        invalidGroundingRod.SetActive(isErrorActive);
        validGroundingRod.SetActive(!isErrorActive);

        if (!PersistentDataStore.errorStatuses.ContainsKey("Grounding Rod Error"))
        {
            PersistentDataStore.errorStatuses["Grounding Rod Error"] = new ErrorStatusData();
        }

        PersistentDataStore.errorStatuses["Grounding Rod Error"].status = isErrorActive
            ? ErrorStatus.NotCorrected
            : ErrorStatus.NotTested;
    }

    //private void InitializeFODs()
    //{
        //List<GameObject> fodObjects = new List<GameObject>
        //{
            //firstAidKit, aluminiumCan, wrench, brokenBottle,
            //bucket, lunchBox, paperBox, newspaper, axe, hammer
        //};

        //int activeFODCount = 0;

        //foreach (var fod in fodObjects)
        //{
            //bool isActive = Random.value > 0.5f;
            //fod.SetActive(isActive);
            //if (isActive) activeFODCount++;
        //}

        //while (activeFODCount < 5)
        //{
            //foreach (var fod in fodObjects)
            //{
                //if (!fod.activeSelf && activeFODCount < 5)
                //{
                    //fod.SetActive(true);
                    //activeFODCount++;
                //}
            //}
        //}
    //}
}
