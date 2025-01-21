using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButtonHandler : MonoBehaviour
{
    [Header("References")]
    public Button audioButton;        // The UI button
    public AudioSource audioSource;   // The AudioSource component
    public AudioClip audioClip;       // The AudioClip you want to play

    private void Start()
    {
        // Assign the callback to the Button’s onClick event
        if (audioButton != null)
        {
            audioButton.onClick.AddListener(PlayAudioClip);
        }
    }

    private void PlayAudioClip()
    {
        if (audioSource != null && audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioButtonHandler: Missing AudioSource or AudioClip!");
        }
    }
}
