using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameTracker : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI moneyText;
    [SerializeField] TMPro.TextMeshProUGUI playerNameText;
    [SerializeField] Slider hungerSlider;
    [SerializeField] Slider thirstSlider;
    [SerializeField] Slider healthSlider;
    [SerializeField] Slider happinessSlider;

    [SerializeField] TMPro.TextMeshProUGUI activity1Text;
    [SerializeField] TMPro.TextMeshProUGUI activity2Text;
    [SerializeField] TMPro.TextMeshProUGUI activity3Text;
    [SerializeField] TMPro.TextMeshProUGUI activity4Text;
    [SerializeField] TMPro.TextMeshProUGUI activity5Text;



    [SerializeField] float currentSeconds = 10f;

    void Update()
    {
        UpdatePlayerInfoPanel();
    }

    private void UpdatePlayerInfoPanel()
    {
        SaveData sd = FindObjectOfType<Gameplay>().GetSaveData();
        moneyText.text = sd.playerMoney.ToString("#.##") + "KM";
        playerNameText.text = sd.playerName + " " + sd.playerSurname;
        activity1Text.text = sd.playerLatestActivities[0];
        activity2Text.text = sd.playerLatestActivities[1];
        activity3Text.text = sd.playerLatestActivities[2];
        activity4Text.text = sd.playerLatestActivities[3];
        activity5Text.text = sd.playerLatestActivities[4];

        sd.playerHunger -= Time.deltaTime * 0.0005f;
        sd.playerThirst -= Time.deltaTime * 0.001f;
        hungerSlider.value = sd.playerHunger;
        thirstSlider.value = sd.playerThirst;
        healthSlider.value = sd.playerHealth;
        happinessSlider.value = sd.playerHappiness;

        if(sd.playerHappiness > 1)
        {
            sd.playerHappiness = 1f;
        }

        if(sd.playerHealth > 1)
        {
            sd.playerHealth = 1f;
        }

        currentSeconds -= Time.deltaTime;
        if(currentSeconds <= 0)
        {
            FindObjectOfType<GameSaveLogic>().Save();
            currentSeconds = 10f;
        }
    }
}
