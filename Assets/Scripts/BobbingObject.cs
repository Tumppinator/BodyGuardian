using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbingObject : MonoBehaviour
{
    float originalY;
    float floatStrength = 10f;

    void Start()
    {
        this.originalY = this.transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x,
            originalY + ((float)Mathf.Sin(Time.time * 8.0f) * floatStrength),
            transform.position.z);
    }
}
