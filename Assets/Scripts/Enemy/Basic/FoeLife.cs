using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FoeLife : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private int maxLife;
    [SerializeField] private int defence;

    [Header("Statistic")]
    [SerializeField] private Image LBar;
    [SerializeField] private TMP_Text LText;
    [SerializeField] private float lerpSpeed;

    private float curLife;
    public bool immune;

    private void Start()
    {
        curLife = maxLife;
        immune = false;
    }
    private void Update()
    {
        if (curLife != LBar.fillAmount)
        {
            LBar.fillAmount = Mathf.Lerp(LBar.fillAmount, curLife / maxLife, Time.deltaTime * lerpSpeed);
            LText.text = curLife + " / " + maxLife;
        }
        if (curLife == 0)
        {
            Destroy(gameObject);
        }
    }
    public void Damager(int dmg)
    {
        if (!immune)
        {
            curLife -= (dmg - defence);
        }

    }
}
