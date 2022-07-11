using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFreeroam : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float smoothSpeed;
    [SerializeField] private float minX, maxX, minY, maxY;

    private void LateUpdate()
    {
        if (target != null)
        {
            //transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), smoothSpeed * Time.deltaTime);

            target.transform.position = Vector3.Lerp(target.transform.position, new Vector3(transform.position.x, transform.position.y, target.transform.position.z), smoothSpeed * Time.deltaTime);

            //limit camera range
            target.transform.position = new Vector3(Mathf.Clamp(target.transform.position.x, minX, maxX),
                                             Mathf.Clamp(target.transform.position.y, minY, maxY),
                                             target.transform.position.z
                                             );
        }
        else
        {
            Debug.Log("No player detected");
        }
    }
}
