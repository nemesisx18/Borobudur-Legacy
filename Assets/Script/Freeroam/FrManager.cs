using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrManager : MonoBehaviour
{
    public static FrManager fmInstance;

    [Header("Freeroam UI")]
    public bool isPaused;
    public bool justSpawn;

    public GameObject transitionUI;

    private void Awake()
    {
        if (fmInstance == null)
        {
            fmInstance = this;
        }
        else
        {
            if (fmInstance != this)
            {
                //Destroy(gameObject);
            }
        }
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        transitionUI.SetActive(true);

        yield return new WaitForSecondsRealtime(2f);
        transitionUI.SetActive(false);
    }
}
