using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    public float speed;         
    public float destroyDelay;

    void Start()
    {
       
        Destroy(gameObject, destroyDelay);
    }

    void Update()
    {
       
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
}
