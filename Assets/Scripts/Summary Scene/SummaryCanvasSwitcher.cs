using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummaryCanvasSwitcher : MonoBehaviour
{
    public GameObject summarySheetCanvas;   // Assign the "Summary Sheet Canvas" in Inspector
    public GameObject summaryTableCanvas;   // Assign the "Summary Table Canvas" in Inspector

    public void ShowSummaryTable()
    {
        // Hide the summary sheet
        if (summarySheetCanvas != null)
            summarySheetCanvas.SetActive(false);

        // Show the summary table
        if (summaryTableCanvas != null)
            summaryTableCanvas.SetActive(true);
    }
}
