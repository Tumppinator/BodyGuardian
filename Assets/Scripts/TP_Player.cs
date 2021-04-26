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
    [SerializeField] ParticleSystem explosionVFX;
    [SerializeField] AudioClip explosionClip;
    [SerializeField] AudioClip laughClip;

    AudioSource audioSource;
    Vector3 cameraPos;
    Vector3 centerPosition;
    float originalZpos;
    float distance = 0f;
    float finalXpos;
    float finalZpos;
    float finalYpos;
    bool gameOver = false;

    Level level;
    Score score;


    // Start is called before the first frame update
    void Start()
    {
        originalZpos = transform.position.z;
        centerPosition = transform.position;
        level = FindObjectOfType<Level>();
        score = FindObjectOfType<Score>();
        audioSource = GetComponent<AudioSource>();
        PlayerPrefs.SetInt("Score", 0);
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

    public bool GetGameOver()
    {
        return gameOver;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && !gameOver)
        {
            gameOver = true;
            if (PlayerPrefs.HasKey("HighScore"))
            {
                if (PlayerPrefs.GetInt("Score") > PlayerPrefs.GetInt("HighScore", 0))
                {
                    PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("Score"));
                }
            }
            StartCoroutine(AfterCollision());
        }
    }

    private IEnumerator AfterCollision()
    {
        var explosionEffect = Instantiate(explosionVFX, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        explosionEffect.Play();
        audioSource.clip = explosionClip;
        audioSource.Play();

        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(3.0f);
        level.LoadGameOver();
    }
}
