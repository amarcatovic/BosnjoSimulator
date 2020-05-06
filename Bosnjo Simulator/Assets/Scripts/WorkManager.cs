using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorkManager : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI loginText;

    private void Start()
    {
        string name = FindObjectOfType<Gameplay>().GetSaveData().playerName;
        loginText.text = "Dobrodošli " + char.ToUpper(name[0])  + FindObjectOfType<Gameplay>().GetSaveData().playerName.Substring(1);
    }

    public void OnGermanyWorkButtonClick()
    {
        SceneManager.LoadScene("MoneyCollectorGameIntro");
    }

    public void OnVodoinstalaterWorkButtonClick()
    {
        SceneManager.LoadScene("VodoinstalaterLoader");
    }

    public void OnTaxiButtonClick()
    {
        SceneManager.LoadScene("TaxiIntro");
    }
}
