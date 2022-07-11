using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveArrow : MonoBehaviour
{
    public float speed;
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(this.gameObject);
    }
}
