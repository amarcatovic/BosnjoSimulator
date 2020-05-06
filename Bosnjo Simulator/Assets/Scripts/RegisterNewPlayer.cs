using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RegisterNewPlayer : MonoBehaviour
{
    [SerializeField] TMP_InputField nameInput; 
    [SerializeField] TMP_InputField surnameInput;
    [SerializeField] TextMeshProUGUI headerText;

    private SaveData sd;
    public bool isNameChange = false;

    private void Start()
    {
        sd = FindObjectOfType<Gameplay>().GetSaveData();
        if (isNameChange)
        {
            headerText.text = "Promjena imena";
            nameInput.text = sd.playerName;
            surnameInput.text = sd.playerSurname;
        }
    }

    public void OnRegisterButtonClick()
    {
        if(nameInput.text == "" || surnameInput.text == "")
        {
            nameInput.text = "";
            surnameInput.text = "";
            if (isNameChange)
            {
                nameInput.text = sd.playerName;
                surnameInput.text = sd.playerSurname;
            }
        }
        else
        {
            SaveData sd = FindObjectOfType<Gameplay>().GetSaveData();
            sd.SetDefaults();
            string ime = nameInput.text.ToLower();
            string imePrvoSlovo = ime[0].ToString();
            ime = ime.Substring(1, ime.Length - 1);
            imePrvoSlovo = imePrvoSlovo.ToUpper();
            sd.playerName = imePrvoSlovo + ime;

            string prezime = surnameInput.text.ToLower();
            string prezimePrvoSlovo = prezime[0].ToString();
            prezime = prezime.Substring(1, prezime.Length - 1);
            prezimePrvoSlovo = prezimePrvoSlovo.ToUpper();
            sd.playerSurname = prezimePrvoSlovo + prezime;
            FindObjectOfType<GameSaveLogic>().Save();

            SceneManager.LoadScene("MainGame");
        }
    
    }
}
