using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void BeginButtonClicked()
    {
        SceneManager.LoadScene("DecisionScene");
    }

    public void HelpButtonClicked()
    {
        SceneManager.LoadScene("HelpScene");
       // Debug.Log("Switch to help scene.");
    }
}
