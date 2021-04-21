using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class StartGame : MonoBehaviour
{
    private static bool startGame = false;

    private void Start()
    {
        startGame = false;
    }

    public void SetStartGame()
    {
        startGame = true;
    }

    public bool GetStartGame()
    {
        return startGame;
    }
}
