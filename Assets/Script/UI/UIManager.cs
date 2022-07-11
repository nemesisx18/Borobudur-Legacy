using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Health Bar")]
    public GameObject healthUI;
    public List<GameObject> healthList = new List<GameObject>();
    public List<Health> darah = new List<Health>();
    public Transform transUI;

    private int healthNow;

    void Start()
    {
        for (int i=0; i < PlayerController.instance.playerHealth; i++)
        {
            GameObject go = Instantiate(healthUI, transUI);
            healthList.Add(go);
            darah.Add(go.GetComponent<Health>());
            healthNow += 1;
        }
    }

    void Update()
    {
        if (healthNow != PlayerController.instance.playerHealth)
        {
            healthNow = PlayerController.instance.playerHealth;
            for (int i = 0; i < darah.Count; i++)
            {
                darah[i].darahIMG.SetActive(false);
            }
            for (int i = 0; i < PlayerController.instance.playerHealth; i++)
            {
                darah[i].darahIMG.SetActive(true);
            }
        }
    }
}
