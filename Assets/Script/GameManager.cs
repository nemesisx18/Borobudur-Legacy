using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI Game")]
    public GameObject pauseCanvas;
    public GameObject sceneTran;

    [Header("Player Selection")]
    public GameObject[] playerObjects;
    public int selectedCharacter;

    private string selectedCharacterDataName = "SelectedCharacter";

    private void Awake()
    {
        HideAllCharacter();
        selectedCharacter = PlayerPrefs.GetInt(selectedCharacterDataName, 0);

        playerObjects[selectedCharacter].SetActive(true);
    }

    public IEnumerator Start()
    {
        Time.timeScale = 1;
        sceneTran.SetActive(true);

        yield return new WaitForSecondsRealtime(2f);
        sceneTran.SetActive(false);
    }

    private void HideAllCharacter()
    {
        foreach (GameObject g in playerObjects)
        {
            g.SetActive(false);
        }
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            if (!PlayerController.instance.isPause)
            {
                PauseGame();
            }
        }
    }

    public void EnableCursor()
    {
        Cursor.visible = true;
    }

    public void DisableCursor()
    {
        Cursor.visible = false;
    }

    public void RetryGame1()
    {
        SceneManager.LoadScene("Stage1");
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    public void RetryGame2()
    {
        SceneManager.LoadScene("Stage2");
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    public void RetryGame3()
    {
        SceneManager.LoadScene("Stage3");
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    public void NextLevel2()
    {
        SceneManager.LoadScene("Stage2");
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    public void NextLevel3()
    {
        SceneManager.LoadScene("Stage3");
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
        Cursor.visible = true;
    }

    public void FreeroamLoad()
    {
        SceneManager.LoadScene("Freeroam");
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    public void EndGame()
    {
        SceneManager.LoadScene("Freeroam");
        Time.timeScale = 1;
        Cursor.visible = false;
        PlayerPrefs.SetString("EndGame", "level selesai");
        PlayerPrefs.Save();
    }

    public void PauseGame()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0;
        PlayerController.instance.isPause = true;
        Cursor.visible = true;
    }

    public void UnpauseGame()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
        PlayerController.instance.isPause = false;
        Cursor.visible = false;
    }
}
