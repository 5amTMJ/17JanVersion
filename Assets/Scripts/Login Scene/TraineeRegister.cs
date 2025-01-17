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
                usernames.Add(line.Trim());
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

}
