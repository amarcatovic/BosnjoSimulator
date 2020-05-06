using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class VisaManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI imePrezimeText;
    [SerializeField] TextMeshProUGUI visaNumberText;
    [SerializeField] TextMeshProUGUI errorText;
    [SerializeField] TextMeshProUGUI hasVisaText;

    [SerializeField] GameObject errorPanel;

    SaveData sd;

    void Start()
    {
        sd = FindObjectOfType<Gameplay>().GetSaveData();
        imePrezimeText.text = sd.playerName + " " + sd.playerSurname;
        if (!sd.hasVisa)
        {
            hasVisaText.text = "NE POSJEDUJE VIZU";
        }
        string visaNumber = "";
        for(int i = 0; i < 9; ++i)
        {
            visaNumber += UnityEngine.Random.Range(0, 9).ToString();
        }
        visaNumberText.text = visaNumber;
    }

    public void OnGetVisaButtonClick()
    {
        if (sd.hasVisa)
        {
            errorText.text = sd.playerName + " ima vizu";
            errorPanel.SetActive(true);
        }
        else if(!sd.hasPassport)
        {
            errorText.text = sd.playerName + " nema pasoš";
            errorPanel.SetActive(true);
        }
        else if (sd.playerMoney <= 100f)
        {
            errorPanel.SetActive(true);
            errorText.text = sd.playerName + " nema dovoljno novca";
        }
        else
        {
            sd.playerMoney -= 100f;
            sd.hasVisa = true;
            FindObjectOfType<Gameplay>().SetNewActivity(sd.playerName + " je dobio vizu za Njemačku");
            FindObjectOfType<GameSaveLogic>().Save();
            SceneManager.LoadScene("MainGame");
        }
    }
}
