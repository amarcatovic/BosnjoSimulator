using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TiketGameManager : MonoBehaviour
{
    [SerializeField] Button endGameButton;
    [SerializeField] bool gameEnded = false;

    [SerializeField] TextMeshProUGUI minutesText;
    [SerializeField] TextMeshProUGUI team1Text;
    [SerializeField] TextMeshProUGUI team2Text;
    [SerializeField] TextMeshProUGUI team1ScoreText;
    [SerializeField] int team1Score = 0;
    [SerializeField] TextMeshProUGUI team2ScoreText;
    [SerializeField] int team2Score = 0;

    [SerializeField] TextMeshProUGUI[] chances;
    [SerializeField] int chancesCount = 0;
    [SerializeField] float gameMinute = 0f;
    [SerializeField] float waitForSecChance = 6f;
    [SerializeField] float secPassedSinceLastChance = 0f;
    [SerializeField] float secPassedSinceGameBegin = 0f;

    private KladionicaManager1 km;
    private List<string> liveChances = new List<string>()
    {
        "Igrači domaćeg tima imaju slobodan udarac. Šut i GOOOOOL", "Kakav dribling domaćina, GOOOOOL", "Odlična pas igra domačina, GOOOOOL", "GOOOOOL za domaćine, sjajna akcija", "GOOOOOOOL, sa 20m za domaćine",
        "Igrači gostujućeg tima imaju sjajnu akciju. GOOOOOL", "Gosti igraju dobru pas igru i GOOOOOL", "Gosti imaju slobodan udarac. GOOOOOL", "Sjajan prodor gostujućeg tima i GOOOOL", "Kontra napad gostiju i GOOOOOOOL",
        "Sjajan šut domaćina, ali gostujući golman brani", "Jedanaesterac za domaćine. Šut i odbrana golmana", "Korner za domaće. Golman je uhvatio loptu.", "Korner za domaće, ali loše izveden", "Korner za domaće, ali štoper gostiju izbacuje loptu iz kaznenog prostora",
        "Korner za goste, ali loše izveden", "Korner za goste, ali štoper domaćih izbacuje loptu iz kaznenog prostora", "Aut za domaće.", "Aut za goste", "Kakav šut kapitena domaćih, golman nije imao šanse, ali je samo prečka", 
        "Velika prilika za goste, savladan je golman, ali sa crte loptu izbija štoper domaćih", "Publika se dobro zabavlja na današnjoj utakmici", "GOOOOOL za domaće. Ipak offside nakon VAR-a", "Penal za goste. Kapiten gostiju prilazi da izvede penal. KAKAV PROMAŠAJ.",
        "Jedan na jedan prilika za domaće, ali kako se samo postavio golman gostiju, sjajna odbrana", "Žuti karton za igraća gostiju", "Žuti karton za domaćeg igraća", "Crveni karton za goste. Ipak žuti nakon konsultacije sa VAR sudijom",
        "Da li je ovo penal za domaće? Ipak ne, sudija je ustanovio da je igrač simulirao", "Gosti pokušavaju igrati na posjed", "Domaći pokušavaju zaustaviti napad gostiju", "Velika prilika za domaće, ali lopta pogađa stativu"
    };

    void Start()
    {
        km = FindObjectOfType<KladionicaManager1>();
        team1Text.text = km.selectedTeam1;
        team2Text.text = km.selectedTeam2;
        team1ScoreText.text = "0";
        team2ScoreText.text = "0";
        endGameButton.interactable = false;
    }

    void Update()
    {
        if(!gameEnded)
        {
            secPassedSinceLastChance += Time.deltaTime;
            minutesText.text = secPassedSinceGameBegin.ToString("#") + "'";
            secPassedSinceGameBegin += Time.deltaTime;

            if(secPassedSinceLastChance > waitForSecChance)
            {
                int randomIndex = Random.Range(0, liveChances.Count);
                if(randomIndex < 5)
                {
                    ++team1Score;
                    team1ScoreText.text = team1Score.ToString();
                    chances[chancesCount++].text = liveChances[randomIndex];
                }
                else if(randomIndex > 4 && randomIndex < 10)
                {
                    ++team2Score;
                    team2ScoreText.text = team2Score.ToString();
                    chances[chancesCount++].text = liveChances[randomIndex];
                }
                else
                {
                    chances[chancesCount++].text = liveChances[randomIndex];
                }

                if(chancesCount >= 15)
                {
                    endGameButton.interactable = true;
                    gameEnded = true;
                    minutesText.text = "90'";
                }
                secPassedSinceLastChance = 0f;
            }
        }
    }

    public void OnSimulateGameButtonClick()
    {
        waitForSecChance = 0f;
    }

    public void GameEndButtonClicked()
    {
        if(gameEnded)
        {
            Gameplay gameplay = FindObjectOfType<Gameplay>();
            SaveData sd = gameplay.GetSaveData();
            string pobjednik = "";
            if(team1Score > team2Score)
            {
                pobjednik = "1";
            }
            else if (team1Score == team2Score)
            {
                pobjednik = "X";
            }
            else
            {
                pobjednik = "2";
            }


            if (km.odabirMetodeKladjenja == "1")
            {
                if(pobjednik != km.odabirMetodeKladjenja)
                {
                    sd.playerMoney -= km.moneyInvested;
                    sd.playerHappiness -= 0.1f;
                    gameplay.SetNewActivity(sd.playerName + " pada na kladionici. Izgubio je " + km.moneyInvested.ToString("#.##") + "KM");
                    FindObjectOfType<GameSaveLogic>().Save();
                    SceneManager.LoadScene("Kladionica");
                }
                else
                {
                    sd.playerMoney += km.moneyInvested * km.selectedKvota1;
                    sd.playerHappiness += 0.1f;
                    gameplay.SetNewActivity(sd.playerName + " je dobio " + (km.moneyInvested * km.selectedKvota1).ToString("#.##") + "KM na kladionici");
                    FindObjectOfType<GameSaveLogic>().Save();
                    SceneManager.LoadScene("Kladionica");
                }
            }

            if (km.odabirMetodeKladjenja == "X")
            {
                if (pobjednik != km.odabirMetodeKladjenja)
                {
                    sd.playerMoney -= km.moneyInvested;
                    sd.playerHappiness -= 0.1f;
                    gameplay.SetNewActivity(sd.playerName + " pada na kladionici. Izgubio je " + km.moneyInvested.ToString("#.##") + "KM");
                    FindObjectOfType<GameSaveLogic>().Save();
                    SceneManager.LoadScene("Kladionica");
                }
                else
                {
                    sd.playerMoney += km.moneyInvested * km.selectedKvotaX;
                    sd.playerHappiness += 0.1f;
                    gameplay.SetNewActivity(sd.playerName + " je dobio " + (km.moneyInvested * km.selectedKvota1).ToString("#.##") + "KM na kladionici");
                    FindObjectOfType<GameSaveLogic>().Save();
                    SceneManager.LoadScene("Kladionica");
                }
            }

            if (km.odabirMetodeKladjenja == "2")
            {
                if (pobjednik != km.odabirMetodeKladjenja)
                {
                    sd.playerMoney -= km.moneyInvested;
                    sd.playerHappiness -= 0.1f;
                    gameplay.SetNewActivity(sd.playerName + " pada na kladionici. Izgubio je " + km.moneyInvested.ToString("#.##") + "KM");
                    FindObjectOfType<GameSaveLogic>().Save();
                    SceneManager.LoadScene("Kladionica");
                }
                else
                {
                    sd.playerMoney += km.moneyInvested * km.selectedKvota2;
                    sd.playerHappiness += 0.1f;
                    gameplay.SetNewActivity(sd.playerName + " je dobio " + (km.moneyInvested * km.selectedKvota1).ToString("#.##") + "KM na kladionici");
                    FindObjectOfType<GameSaveLogic>().Save();
                    SceneManager.LoadScene("Kladionica");
                }
            }

            Destroy(FindObjectOfType<KladionicaManager1>().gameObject);
        }
    }
}
