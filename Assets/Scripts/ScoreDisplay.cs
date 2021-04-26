using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ScoreDisplay : MonoBehaviour
{
    TextMeshProUGUI Scoretext;
    Score score;

    private void Start()
    {
        Scoretext = GetComponent<TextMeshProUGUI>();
        score = FindObjectOfType<Score>();
    }

    private void Update()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            Scoretext.text = "Score: " + PlayerPrefs.GetInt("Score", 0);
        }
    }
}
