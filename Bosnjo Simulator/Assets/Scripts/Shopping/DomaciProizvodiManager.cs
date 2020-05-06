using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DomaciProizvodiManager : MonoBehaviour
{
    public bool isFabrika = false;
    [SerializeField] Proizvod[] proizvodi;

    [SerializeField] TextMeshProUGUI timeLeftText;
    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] float timeLeft = 30f;

    Gameplay gameplay;
    SaveData sd;
    GameSaveLogic gsl;

    public float score = 0;

    private void Start()
    {
        gameplay = FindObjectOfType<Gameplay>();
        sd = gameplay.GetSaveData();
        gsl = FindObjectOfType<GameSaveLogic>();

        for(int i = 0; i < proizvodi.Length; ++i)
        {
            proizvodi[i].appearInSec = UnityEngine.Random.Range(0f, 10f);
            proizvodi[i].beActiveForSec = UnityEngine.Random.Range(1f, 2f);
        }
    }

    private void Update()
    {
        TimeManagment();
        scoreText.text = score.ToString("#.##");

    }

    private void TimeManagment()
    {
        timeLeft -= Time.fixedDeltaTime;
        timeLeftText.text = timeLeft.ToString("#.##");
        if(timeLeft <= 0f)
        {
            if(score > 0)
            {
                if(isFabrika)
                {
                    string kreditText = "";
                    gameplay.KreditManager(ref score, ref kreditText);
                    sd.playerMoney += score;
                    sd.playerHealth -= 0.1f;
                    sd.playerHunger -= 0.1f;
                    sd.playerThirst -= 0.1f;
                    sd.playerHappiness -= 0.1f;
                    gameplay.SetNewActivity(sd.playerName + " se vraća sa posla u fabrici. Zaradio je " + score.ToString("#.##") + "KM." + kreditText);
                }
                else
                {
                    sd.playerMoney -= 25f;
                    sd.playerHealth += 0.2f;
                    sd.playerHunger += 0.15f;
                    sd.playerThirst += 0.15f;
                    gameplay.SetNewActivity(sd.playerName + " kupuje domaće namirnice. Svaka čast!");
                }
            }
            else
            {
                if (isFabrika)
                {
                    sd.playerMoney += score;
                    sd.playerHealth -= 0.1f;
                    sd.playerHunger -= 0.1f;
                    sd.playerThirst -= 0.1f;
                    sd.playerHappiness -= 0.1f;
                    gameplay.SetNewActivity(sd.playerName + " se vraća s posla u fabrici. Napravio je propust i morao platiti štetu od " + score.ToString("#.##") + "KM");
                }
                else
                {
                    sd.playerMoney -= 50f;
                    sd.playerHappiness -= 0.1f;
                    sd.playerHealth -= 0.15f;
                    sd.playerHunger -= 0.05f;
                    sd.playerThirst -= 0.1f;
                    gameplay.SetNewActivity(sd.playerName + " se vraća iz prodavnice. Sljedeći put preporućujemo da kupujete domaće.");
                }
            }

            gsl.Save();
            SceneManager.LoadScene("MainGame");
        }
    }
}
