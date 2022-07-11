using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batuSpesial : MonoBehaviour
{
    public GameObject[] batu;
    public float interval;
    public float spawnCount;
    public float spawnQty;

    private List<GameObject> myObjects = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        StartCoroutine(SpawnArrow());
    }
    public IEnumerator SpawnArrow()
    {
        WaitForSeconds delay = new WaitForSeconds(interval);
        while (true)
        {
            if (spawnQty != 3)
            {
                float waitForNext = 2f / spawnCount;
                for (int i = 0; i < spawnCount; i++)
                {
                    GameObject prefab = batu[UnityEngine.Random.Range(0, batu.Length)];
                    GameObject clone = Instantiate(prefab, new Vector2(transform.position.x, transform.position.y), Quaternion.Euler(0, 0, 0));

                    //Add the object to the list
                    myObjects.Add(clone);
                    spawnQty += 1;

                    yield return new WaitForSeconds(waitForNext);
                }
            }
            else
            {
                this.gameObject.SetActive(false);
            }

            yield return delay;
        }
    }
}
