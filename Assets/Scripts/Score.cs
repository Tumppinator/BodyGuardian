using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Score : MonoBehaviour
{

    private static bool startGame = false;
    private static int score = 0;
    private float time = 0;

    private void Start()
    {
        startGame = false;
        if(!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }
    }

    private void Update()
    {
        if(GetStartGame())
        {
            time += Time.deltaTime;
        }
    }

    public void SetStartGame()
    {
        startGame = true;
    }

    public bool GetStartGame()
    {
        return startGame;
    }

    public void UpdateScore()
    {
        score -= (int)(time * 2.5f);
        time = 0;
        score += 100;
        if(score < 0)
        {
            score = 0;
        }
        PlayerPrefs.SetInt("Score", score);
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetScore()
    {
        PlayerPrefs.SetInt("Score", 0);
        score = 0;
    }

}
