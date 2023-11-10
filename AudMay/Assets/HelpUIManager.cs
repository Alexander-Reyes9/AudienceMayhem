using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpUIManager : MonoBehaviour
{
    public void BackButtonPressed()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
