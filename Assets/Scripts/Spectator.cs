using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectator : MonoBehaviour
{
    GameObject player;
    TP_Player TP_Player;
    BobbingObject bobbingObject;
    AudioSource audioSource;
    bool playAudio = false;
    bool oneTime = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        TP_Player = FindObjectOfType<TP_Player>();
        bobbingObject = FindObjectOfType<BobbingObject>();
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = (Random.Range(0.6f, 0.9f));
    }

    // Update is called once per frame
    void Update()
    {
        if(!TP_Player.GetGameOver())
        {
            this.transform.LookAt(player.transform);
        }
        else
        {
            this.transform.LookAt(bobbingObject.transform);
            playAudio = true;
        }

        if(playAudio && !oneTime)
        {
            audioSource.Play();
            playAudio = false;
            oneTime = true;
        }
    }
}
