using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisaCheck : MonoBehaviour
{
    [SerializeField] Button workButton;
    [SerializeField] GameObject errorPanel;
    [SerializeField] TMPro.TextMeshProUGUI errorText;

    SaveData sd;

    void Start()
    {
        sd = FindObjectOfType<Gameplay>().GetSaveData();
        if(!sd.hasVisa)
        {
            errorText.text = sd.playerName + " nema vizu";
            errorPanel.SetActive(true);
            workButton.interactable = false;
        }
        else if(sd.playerHealth < 0.5f)
        {
            errorText.text = sd.playerName + " nije dovoljno zdrav";
            errorPanel.SetActive(true);
            workButton.interactable = false;
        }
    }

}
