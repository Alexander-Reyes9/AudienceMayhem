using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SwitchScreen : MonoBehaviour
{
   void Start()
    {

    }
    public void playerScene()
    {
        SceneManager.LoadScene("CreateRoomScene");
    }

    public void audienceScreen()
    {
        SceneManager.LoadScene("JoinScene");
    }
}
