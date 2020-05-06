using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TruckBetting : MonoBehaviour
{
    [SerializeField] GameObject errorPanel;
    [SerializeField] Image truckImg;

    [SerializeField] TextMeshProUGUI moneyInvestedText;
    [SerializeField] public float moneyInvested;
    [SerializeField] public string playerTruckChoice = "Žuti";
    private new AudioSource audio;

    private List<Color> colors = new List<Color>() { Color.yellow, Color.red, Color.blue, Color.green };
    private List<string> truckNames = new List<string>() { "Žuti", "Crveni", "Plavi", "Zeleni" };
    private int index = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        audio = GetComponent<AudioSource>();
    }

    public void OnNextButtonClick()
    {
        audio.Play();
        SaveData sd = FindObjectOfType<Gameplay>().GetSaveData();
        if (sd.playerMoney < moneyInvested)
        {
            errorPanel.SetActive(true);
        }
        else
        {
            sd.playerMoney -= moneyInvested;
            FindObjectOfType<GameSaveLogic>().Save();
            SceneManager.LoadScene("TruckRace");
        }
    }

    public void LeftButtonClick()
    {
        audio.Play();
        if (index == 0)
        {
            index = 3;
        }
        else
        {
            --index;
        }

        truckImg.color = colors[index];
        playerTruckChoice = truckNames[index];
    }

    public void RightButtonClick()
    {
        audio.Play();
        index = (index + 1) % 4;
        truckImg.color = colors[index];
        playerTruckChoice = truckNames[index];
    }

    public void OnPlusButtonClick()
    {
        audio.Play();
        moneyInvested += 1f;
        moneyInvestedText.text = moneyInvested.ToString("#") + " KM";

    }

    public void OnMinusButtonClick()
    {
        audio.Play();
        if (moneyInvested > 1f)
        {
            moneyInvested -= 1f;
            moneyInvestedText.text = moneyInvested.ToString("#") + " KM";
        }

    }

}
