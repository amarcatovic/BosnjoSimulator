using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Cars: MonoBehaviour
{
    public string carName;
    public float carPrice;

    [SerializeField] GameObject errorPanel;
    [SerializeField] TextMeshProUGUI errorText;

    [SerializeField] Button confirmButton;

    Gameplay gp;
    SaveData sd;

    public void OnDriveButtonClick()
    {
        gp = FindObjectOfType<Gameplay>();
        sd = gp.GetSaveData();

        if(sd.playerMoney < 20)
        {
            errorPanel.SetActive(true);
            errorText.text = "Treba Vam 20KM za goriva";
            return;
        }

        string[] opcija = { " kroz grad 100 na sat.", ". Ugasio mu se par puta.", " i svira svakom prolazniku.", ". Zaustavlja ga policija, ali je sve uredno.", ". Otišao je sa ženom do punice."};

        gp.SetNewActivity(sd.playerName + " vozi " + carName + opcija[Random.Range(0, opcija.Length)]);
        sd.playerMoney -= 20;
        sd.playerHappiness += 0.05f;
        FindObjectOfType<GameSaveLogic>().Save();
        SceneManager.LoadScene("Vozi");
    }

    public void OnHouseButtonClick()
    {
        gp = FindObjectOfType<Gameplay>();
        sd = gp.GetSaveData();

        if (sd.playerMoney < 50)
        {
            errorPanel.SetActive(true);
            errorText.text = "Treba Vam 50KM za zabavu";
            return;
        }

        string[] opcija = { " organizuje roštiljadu ", " organizuje zabavu ", " gleda sa društvom utakmicu ", " gleda film sa rajom ", " uživa uz karaoke sa društvom " };

        gp.SetNewActivity(sd.playerName + opcija[Random.Range(0, opcija.Length)] + " u " + carName);
        sd.playerMoney -= 50;
        sd.playerHappiness += 0.1f;
        FindObjectOfType<GameSaveLogic>().Save();
        SceneManager.LoadScene("Party");
    }

    public void OnSellButtonClick()
    {
        confirmButton.gameObject.SetActive(true);
    }

    public void OnConfirmButtonClick()
    {
        gp = FindObjectOfType<Gameplay>();
        sd = gp.GetSaveData();

        sd.playerMoney += carPrice / 2;
        gp.SetNewActivity(sd.playerName + " prodaje " + carName + " za " + (carPrice / 2).ToString("#.##") + "KM.");
        sd.playerCars.Remove(carName);
        FindObjectOfType<GameSaveLogic>().Save();
        SceneManager.LoadScene("MainGame");
    }

    public void OnConfirmHouseButtonClick()
    {
        gp = FindObjectOfType<Gameplay>();
        sd = gp.GetSaveData();

        sd.playerMoney += carPrice / 2;
        gp.SetNewActivity(sd.playerName + " prodaje " + carName + " za " + (carPrice / 2).ToString("#.##") + "KM.");
        sd.playerHomes.Remove(carName);
        FindObjectOfType<GameSaveLogic>().Save();
        SceneManager.LoadScene("MainGame");
    }

    public void OnCancelButtonClick()
    {
        confirmButton.gameObject.SetActive(false);
    }



}
