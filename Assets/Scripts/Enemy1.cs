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

    public void SpawnExplosionEffect()
    {
        var explosionEffect = Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        explosionEffect.Play();
    }
}
