using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy1 : MonoBehaviour
{

    NavMeshAgent enemy;
    GameObject player;
    [SerializeField] ParticleSystem explosion;

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

    private void OnDestroy()
    {
        var explosionEffect = Instantiate(explosion, new Vector3(transform.position.x, transform.position.y + 0.7f, transform.position.z), transform.rotation);
        explosionEffect.Play();
    }
}
