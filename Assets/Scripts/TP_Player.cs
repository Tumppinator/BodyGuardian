using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TP_Player : MonoBehaviour
{
    [SerializeField] float movementMultiplier = 2f;

    Vector3 cameraPos;
    float originalZpos;


    // Start is called before the first frame update
    void Start()
    {
        originalZpos = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        cameraPos = Camera.main.transform.position;
        float finalXpos = cameraPos.x * movementMultiplier;
        float finalZpos = cameraPos.z * movementMultiplier + originalZpos;

        transform.position = new Vector3(finalXpos, transform.position.y, finalZpos);
    }
}
