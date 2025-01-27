using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;

public class SummaryTableController : MonoBehaviour
{
    [Header("Up to 10 rows for Username, Score, Time")]
    public TextMeshProUGUI[] usernameTexts;   // 10 Text objects for usernames
    public TextMeshProUGUI[] scoreTexts;      // 10 Text objects for scores
    public TextMeshProUGUI[] finalTimeTexts;  // 10 Text objects for times

    private void Start()
    {
        // 1) Load data from CSV
        List<PlayerData> players = LoadAllPlayersFromCSV();

        // Optional: sort or filter if you like
        // For example, sort ascending by finalTime (fastest first):
        // players.Sort((a, b) => a.finalTime.CompareTo(b.finalTime));

        // 2) Show up to 10 players in the UI
        for (int i = 0; i < 10; i++)
        {
            if (i < players.Count)
            {
                // Show row i
                usernameTexts[i].text = players[i].username;
                scoreTexts[i].text = $"{players[i].correctedCount}/{players[i].testedCount}";
                finalTimeTexts[i].text = FormatTime(players[i].finalTime);
            }
            else
            {
                // Clear or hide rows if we have fewer than 10 players
                usernameTexts[i].text = "";
                scoreTexts[i].text = "";
                finalTimeTexts[i].text = "";
            }
        }
    }

    /// <summary>
    /// Reads TraineeAccounts.csv from disk and returns a list of PlayerData
    /// for *all* players (username, finalTime, correctedCount, testedCount).
    /// </summary>
    private List<PlayerData> LoadAllPlayersFromCSV()
    {
        List<PlayerData> result = new List<PlayerData>();
        string filePath = Application.dataPath + "/TraineeAccounts.csv";

        if (!File.Exists(filePath))
        {
            Debug.LogWarning("TraineeAccounts.csv not found. No data to display.");
            return result;
        }

        // Each row example:
        // username,finalTime, ... error statuses..., correctedCount, testedCount
        // e.g.: JohnDoe,120,Corrected,NotCorrected,...,6,8

        string[] lines = File.ReadAllLines(filePath);

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line))
                continue;

            // If you have a header row, skip if it starts with "username,"
            if (i == 0 && line.StartsWith("username,"))
            {
                continue;
            }

            string[] columns = line.Split(',');
            if (columns.Length < 4)
            {
                // Need at least username, finalTime, correctedCount, testedCount
                continue;
            }

            // 0: username
            string username = columns[0].Trim();

            // 1: finalTime
            float finalTimeVal = 0f;
            float.TryParse(columns[1].Trim(), out finalTimeVal);

            // The last 2 columns are correctedCount, testedCount
            int correctedVal = 0;
            int testedVal = 0;

            if (columns.Length >= 4)
            {
                // e.g. if we have 6 columns total, the last 2 are:
                // columns[4], columns[5]
                // but let's do columns[columns.Length - 2] & columns[columns.Length - 1]
                string correctedStr = columns[columns.Length - 2].Trim();
                string testedStr = columns[columns.Length - 1].Trim();

                int.TryParse(correctedStr, out correctedVal);
                int.TryParse(testedStr, out testedVal);
            }

            // Create a PlayerData struct
            PlayerData data = new PlayerData()
            {
                username = username,
                finalTime = finalTimeVal,
                correctedCount = correctedVal,
                testedCount = testedVal
            };

            result.Add(data);
        }

        return result;
    }

    /// <summary>
    /// Convert finalTime (float seconds) to "MM:SS" format for display.
    /// </summary>
    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

/// <summary>
/// A container for the CSV data we care about: username, finalTime, correctedCount, testedCount.
/// </summary>
public struct PlayerData
{
    public string username;
    public float finalTime;
    public int correctedCount;
    public int testedCount;
}