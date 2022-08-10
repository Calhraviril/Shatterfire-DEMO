using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMage : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int max_hp; 
    [SerializeField] private int max_ap; 
    [SerializeField] private int max_mp;
    [SerializeField] private int defence;
    [SerializeField] private float immunityFrames; // So that you cant be oneshotted by rabid attacks from enemies

    [Header("Statistic")]
    [SerializeField] private Image statBarLife;
    [SerializeField] private TMP_Text statTextLife;
    [SerializeField] private TMP_Text namedBar;
    [SerializeField] private float lerpSpeed;

    private float current_hp; // When zero, the player dies
    private float current_ap; // When zero, no jumping or running is allowed
    private float current_mp; // When zero, no abilities are available
    private float framesafe; // Used to store the value of how long immunity frames are still active
    public float HP { get => current_hp; }

    private void Start()
    {
        // Sets values to max at beginning
        current_hp = max_hp;
        current_ap = max_ap;
        current_mp = max_mp;
        namedBar.text = Login.nickname;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            current_hp = 0;
        }
        if (current_hp != statBarLife.fillAmount)
        {
            statBarLife.fillAmount = Mathf.Lerp(statBarLife.fillAmount, current_hp / max_hp, Time.deltaTime * lerpSpeed);
            statTextLife.text = current_hp + " / " + max_hp;
        }
        if (current_hp == 0) // Ends the game when reaching zero life
        {
            statBarLife.fillAmount = 0;
            Time.timeScale = 0;
            GameObject.Find("HellFire").GetComponent<BulletWorks>().enabled = false;
        }
    }

    public void PlayerDamager(int dmg)
    {
        if (framesafe < Time.time)
        {
            current_hp -= (dmg - defence);
            framesafe = Time.time + immunityFrames;
        }
    }
}
