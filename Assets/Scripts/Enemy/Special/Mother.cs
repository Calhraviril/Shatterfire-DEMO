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
    [SerializeField] private GameObject kilject;


}
