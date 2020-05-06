using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalScoreCoins : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Text moneyEarnedScoreText;
    [SerializeField] Text taxMoneyText;
    [SerializeField] Text jobText;
    [SerializeField] Text bonusText;

    private float bonus = 0f;

    Gameplay gameplay;
    SaveData sd;

    // Start is called before the first frame update
    void Start()
    {
        var cdm = FindObjectOfType<CoinDropMainManager>();
        gameplay = FindObjectOfType<Gameplay>();
        sd = gameplay.GetSaveData();
        bonusText.text = "";

        float money = cdm.totalMoney;

        string posao = "";
        if (sd.playerFinishedFaculties.Count > 0)
        {
            var fax = sd.playerFinishedFaculties[Random.Range(0, sd.playerFinishedFaculties.Count)];

            switch (fax)
            {
                case "Šumarski Fakultet":
                    posao += "šumarski inženjer";
                    bonus = Random.Range(100, 300);
                    break;
                case "Prehrambeni Fakultet":
                    posao += "prehrambeni inženjer";
                    bonus = Random.Range(300, 500);
                    break;
                case "Saobraćajni Fakultet":
                    posao += "saobraćajni inženjer";
                    bonus = Random.Range(500, 700);
                    break;
                case "Ekonomski Fakultet":
                    posao += "ekonomista";
                    bonus = Random.Range(700, 900);
                    break;
                case "Metalurški Fakultet":
                    posao += "metalurški inženjer";
                    bonus = Random.Range(900, 1100);
                    break;
                case "Arhitektonski Fakultet":
                    posao += "arhitekta";
                    bonus = Random.Range(1100, 1300);
                    break;
                case "Pravni Fakultet":
                    posao += "advokat";
                    bonus = Random.Range(1300, 1500);
                    break;
                case "Informatički Fakultet":
                    posao += "programer";
                    bonus = Random.Range(1500, 1700);
                    break;
                case "Mašinski Fakultet":
                    posao += "mašinski inženjer";
                    bonus = Random.Range(1700, 1900);
                    break;
                case "Elektrotehnički Fakultet":
                    posao += "elektro inženjer";
                    bonus = Random.Range(1900, 2100);
                    break;
                case "Medicinski Fakultet":
                    posao += "doktor";
                    bonus = Random.Range(2100, 3000);
                    break;
            }
        }
        else
        {
            string[] poslovi = { "medicinski tehničar", "vozač kamiona", "komercijalista", "pekar", "građevinski radnik", "vodoinstalater", "električar", "obučar" };
            posao += poslovi[Random.Range(0, poslovi.Length)];
            bonus = 0f;
        }

        bonus += (bonus / 10) * sd.playerFinishedFaculties.Count;
        jobText.text = "Posao: " + posao;
        if(bonus != 0f)
        {
            bonusText.text = "Bonus: " + bonus.ToString("#.##") + "KM";
        }
        posao = "Radio je kao " + posao;

        string text = "";
        money = (cdm.totalMoney + bonus);
        gameplay.KreditManager(ref money, ref text);

        posao += " " + text;

        sd.playerMoney += money;


        scoreText.text = (cdm.totalMoney + bonus).ToString("#.##") + "KM";
        taxMoneyText.text = "Porez: " + cdm.taxMoney.ToString("#.##") + "KM";
        moneyEarnedScoreText.text = "Zarada: " + cdm.money.ToString("#.##") + "KM";

        gameplay.SetNewActivity(gameplay.GetSaveData().playerName + " se vraća iz Njemačke sa " + money.ToString("#.##") + "KM. " + posao);
        Destroy(FindObjectOfType<CoinDropMainManager>().gameObject);
        FindObjectOfType<GameSaveLogic>().Save();
    }

    public void OnCollectMoneyButonClick()
    {
        SceneManager.LoadScene("MainGame");
    }
}
