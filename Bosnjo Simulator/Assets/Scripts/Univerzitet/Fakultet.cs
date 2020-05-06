using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Fakultet : MonoBehaviour
{
    [SerializeField] GameObject errorPanel;
    [SerializeField] TextMeshProUGUI errorText;

    [SerializeField] TextMeshProUGUI nazivFakultetaText;
    [SerializeField] string nazivFakulteta;
    [SerializeField] TextMeshProUGUI trenutnaGodinaFakultetaText;
    [SerializeField] TextMeshProUGUI trajanjeFakultetaText;
    [SerializeField] int trajanjeFakulteta;
    [SerializeField] TextMeshProUGUI cijenaFakultetaText;
    [SerializeField] int cijenaFakulteta;
    [SerializeField] Button fakultetButton;
    [SerializeField] TextMeshProUGUI fakultetButtonText;

    Gameplay gameplay;
    SaveData sd;

    void Start()
    {
        nazivFakultetaText.text = nazivFakulteta;
        trajanjeFakultetaText.text = "Trajanje: " + trajanjeFakulteta.ToString() + " godine";
        cijenaFakultetaText.text = "Cijena po godini: " + cijenaFakulteta.ToString() + "KM";

        gameplay = FindObjectOfType<Gameplay>();
        sd = gameplay.GetSaveData();

        int indexFakulteta = sd.playerCurrentFaculties.IndexOf(nazivFakulteta);
        if(indexFakulteta != -1)
        {
            trenutnaGodinaFakultetaText.text = "Trenutna godina: " + sd.playerCurrentFacultiesYear[indexFakulteta].ToString();
        }
        else
        {
            if(sd.playerFinishedFaculties.IndexOf(nazivFakulteta) != -1)
            {
                trenutnaGodinaFakultetaText.color = Color.yellow;
                trenutnaGodinaFakultetaText.text = "Diplomirao";
            }
            else
            {
                trenutnaGodinaFakultetaText.color = Color.red;
                trenutnaGodinaFakultetaText.text = "Ne studira na ovom fakultetu";
            }
        }
        
        
        if(sd.playerFinishedFaculties.Contains(nazivFakulteta))
        {
            fakultetButtonText.text = "Završen";
            fakultetButton.interactable = false;
        }
    }

    public void FakultetButtonClick()
    {
        if(sd.playerMoney < cijenaFakulteta)
        {
            errorText.text = "Nemate dovoljno novca za fakultet";
            errorPanel.SetActive(true);
            return;
        }
        else if(sd.playerHealth < 0.5)
        {
            errorText.text = "Zdravlje mora biti veće od 50%";
            errorPanel.SetActive(true);
            return;
        }
        else if(sd.playerHappiness < 0.9f)
        {
            errorText.text = "Nemate volje za fakultet. Zabava mora biti veća od 90%";
            errorPanel.SetActive(true);
            return;
        }
        else if(sd.playerHunger < 0.5)
        {
            errorText.text = "Previše ste gladni. Glad mora biti veća od 50%";
            errorPanel.SetActive(true);
            return;
        }
        else if (sd.playerThirst < 0.8)
        {
            errorText.text = "Žedni ste. Žeđ mora biti veća od 80%";
            errorPanel.SetActive(true);
            return;
        }

        FakultetManager fm = FindObjectOfType<FakultetManager>();
        int trenutniFakultetIndex = sd.playerCurrentFaculties.IndexOf(nazivFakulteta);
        if(trenutniFakultetIndex == -1)
        {
            sd.playerCurrentFaculties.Add(nazivFakulteta);
            sd.playerCurrentFacultiesYear.Add(1);
            fm.godinaFakulteta = 1;
        }
        else
        {
            fm.godinaFakulteta = sd.playerCurrentFacultiesYear[trenutniFakultetIndex];
            if(fm.godinaFakulteta == trajanjeFakulteta)
            {
                fm.isZadnjaGodina = true;
            }
        }

        fm.odabraniFakultet = nazivFakulteta;
        fm.isZadnjaGodina = false;

        sd.playerMoney -= cijenaFakulteta;
        sd.playerHealth -= 0.5f;
        sd.playerHunger -= 0.5f;
        sd.playerThirst -= 0.8f;
        sd.playerHappiness -= 0.9f;

        SceneManager.LoadScene("FakultetBeforeGame");
    }
    
}
