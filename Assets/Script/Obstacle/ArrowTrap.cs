
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    public GameObject[] arrows;
    public float interval;
    public float spawnCount = 3;

    [Header("Arrow direction")]
    public float arrowDir; //arrow rotation
    public float arrowPlace; //jarak spawn panah                       

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
            float waitForNext = 2f / spawnCount;
            for (int i = 0; i < spawnCount; i++)
            {
                GameObject prefab = arrows[UnityEngine.Random.Range(0, arrows.Length)];
                GameObject clone = Instantiate(prefab, new Vector2(transform.position.x + arrowPlace, transform.position.y), Quaternion.Euler(0, arrowDir, 0));

                //Add the object to the list
                myObjects.Add(clone);

                yield return new WaitForSeconds(waitForNext);
            }
            
            yield return delay;
        }
    }
}
