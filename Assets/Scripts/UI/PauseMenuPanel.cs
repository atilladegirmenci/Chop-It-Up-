using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuPanel : MonoBehaviour
{
   public void Resume()
    {
        gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("resume clicked");

    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void NewGame()
    {
        GameManager.instance.NewGame();
    }
}
