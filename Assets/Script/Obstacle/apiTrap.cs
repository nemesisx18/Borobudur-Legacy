using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class apiTrap : MonoBehaviour
{
    public GameObject apiChild;
    public float interval;
    public float spawnCount = 3;

    private List<GameObject> myObjects = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        StartCoroutine(ApiAktif());
    }
    public IEnumerator ApiAktif()
    {
        WaitForSeconds delay = new WaitForSeconds(interval);
        while (true)
        {
            float waitForNext = 2f / spawnCount;
            for (int i = 0; i < spawnCount; i++)
            {
                GameObject prefab = apiChild;
                prefab.SetActive(true);

                //Add the object to the list
                myObjects.Add(prefab);

                yield return new WaitForSeconds(waitForNext);
            }

            apiChild.SetActive(false);
            yield return delay;
        }
    }
}
