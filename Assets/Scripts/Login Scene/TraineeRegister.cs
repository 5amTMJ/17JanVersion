using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TraineeRegister : MonoBehaviour
{
    public TMP_InputField usernameInputField;
    public Button enterButton;
    public Button createAccountButton;
    public Button quitButton;
    public TMP_Text messageText;
    public static string currentUsername;  // Store the username of the active session

    private List<string> usernames = new List<string>();
    private string filePath;

    void Start()
    {
        filePath = Application.dataPath + "/TraineeAccounts.csv";
        LoadUserData();

        enterButton.onClick.AddListener(HandleEnterButton);
        createAccountButton.onClick.AddListener(HandleCreateAccountButton);
        quitButton.onClick.AddListener(QuitApplication);
    }

    public void HandleEnterButton()
    {
        string username = usernameInputField.text.Trim();

        if(string.IsNullOrEmpty(username))
        {
            DisplayMessage("Please enter a username.");
            return;
        }

        if (usernames.Contains(username))
        {
            // Store the user name for later
            currentUsername = username;

            DisplayMessage($"Welcome, {username}!");
            SceneManager.LoadScene("MainMenu");

        }
        else
        {
            DisplayMessage("Account does not exist.");
        }
    }

    public void HandleCreateAccountButton()
    {
        string username = usernameInputField.text.Trim();

        if (string.IsNullOrEmpty(username))
        {
            DisplayMessage("Please enter a username.");
            return;
        }

        if (usernames.Contains(username))
        {
            DisplayMessage("Account already exists.");
            return;
        }

        usernames.Add(username);
        SaveUserData();
        DisplayMessage("Registration successful.");
    }

    public void QuitApplication()
    {
        Application.Quit();
        Debug.Log("Application Quit");
    }

    public void LoadUserData()
    {
        if (!File.Exists(filePath))
        {
            Debug.Log("User data file not found. Creating a new file.");
            File.Create(filePath).Dispose();
            return;
        }

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                // Split on comma to extract username
                string[] columns = line.Split(',');
                if (columns.Length > 0)
                {
                    // The first column is the username
                    string firstColumn = columns[0].Trim();
                    if (!string.IsNullOrEmpty(firstColumn))
                    {
                        usernames.Add(firstColumn);
                    }
                }
            }
        }

        Debug.Log("User data loaded. Total users: " + usernames.Count);
    }

    public void SaveUserData()
    {
        using (StreamWriter writer = new StreamWriter(filePath, false))
        {
            foreach (string username in usernames)
            {
                writer.WriteLine(username);
            }
        }
        Debug.Log("User data saved to " + filePath);
    }

    public void DisplayMessage(string message)
    {
        messageText.text = message;
        StartCoroutine(ClearMessageAfterDelay(5f));
    }

    private System.Collections.IEnumerator ClearMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        messageText.text = "";
    }

    public static void UpdateTraineeRow(
    string username,
    float finalTime,
    string antennaStatus,
    string cableStatus,
    string rightCornerStatus,
    string leftCornerStatus,
    string leftLadderStatus,
    string rightLadderStatus,
    string groundingRodStatus,
    string fireExtinguisherStatus,
    string fodStatus,
    int correctedCount,
    int testedCount
)
    {
        // 1) Read CSV lines
        string filePath = Application.dataPath + "/TraineeAccounts.csv";
        if (!File.Exists(filePath))
        {
            // If file doesn't exist yet, create it
            File.Create(filePath).Dispose();
        }

        List<string> lines = new List<string>(File.ReadAllLines(filePath));
        bool foundUser = false;

        // 2) Loop through lines, find user row
        for (int i = 0; i < lines.Count; i++)
        {
            // Split by commas
            string[] columns = lines[i].Split(',');
            if (columns.Length > 0 && columns[0].Trim().Equals(username, System.StringComparison.OrdinalIgnoreCase))
            {
                // 3) Overwrite line with new data
                lines[i] = GenerateCsvLine(
                    username, finalTime, antennaStatus, cableStatus, rightCornerStatus,
                    leftCornerStatus, leftLadderStatus, rightLadderStatus,
                    groundingRodStatus, fireExtinguisherStatus, fodStatus,
                    correctedCount, testedCount
                );
                foundUser = true;
                break;
            }
        }

        // 4) If user row not found, append new line
        if (!foundUser)
        {
            lines.Add(GenerateCsvLine(
                username, finalTime, antennaStatus, cableStatus, rightCornerStatus,
                leftCornerStatus, leftLadderStatus, rightLadderStatus,
                groundingRodStatus, fireExtinguisherStatus, fodStatus,
                correctedCount, testedCount
            ));
        }

        // 5) Write lines back to file
        File.WriteAllLines(filePath, lines.ToArray());
    }

    // Helper method
    private static string GenerateCsvLine(
        string username,
        float finalTime,
        string antennaStatus,
        string cableStatus,
        string rightCornerStatus,
        string leftCornerStatus,
        string leftLadderStatus,
        string rightLadderStatus,
        string groundingRodStatus,
        string fireExtinguisherStatus,
        string fodStatus,
        int correctedCount,
        int testedCount
    )
    {
        // Compose each column in the order you desire:
        //  0: username
        //  1: finalTime
        //  2..: statuses
        // last: correctedCount and testedCount
        return string.Join(",", new string[]
        {
        username,
        finalTime.ToString("F0"), // or "F2" if you want decimals
        antennaStatus,
        cableStatus,
        rightCornerStatus,
        leftCornerStatus,
        leftLadderStatus,
        rightLadderStatus,
        groundingRodStatus,
        fireExtinguisherStatus,
        fodStatus,
        correctedCount.ToString(),
        testedCount.ToString()
        });
    }

}
