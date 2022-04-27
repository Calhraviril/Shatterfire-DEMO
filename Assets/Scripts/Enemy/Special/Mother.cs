using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mother : MonoBehaviour
{
    // This script is used to detect whether or not mother has been given her cake, NOW
    // Also used to give Mother her special attack

    [Header("Spawnables")]
    [SerializeField] private GameObject easiest;

    [Header("Other")]
    [SerializeField] private float spawnRate;
    [SerializeField] private int maxSpawn;
    [SerializeField] private int spawnedPerWave;
    [SerializeField] private int burstSpawnWave;
    private float timer;
    private int spawned;

    private void Start()
    {
        timer = spawnRate + Time.time;
    }
    private void Update()
    {
        if (Time.time > timer && spawned < maxSpawn)
        {
            Spawner(spawnedPerWave);
            spawned += spawnedPerWave;
            timer = Time.time + spawnRate;
        }
    }

    private Vector3 RandPos()
    {
        float xRand = Random.Range(-2, 2) + transform.position.x;
        float zRand = Random.Range(-2, 2) + transform.position.z;
        Vector3 finPos = new Vector3(xRand, transform.position.y, zRand);
        return finPos;
    }
    private void Spawner(int toSpawn)
    {
        for (int i = 0; i < toSpawn; i++)
        {
            Instantiate(easiest, RandPos(), easiest.transform.rotation);
        }
    }
}
