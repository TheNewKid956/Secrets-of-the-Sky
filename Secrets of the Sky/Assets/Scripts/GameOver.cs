using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject ui;

    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void Retry()
    {
        Toggle();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerStats.health = 1;
        PlayerStats.canMove = true;
        PlayerStats.canRotate = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerStats.timesChecked = 0;

    }

    public void Menu()
    {
        Debug.Log("Go To Menu");
    }
}
