using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuToTutorial : MonoBehaviour
{
    public void GoToTutorialScene()
    {
        SceneManager.LoadScene("TutorialScene");
    }
}