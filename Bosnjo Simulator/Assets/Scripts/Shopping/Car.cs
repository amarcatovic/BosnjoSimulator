using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class Car : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI carNameText;
    [SerializeField] TextMeshProUGUI carButtonText;
    [SerializeField] Button carBuyButton;
    [SerializeField] GameObject errorPanel;
    [SerializeField] TextMeshProUGUI errorPanelText;
    [SerializeField] float price;
    public float secs = 5f;
    public bool clicked = false;

    private void Start()
    {
        carButtonText.text = "KUPI " + price.ToString("#.##");
    }

    private void Update()
    {
        if(clicked)
        {
            carButtonText.text = "POTVRDI";
            secs -= Time.deltaTime;
        }
        if(secs <= 0)
        {
            clicked = false;
            secs = 5f;
            carButtonText.text = "KUPI " + price.ToString("#.##");
        }
    }

    public void OnBuyCarButtonClick()
    {
        if(!clicked)
        {
            clicked = true;
        }
        else
        {
            Gameplay gameplay = FindObjectOfType<Gameplay>();
            SaveData sd = gameplay.GetSaveData();

            if(sd.playerMoney < price)
            {
                errorPanel.SetActive(true);
            }
            else
            {
                bool found = false;
                for(int i = 0; i < sd.playerCars.Count; i++)
                {
                    if(sd.playerCars[i] == carNameText.text)
                    {
                        errorPanel.SetActive(true);
                        errorPanelText.text = sd.playerName + " već ima " + carNameText.text;
                        found = true;
                    }
                }

                if(!found)
                {
                    errorPanel.SetActive(false);
                    gameplay.SetNewActivity(sd.playerName + " kupuje " + PrilagodiIme(carNameText.text) + ".");
                    sd.playerCars.Add(carNameText.text);
                    sd.playerMoney -= price;
                    FindObjectOfType<GameSaveLogic>().Save();
                    SceneManager.LoadScene("MainGame");
                }
            }
        }
    }

    public void OnBuyHouseButtonClick()
    {
        if (!clicked)
        {
            clicked = true;
        }
        else
        {
            Gameplay gameplay = FindObjectOfType<Gameplay>();
            SaveData sd = gameplay.GetSaveData();

            if (sd.playerMoney < price)
            {
                errorPanel.SetActive(true);
            }
            else
            {
                bool found = false;
                for (int i = 0; i < sd.playerHomes.Count; i++)
                {
                    if (sd.playerHomes[i] == carNameText.text)
                    {
                        errorPanel.SetActive(true);
                        errorPanelText.text = sd.playerName + " već ima " + carNameText.text;
                        found = true;
                    }
                }

                if (!found)
                {
                    errorPanel.SetActive(false);
                    gameplay.SetNewActivity(sd.playerName + " kupuje " + PrilagodiIme(carNameText.text) + ".");
                    sd.playerHomes.Add(carNameText.text);
                    sd.playerMoney -= price;
                    FindObjectOfType<GameSaveLogic>().Save();
                    SceneManager.LoadScene("MainGame");
                }
            }
        }
    }

    private string PrilagodiIme(string ime)
    {
        if (ime == "Porodična Kuća")
        {
            return "Porodičnu Kuću";
        }
        if (ime[ime.Length - 1] == 'a' || ime[ime.Length - 1] == 'o')
        {
            return ime.Substring(0, ime.Length - 1) + "u";
        }

        return ime;
    }
}
