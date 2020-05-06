using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class KladionicaManager1 : MonoBehaviour
{
    [SerializeField] GameObject errorPanel;
    [SerializeField] TextMeshProUGUI errorText;

    [SerializeField] TextMeshProUGUI teamsPlayingText;
    [SerializeField] TextMeshProUGUI kvota1Text;
    [SerializeField] TextMeshProUGUI kvotaXText;
    [SerializeField] TextMeshProUGUI kvota2Text;

    [SerializeField] TextMeshProUGUI metoda1Text;
    [SerializeField] TextMeshProUGUI metodaxText;
    [SerializeField] TextMeshProUGUI metoda2Text;

    [SerializeField] TextMeshProUGUI moneyInvestedText;
    private new AudioSource audio;

    // SELECTED TEAMS
    [SerializeField] public string selectedTeam1;
    [SerializeField] public string selectedTeam2;
    [SerializeField] public float selectedKvota1;
    [SerializeField] public float selectedKvota2;
    [SerializeField] public float selectedKvotaX;
    [SerializeField] public float moneyInvested = 1f;

    [SerializeField] public string odabirMetodeKladjenja = "";

    [SerializeField]
    List<string> teams = new List<string>()
    {
        "FC Bayern", "FC Barcelona", "Real Madrid", "Chelsea", "Machester United", "Manchester City", "PSG", "Arsenal", "Lyon", "Roma", "Juventus", "Lazio", "Borussia Dortmund", "RB Leibzig", "Everton", "Liverpool", "Atletico Madrid", "Ajax", "PSV", "Sevilla"
    };

    bool betButtonClicked = false;

    Gameplay gameplay;
    SaveData sd;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (!betButtonClicked)
        {
            CreatePair();
        }

        gameplay = FindObjectOfType<Gameplay>();
        sd = gameplay.GetSaveData();
    }

    public void OnBet1ButtonClick()
    {
        odabirMetodeKladjenja = "1";
        metoda1Text.color = Color.red;
        metodaxText.color = Color.white;
        metoda2Text.color = Color.white;
        errorPanel.SetActive(false);
        Debug.Log(audio);
        audio.Play();
    }

    public void OnBetXButtonClick()
    {
        odabirMetodeKladjenja = "X";
        metodaxText.color = Color.red;
        metoda1Text.color = Color.white;
        metoda2Text.color = Color.white;
        errorPanel.SetActive(false);
        audio.Play();
    }

    public void OnBet2ButtonClick()
    {
        odabirMetodeKladjenja = "2";
        metoda2Text.color = Color.red;
        metodaxText.color = Color.white;
        metoda1Text.color = Color.white;
        errorPanel.SetActive(false);
        audio.Play();
    }

    public void OnBetButtonClick()
    {
        audio.Play();
        if (odabirMetodeKladjenja == "")
        {
            errorPanel.SetActive(true);
        }
        else if (moneyInvested > sd.playerMoney)
        {
            errorText.text = "Nemate Dovoljno Novca";
            errorPanel.SetActive(true);
        }
        else
        {
            sd.playerMoney -= moneyInvested;
            FindObjectOfType<GameSaveLogic>().Save();
            betButtonClicked = true;
            SceneManager.LoadScene("TiketUtakmica");
        }
    }

    public void OnPlusButtonClick()
    {
       moneyInvested += 1f;
       moneyInvestedText.text = moneyInvested.ToString("#") + " KM";
       audio.Play();

    }

    public void OnMinusButtonClick()
    {
        if (moneyInvested > 1f)
        {
            moneyInvested -= 1f;
            moneyInvestedText.text = moneyInvested.ToString("#") + " KM";
            audio.Play();
        }

    }

    public void OnChangePairButtonClick()
    {
        CreatePair();
        audio.Play();
    }

    private void CreatePair()
    {
        do
        {
            string team1 = teams[UnityEngine.Random.Range(0, teams.Count)];
            selectedTeam1 = team1;
            string team2 = teams[UnityEngine.Random.Range(0, teams.Count)];
            selectedTeam2 = team2;
            teamsPlayingText.text = team1 + " - " + team2;

            float kvota1, kvotaX, kvota2;
            float kvotaBazna = UnityEngine.Random.Range(1f, 2f);
            if (kvotaBazna < 1.5f)
            {
                selectedKvota1 = kvota1 = kvotaBazna;
                selectedKvotaX = kvotaX = kvotaBazna + UnityEngine.Random.Range(1f, 2f);
                selectedKvota2 = kvota2 = kvotaBazna + 1f + UnityEngine.Random.Range(0f, 1f);
                kvota1Text.text = kvota1.ToString("#.##");
                kvotaXText.text = kvotaX.ToString("#.##");
                kvota2Text.text = kvota2.ToString("#.##");
            }
            else
            {
                selectedKvota2 = kvota2 = kvotaBazna;
                selectedKvotaX = kvotaX = kvotaBazna + UnityEngine.Random.Range(1f, 2f);
                selectedKvota1 = kvota1 = kvotaBazna + 1f + UnityEngine.Random.Range(0f, 1f);
                kvota1Text.text = kvota1.ToString("#.##");
                kvotaXText.text = kvotaX.ToString("#.##");
                kvota2Text.text = kvota2.ToString("#.##");
            }

            if (team1 != team2)
                break;
        } while(true);
    }
}
