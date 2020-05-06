using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CoinDropGamePlay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI taxmoneyText;
    [SerializeField] TextMeshProUGUI timeLeftText;
    [SerializeField] float money = 0f;
    [SerializeField] float timeLeft = 10f;
    [SerializeField] float moneyWasted = 0f;
    [SerializeField] AudioSource sound;

    private void Update()
    {
        if (timeLeft <= 0)
        {
            var cdmm = FindObjectOfType<CoinDropMainManager>();
            cdmm.totalMoney = money - moneyWasted;
            cdmm.money = money;
            cdmm.taxMoney = moneyWasted;
            SceneManager.LoadScene("MoneyCollectorGameScore");
        }
        else
        {
            timeLeft -= Time.deltaTime;
            timeLeftText.text = timeLeft.ToString("#.##");
        }        
    }

    void Start()
    {
        moneyText.text = "0";
        taxmoneyText.text = "0";
        money = 0;
        if (GetComponent<AudioSource>())
        {
            sound = GetComponent<AudioSource>();
        }
    }

    public void AddMoneyWasted(float value)
    {
        moneyWasted += value / 2;
        taxmoneyText.text = moneyWasted.ToString("#.##");
    }

    public void AddMoneyAndChangeScore(float value)
    {
        money += value;
        moneyText.text = money.ToString();
        sound.Play();
    }

    public float GetMoney()
    {
        return money;
    }
}
