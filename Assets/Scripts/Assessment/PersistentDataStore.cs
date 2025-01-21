using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PersistentDataStore
{
    // Initialize the errorStatuses dictionary and make sure FOD Error is tracked.
    static PersistentDataStore()
    {
        if (!errorStatuses.ContainsKey("FOD Error"))
        {
            errorStatuses["FOD Error"] = new ErrorStatusData { status = ErrorStatus.NotCorrected };
        }
    }

    public static float assessmentTime = 0f;

    public static Dictionary<string, ErrorStatusData> errorStatuses = new Dictionary<string, ErrorStatusData>();
}

public class ErrorStatusData
{
    public ErrorStatus status;
}

public enum ErrorStatus
{
    Corrected,
    NotCorrected,
    NotTested
}
