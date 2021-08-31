using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Oscillator : MonoBehaviour
{
    float timeCounter = 0;

    float startX = 0;
    float startY = 0;
    float startZ = 0;

    public float speed = 4f;
    public float radius = 1f; 

    // Start is called before the first frame update
    void Start()
    {
        startX = transform.position[0];
        startY = transform.position[1];
        startZ = transform.position[2];
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime*speed;
        timeCounter = timeCounter%(2*Mathf.PI);
        float x = Mathf.Cos(timeCounter)*radius;
        float y = 0;
        float z = Mathf.Sin(timeCounter)*radius;
        transform.position = new Vector3(startX+x, startY+y, startZ+z); 
    }
}
