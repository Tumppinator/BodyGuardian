using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy1 : MonoBehaviour
{

    NavMeshAgent enemy;
    GameObject player;

    private void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        enemy.destination = player.transform.position;
    }

}
