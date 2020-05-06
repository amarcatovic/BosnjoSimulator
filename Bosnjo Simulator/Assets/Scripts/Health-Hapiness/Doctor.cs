using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Doctor : MonoBehaviour
{
    [SerializeField] float doctorPrice = 5f;
    [SerializeField] float doctorHealthValue = 0.5f;
    [SerializeField] TextMeshProUGUI doctorPriceText;
    [SerializeField] UnityEngine.UI.Slider doctorHealthSlider;
    public bool isSpecijalista;
    public bool isDoctor;
    public bool isStudent;
    public bool isAlt;
    

    [SerializeField] GameObject error;

    private void Awake()
    {
        doctorPriceText.text = "LIJECI SE " + doctorPrice.ToString("#.##") + "KM";
        doctorHealthSlider.value = doctorHealthValue;
    }

    public void OnBuyDoctorButtonClick()
    {
        Gameplay temp = FindObjectOfType<Gameplay>();
        SaveData sd = temp.GetSaveData();
        if (sd.playerMoney < doctorPrice)
        {
            error.SetActive(true);
        }
        else
        {
            sd.playerHealth = Mathf.Clamp(sd.playerHealth + doctorHealthValue, 0f, 1f);
            sd.playerMoney -= doctorPrice;

            if(isSpecijalista)
            {
                temp.SetNewActivity(sd.playerName + " odlazi kod specijaliste. Sada je potpuno zdrav");
            }
            else if(isDoctor)
            {
                temp.SetNewActivity(sd.playerName + " odlazi kod doktora");
            }
            else if (isStudent)
            {
                temp.SetNewActivity(sd.playerName + " odlazi kod studenta medicine na nešto jeftinije liječenje");
            }
            else if (isAlt)
            {
                temp.SetNewActivity(sd.playerName + " traži lijek koristeći alternativnu medicinu");
            }
            SceneManager.LoadScene("MainGame");
        }
    }
}
