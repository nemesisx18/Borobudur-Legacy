using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public VideoPlayer vp;
    public float timer;
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        timer = Mathf.FloorToInt((float)vp.length);

        yield return new WaitForSecondsRealtime(timer + 1f);
        SceneManager.LoadScene("MainMenu");
    }
}
