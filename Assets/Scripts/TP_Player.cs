using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TP_Player : MonoBehaviour
{
    [SerializeField] float movementMultiplier = 2f;
    [SerializeField] float radius = 17f;
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] float yMovementMultiplier = 7f;
    [SerializeField] float yMovementMin = 1f;
    [SerializeField] float yMovementMax = 3f;
    [SerializeField] float yMovementOffset = 1f;

    Vector3 cameraPos;
    Vector3 centerPosition;
    float originalZpos;
    float distance = 0f;
    float finalXpos;
    float finalZpos;
    float finalYpos;

    Level level;
    Score score;


    // Start is called before the first frame update
    void Start()
    {
        originalZpos = transform.position.z;
        centerPosition = transform.position;
        level = FindObjectOfType<Level>();
        score = FindObjectOfType<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        cameraPos = Camera.main.transform.position;
        finalXpos = cameraPos.x * movementMultiplier;
        finalZpos = cameraPos.z * movementMultiplier + originalZpos;
        finalYpos = cameraPos.y * yMovementMultiplier + yMovementOffset;
        float clampedYpos = Mathf.Clamp(finalYpos, yMovementMin, yMovementMax);

        distance = Vector3.Distance(centerPosition, transform.position);

        if(distance > radius)
        {
            Vector3 fromOriginToObject = transform.position - centerPosition;
            fromOriginToObject *= radius / distance;
            transform.position = centerPosition + fromOriginToObject;
        }
        else
        {
            transform.position = new Vector3(finalXpos, clampedYpos, finalZpos);
        }

        transform.Rotate(new Vector3(0f, rotationSpeed, 0f) * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            PlayerPrefs.SetInt("Score", score.GetScore());
            if (PlayerPrefs.HasKey("HighScore"))
            {
                if (PlayerPrefs.GetInt("Score") > PlayerPrefs.GetInt("HighScore", 0))
                {
                    PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("Score"));
                }
            }
            level.LoadGameOver();
        }
    }
}
