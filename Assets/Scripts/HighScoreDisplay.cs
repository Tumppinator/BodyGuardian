using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class HighScoreDisplay : MonoBehaviour
{
    TextMeshProUGUI HighScoretext;

    private void Start()
    {
        HighScoretext = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            HighScoretext.text = "High Score : " + PlayerPrefs.GetInt("HighScore", 0);
        }
    }
}
