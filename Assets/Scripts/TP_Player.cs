using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TP_Player : MonoBehaviour
{
    [SerializeField] float movementMultiplier = 2f;
    [SerializeField] float radius = 17f;
    [SerializeField] float rotationSpeed = 100f;

    Vector3 cameraPos;
    Vector3 centerPosition;
    float originalZpos;
    float distance = 0f;
    float finalXpos;
    float finalZpos;

    Level level;


    // Start is called before the first frame update
    void Start()
    {
        originalZpos = transform.position.z;
        centerPosition = transform.position;
        level = FindObjectOfType<Level>();
    }

    // Update is called once per frame
    void Update()
    {
        cameraPos = Camera.main.transform.position;
        finalXpos = cameraPos.x * movementMultiplier;
        finalZpos = cameraPos.z * movementMultiplier + originalZpos;

        distance = Vector3.Distance(centerPosition, transform.position);

        if(distance > radius)
        {
            Vector3 fromOriginToObject = transform.position - centerPosition;
            fromOriginToObject *= radius / distance;
            transform.position = centerPosition + fromOriginToObject;
        }
        else
        {
            transform.position = new Vector3(finalXpos, transform.position.y, finalZpos);
        }

        transform.Rotate(new Vector3(0f, rotationSpeed, 0f) * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            level.LoadMainMenu();
        }
    }
}
