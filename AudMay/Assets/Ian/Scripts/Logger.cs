using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger : MonoBehaviour
{
    public static string document;

    private string debugPanel;
    public static void Log (string msg)
    {
        document = msg + "\n" + document;
    }

    private void OnGUI()
    {
        debugPanel = GUI.TextArea(new Rect(0, 0, Screen.width - Screen.width / 2, Screen.height - Screen.height / 2), document);
    }
}
