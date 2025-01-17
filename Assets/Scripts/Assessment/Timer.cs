using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float initialTime = 60f;
    private float currentTime;
    public bool isRunning = false;
    public GameObject playerObject;
    public TextMeshProUGUI gameOverText;

    void Start()
    {
        currentTime = initialTime;
        UpdateTimeDisplay();
        HideGameOverText();
    }

    public void StartTimer()
    {
        isRunning = true;
        currentTime = initialTime;
    }

    void Update()
    {
        if(isRunning)
        {
            currentTime -= Time.deltaTime;
            if(currentTime<=0)
            {
                currentTime = 0;
                isRunning = false;
                OnTimerFinished();
            }
            UpdateTimeDisplay();
        }
    }

    void UpdateTimeDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void OnTimerFinished()
    {
        Debug.Log("Time's up!");
        StopPlayerMovement();
        ShowGameOverText();
        FreezeVRView();
    }

    void StopPlayerMovement()
    {
        if (playerObject!=null)
        {
            Rigidbody rb = playerObject.GetComponent<Rigidbody>();
            if (rb!=null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }

    void ShowGameOverText()
    {
        if(gameOverText!=null)
        {
            gameOverText.gameObject.SetActive(true);
        }
    }

    void HideGameOverText()
    {
        if (gameOverText!=null)
        {
            gameOverText.gameObject.SetActive(false);
        }
    }

    public void ResetTimer()
    {
        isRunning = false;
        currentTime = initialTime;
        HideGameOverText();
    }

    void FreezeVRView()
    {
        var playerMovement = playerObject.GetComponent<MonoBehaviour>();
        if(playerMovement!=null)
        {
            playerMovement.enabled = false;
        }
    }
}
