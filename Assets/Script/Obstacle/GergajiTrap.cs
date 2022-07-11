using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GergajiTrap : MonoBehaviour
{
    public GameObject gergaji;
    public float interval;
    public float spawnCount = 1;

    private List<GameObject> myObjects = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        StartCoroutine(GergajiAktif());
    }
    public IEnumerator GergajiAktif()
    {
        WaitForSeconds delay = new WaitForSeconds(interval);
        while (true)
        {
            float waitForNext = 2f / spawnCount;
            for (int i = 0; i < spawnCount; i++)
            {
                GameObject prefab = gergaji;
                prefab.SetActive(true);

                //Add the object to the list
                myObjects.Add(prefab);

                yield return new WaitForSeconds(waitForNext);
            }

            gergaji.SetActive(false);
            yield return delay;
        }
    }
}
