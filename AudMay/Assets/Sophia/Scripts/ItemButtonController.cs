using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButtonController : MonoBehaviour
{
    private bool click;
    public GameObject canvasText;

    public void buttonClick()
    {
        if (canvasText.gameObject.activeSelf)
        {
            canvasText.gameObject.SetActive(false);
        }
        else
        {
            canvasText.gameObject.SetActive(true);
        }
    }
}
