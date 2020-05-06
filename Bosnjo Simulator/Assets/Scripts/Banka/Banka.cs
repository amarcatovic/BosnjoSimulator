using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Banka : MonoBehaviour
{
    [SerializeField] GameObject errorPanel;
    [SerializeField] TMPro.TextMeshProUGUI errorText;
    [SerializeField] TMPro.TextMeshProUGUI moneyLeftText;

    private Gameplay gameplay;
    private SaveData sd;

    private void Start()
    {
        gameplay = FindObjectOfType<Gameplay>();
        sd = gameplay.GetSaveData();
        if(sd.hasKredit)
        {
            moneyLeftText.text = "Preostalo za vratiti: " + sd.kreditValue.ToString("#.##") + "KM";
        }
        else
        {
            moneyLeftText.text = "";
        }
    }

    private bool ProvjeraKredita()
    {
        if (sd.hasKredit)
        {
            errorPanel.SetActive(true);
            errorText.text = sd.playerName + " je već podigao kredit";
            return true;
        }
            

        return false;
    }

    public void OnOption1ButtonClick()
    {
        if(!ProvjeraKredita())
        {
            sd.hasKredit = true;
            sd.kreditValue = 11000f;
            sd.playerMoney += 10000f;
            gameplay.SetNewActivity(sd.playerName + " diže kredit od 10000KM");
            SceneManager.LoadScene("MainGame");
        }
    }

    public void OnOption2ButtonClick()
    {
        if (!ProvjeraKredita())
        {
            sd.hasKredit = true;
            sd.kreditValue = 51000f;
            sd.playerMoney += 50000f;
            gameplay.SetNewActivity(sd.playerName + " diže kredit od 50000KM");
            SceneManager.LoadScene("MainGame");
        }
    }

    public void OnOption3ButtonClick()
    {
        if (!ProvjeraKredita())
        {
            sd.hasKredit = true;
            sd.kreditValue = 110000f;
            sd.playerMoney += 100000f;
            gameplay.SetNewActivity(sd.playerName + " diže kredit od 100000KM. Pravo dobro se zadužio!");
            SceneManager.LoadScene("MainGame");
        }
    }
}
