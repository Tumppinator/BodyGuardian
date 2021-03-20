using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{

    private static bool startGame = false;

  
    public void SetStartGame()
    {
        startGame = true;
    }

    public bool GetStartGame()
    {
        return startGame;
    }

}
