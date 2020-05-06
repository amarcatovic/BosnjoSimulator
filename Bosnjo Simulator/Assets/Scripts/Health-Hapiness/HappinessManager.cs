using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HappinessManager : MonoBehaviour
{
    [SerializeField] private string tripReport;
    [SerializeField] Slider playerCurrentHealthSlider;
    SaveData sd;
    public string tripType;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        sd = FindObjectOfType<Gameplay>().GetSaveData();
        playerCurrentHealthSlider.value = sd.playerHappiness;
    }

    public string GetTripReport()
    {
        return this.tripReport;
    }

    public void SetTripReport(string tripReport)
    {
        this.tripReport = tripReport;
        FindObjectOfType<Gameplay>().SetNewActivity(this.tripReport);
    }
}
