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
    [SerializeField] private float lerpSpeed;

    private float curLife;

    private void Start()
    {
        curLife = maxLife;
    }
    private void Update()
    {
        if (curLife != LBar.fillAmount)
        {
            LBar.fillAmount = Mathf.Lerp(LBar.fillAmount, curLife / maxLife, Time.deltaTime * lerpSpeed);
        }
        if (curLife == 0)
        {
            Destroy(gameObject);
        }
    }
    public void Damager(int dmg)
    {
        curLife -= (dmg - defence);
    }
}
