using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PassportManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bosnjoNameText;
    [SerializeField] TMPro.TextMeshProUGUI passportAvailableText;
    [SerializeField] TMPro.TextMeshProUGUI errorText;
    [SerializeField] float passportPrice = 1000f;
    [SerializeField] GameObject errorPanel;
    SaveData sd;

    void Start()
    {
        sd = FindObjectOfType<Gameplay>().GetSaveData();
        bosnjoNameText.text = sd.playerName + " " + sd.playerSurname;
        if(!sd.hasPassport)
        {
            passportAvailableText.text = "Pasoš: Ne posjeduje";
            passportAvailableText.color = Color.red;
        }
    }

    public void OnGetPassportButtonClick()
    {
        if(sd.hasPassport)
        {
            errorText.text = sd.playerName + " posjeduje pasoš";
            errorPanel.SetActive(true);
        }
        else if(sd.playerMoney <= passportPrice)
        {
            errorPanel.SetActive(true);
            errorText.text = sd.playerName + " nema dovoljno novca";
        }
        else
        {
            sd.playerMoney -= passportPrice;
            sd.hasPassport = true;
            FindObjectOfType<Gameplay>().SetNewActivity(sd.playerName + " je dobio pasoš");
            FindObjectOfType<GameSaveLogic>().Save();
            SceneManager.LoadScene("MainGame");
        }
    }

   
}
