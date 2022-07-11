using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public Animation anim;
    public float delayTime;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Go());
    }

    IEnumerator Go()
    {
        while (true)
        {
            anim.Play();
            yield return new WaitForSeconds(5f);
        }
    }
}
