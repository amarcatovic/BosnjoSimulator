using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class RaceDirector : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI truckWinnerText;
    [SerializeField] Button nextButton;

    private bool raceFinished = false;
    private string raceWinner = "";
    private new AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        nextButton.interactable = false;
        audio.Play();
    }

    public void RegisterTruckFinish(string truckName)
    {
        if(!raceFinished)
        {
            audio.Stop();
            raceWinner = truckName;
            truckWinnerText.text = raceWinner + " je pobjednik";

            if(raceWinner == "Žuti")
            {
                truckWinnerText.color = Color.yellow;
            }
            if (raceWinner == "Crveni")
            {
                truckWinnerText.color = Color.red;
            }
            if (raceWinner == "Plavi")
            {
                truckWinnerText.color = Color.blue;
            }
            if (raceWinner == "Zeleni")
            {
                truckWinnerText.color = Color.green;
            }

            raceFinished = true;
            nextButton.interactable = true;
        }
    }

    public void OnNextButtonClick()
    {
        if(raceFinished)
        {
            TruckBetting tb = FindObjectOfType<TruckBetting>();
            Gameplay gameplay = FindObjectOfType<Gameplay>();
            SaveData sd = gameplay.GetSaveData();

            if(tb.playerTruckChoice == raceWinner)
            {
                sd.playerMoney += 25 * tb.moneyInvested;
                sd.playerHappiness += 0.05f;
                gameplay.SetNewActivity(sd.playerName + " se kladio na " + raceWinner + " kamion i osvojio " + (25 * tb.moneyInvested).ToString("#") + "KM");
                FindObjectOfType<GameSaveLogic>().Save();
            }
            else
            {
                sd.playerMoney -= tb.moneyInvested;
                sd.playerHappiness -= 0.05f;
                gameplay.SetNewActivity(sd.playerName + " se kladio na " + raceWinner + " kamion i izgubio " + tb.moneyInvested.ToString("#") + "KM");
                FindObjectOfType<GameSaveLogic>().Save();
            }

            Destroy(tb.gameObject);
            SceneManager.LoadScene("Kladionica");
        }
    }
}
