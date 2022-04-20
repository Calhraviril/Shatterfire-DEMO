using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMage : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int max_hp; 
    [SerializeField] private int max_ap; 
    [SerializeField] private int max_mp;
    [SerializeField] private int defence;

    [Header("Statistic")]
    [SerializeField] private Image statBarLife;
    [SerializeField] private float lerpSpeed;

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
    private void Update()
    {
        if (current_hp != statBarLife.fillAmount)
        {
            statBarLife.fillAmount = Mathf.Lerp(statBarLife.fillAmount, current_hp / max_hp, Time.deltaTime * lerpSpeed);
        }
    }

    public void PlayerDamager(int dmg)
    {
        current_hp -= (dmg - defence);
    }
}
