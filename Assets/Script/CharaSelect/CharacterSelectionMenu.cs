using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionMenu : MonoBehaviour
{
    public Button button1, button2, button3;

    [Header("Chara UI")]
    public GameObject blankIMG;
    public GameObject charaSelectionCanvas;
    public GameObject[] playerObjects;

    public int selectedCharacter = 0;

    private string selectedCharacterDataName = "SelectedCharacter";


    private void HideAllCharacter()
    {
        foreach (GameObject g in playerObjects)
        {
            g.SetActive(false);
        }
    }
 
    public void JOHN0()
    {
        selectedCharacter = 0;

        HideAllCharacter();

        PlayerPrefs.SetInt(selectedCharacterDataName, selectedCharacter);
        Time.timeScale = 1;
        FrManager.fmInstance.isPaused = false;
        FrManager.fmInstance.justSpawn = true;
        StartCoroutine(BlankOnce());

        playerObjects[selectedCharacter].SetActive(true);
    }

    public void AYOU()
    {
        selectedCharacter = 1;

        HideAllCharacter();

        PlayerPrefs.SetInt(selectedCharacterDataName, selectedCharacter);
        Time.timeScale = 1;
        FrManager.fmInstance.isPaused = false;
        FrManager.fmInstance.justSpawn = true;
        StartCoroutine(BlankOnce());

        playerObjects[selectedCharacter].SetActive(true);
    }

    public void BEEMO()
    {
        selectedCharacter = 2;

        HideAllCharacter();

        PlayerPrefs.SetInt(selectedCharacterDataName, selectedCharacter);
        Time.timeScale = 1;
        FrManager.fmInstance.isPaused = false;
        FrManager.fmInstance.justSpawn = true;
        StartCoroutine(BlankOnce());

        playerObjects[selectedCharacter].SetActive(true);
    }
        

    public void StartGame()
    {
        Time.timeScale = 1;
        FrManager.fmInstance.isPaused = false;
        charaSelectionCanvas.SetActive(false);
    }

    IEnumerator BlankOnce()
    {
        blankIMG.SetActive(true);
        charaSelectionCanvas.SetActive(false);

        yield return new WaitForSecondsRealtime(0.5f);
        blankIMG.SetActive(false);

    }

    void Start()
    {
        HideAllCharacter();

        selectedCharacter = PlayerPrefs.GetInt(selectedCharacterDataName, 0);

        playerObjects[selectedCharacter].SetActive(true);
    }

    void Update()
    {
        if(selectedCharacter == 0)
        {
            button1.interactable = false;
        }
        else
        {
            button1.interactable = true;
        }

        if (selectedCharacter == 1)
        {
            button2.interactable = false;
        }
        else
        {
            button2.interactable = true;
        }

        if (selectedCharacter == 2)
        {
            button3.interactable = false;
        }
        else
        {
            button3.interactable = true;
        }
    }
}
