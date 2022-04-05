using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMage : MonoBehaviour
{
    [SerializeField] private int max_hp; 
    [SerializeField] private int max_ap; 
    [SerializeField] private int max_mp; 

    private int current_hp; // When zero, the player dies
    private int current_ap; // When zero, no jumping or running is allowed
    private int current_mp; // When zero, no abilities are available

    private void Start()
    {
        // Sets values to max at beginning
        current_hp = max_hp;
        current_ap = max_ap;
        current_mp = max_mp;
    }
}
