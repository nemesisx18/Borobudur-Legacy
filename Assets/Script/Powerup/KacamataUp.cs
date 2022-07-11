using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KacamataUp : MonoBehaviour
{
    public GameObject kacamata;
    public GameObject[] box;
    public GameObject[] boxKosong;
    //public SpriteRenderer[] spriteImg;
    public float waitTime;
    public bool isDone;

    private void Start()
    {
        Debug.Log("item aktif!!!!");
    }

    private void Update()
    {
        box = GameObject.FindGameObjectsWithTag("Box");
        boxKosong = GameObject.FindGameObjectsWithTag("Kotak");

        if (!isDone)
        {
            StartCoroutine(TransparentImg());
        }
        else
        {
            kacamata.SetActive(false);
        }
    }

    IEnumerator TransparentImg()
    {
        for (int i = 0; i < box.Length; i++)
        {
            box[i].GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.1f);
            boxKosong[i].GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.1f);
        }

        gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);

        yield return new WaitForSeconds(waitTime);
        Debug.Log("trans selesai");
        
        for (int i = 0; i < box.Length; i++)
        {
            box[i].GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            boxKosong[i].GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        isDone = true;
    }
}
