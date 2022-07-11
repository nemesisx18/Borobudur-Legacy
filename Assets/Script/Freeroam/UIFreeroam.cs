using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIFreeroam : MonoBehaviour
{
    public GameObject basement;
    public GameObject arsipLibrary;
    public GameObject mainCanvas;

    [Header("Relief Info")]
    public string Stage1;
    public string Stage2;
    public string Stage3;

    public Sprite lockDefault;
    public Sprite gajah;
    public Sprite pohon;
    public Sprite singa;
    public Sprite tutorAwal;

    public GameObject reliefGajah;
    public GameObject reliefPohon;
    public GameObject reliefSinga;

    private void Start()
    {
        if (PlayerPrefs.HasKey("key"))
        {
            mainCanvas.SetActive(false);
        }
        else
        {
            PlayerPrefs.SetInt("key", 0);
            PlayerPrefs.Save();

            StartCoroutine(DisableTutor());
        }
    }

    IEnumerator DisableTutor()
    {
        mainCanvas.SetActive(true);
        mainCanvas.GetComponent<Image>().sprite = tutorAwal;
        yield return new WaitForSecondsRealtime(6f);
        mainCanvas.SetActive(false);
    }

    private void Update()
    {
        if(PlayerPrefs.GetString("Gajah") == Stage1)
        {
            reliefGajah.GetComponent<Image>().sprite = gajah;
            reliefGajah.GetComponent<Button>().interactable = true;
        }
        else
        {
            reliefGajah.GetComponent<Image>().sprite = lockDefault;
            reliefGajah.GetComponent<Button>().interactable = false;
        }

        if (PlayerPrefs.GetString("Pohon") == Stage2)
        {
            reliefPohon.GetComponent<Image>().sprite = pohon;
            reliefPohon.GetComponent<Button>().interactable = true;
        }
        else
        {
            reliefPohon.GetComponent<Image>().sprite = lockDefault;
            reliefPohon.GetComponent<Button>().interactable = false;
        }

        if (PlayerPrefs.GetString("Singa") == Stage3)
        {
            reliefSinga.GetComponent<Image>().sprite = singa;
            reliefSinga.GetComponent<Button>().interactable = true;
        }
        else
        {
            reliefSinga.GetComponent<Image>().sprite = lockDefault;
            reliefSinga.GetComponent<Button>().interactable = false;
        }
    }

    public void ExitBasement()
    {
        basement.SetActive(false);
        Time.timeScale = 1;
        FrManager.fmInstance.isPaused = false;
    }

    public void ExitLibrary()
    {
        arsipLibrary.GetComponent<Animator>().Play("closeLibrary");

        StartCoroutine(FadeLibrary());
    }

    IEnumerator FadeLibrary()
    {
        yield return new WaitForSecondsRealtime(1.2f);
        Debug.Log("exitt");

        arsipLibrary.SetActive(false);
        Time.timeScale = 1;
        FrManager.fmInstance.isPaused = false;
    }
}
