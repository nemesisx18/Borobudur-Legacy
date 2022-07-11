using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public GameObject loadBG;
    public GameObject controlUI;
    public GameObject settingUI;
    public GameObject sceneTransition;

    public AudioSource audioMain;
    public AudioSource bgmSource;
    public AudioClip click;

    IEnumerator Start()
    {
        Cursor.visible = true;

        sceneTransition.SetActive(true);

        yield return new WaitForSecondsRealtime(1.2f);
        sceneTransition.SetActive(false);
    }

    public void PlayGame()
    {
        audioMain.PlayOneShot(click);
        loadBG.SetActive(true);
        StartCoroutine(AudioFade.StartFade(bgmSource, 2.5f, 0.001f));
    }

    public void OpenControl()
    {
        controlUI.SetActive(true);
        audioMain.PlayOneShot(click);
    }

    public void OpenSetting()
    {
        settingUI.SetActive(true);
        audioMain.PlayOneShot(click);
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("App quit");
    }
}
