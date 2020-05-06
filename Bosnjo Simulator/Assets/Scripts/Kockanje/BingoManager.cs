using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BingoManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI number1Text;
    [SerializeField] public int number1 = 1;
    [SerializeField] TextMeshProUGUI number2Text;
    [SerializeField] public int number2 = 2;
    [SerializeField] TextMeshProUGUI number3Text;
    [SerializeField] public int number3 = 3;
    private new AudioSource audio;

    [SerializeField] GameObject errorPanel;
    [SerializeField] TextMeshProUGUI errorText;

    [SerializeField] TextMeshProUGUI moneyInvestedText;
    [SerializeField] public float moneyInvested = 1f;

    Gameplay gameplay;
    SaveData sd;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        gameplay = FindObjectOfType<Gameplay>();
        sd = gameplay.GetSaveData();
    }

    public void OnBeginGameButtonClick()
    {
        audio.Play();
        if (number1 != number2 && number3 != number1)
        {
            sd.playerMoney -= moneyInvested;
            FindObjectOfType<GameSaveLogic>().Save();
            SceneManager.LoadScene("BingoIzvlacenje");
        }
        else if(sd.playerMoney < moneyInvested)
        {
            errorPanel.SetActive(true);
            errorText.text = sd.playerName + " nema dovoljno novca";
        }
        else
        {
            errorPanel.SetActive(true);
            errorText.text = "Izaberite različite brojeve";
        }
    }
    
    public void OnNumberOneUpButtonClick()
    {
        audio.Play();
        ++number1;
        number1Text.text = number1.ToString();
    }

    public void OnNumberOneDownButtonClick()
    {
        audio.Play();
        if (number1 > 1)
        {
            --number1;
            number1Text.text = number1.ToString();
        }
    }

    public void OnNumberTwoUpButtonClick()
    {
        audio.Play();
        ++number2;
        number2Text.text = number2.ToString();
    }

    public void OnNumberTwoDownButtonClick()
    {
        audio.Play();
        if (number2 > 1)
        {
            --number2;
            number2Text.text = number2.ToString();
        }
    }

    public void OnNumberThreeUpButtonClick()
    {
        audio.Play();
        ++number3;
        number3Text.text = number3.ToString();
    }

    public void OnNumberThreeDownButtonClick()
    {
        audio.Play();
        if (number3 > 1)
        {
            --number3;
            number3Text.text = number3.ToString();
        }
    }

    public void OnPlusButtonClick()
    {
        audio.Play();
        if (number1 <= 43)
        {
            moneyInvested += 1f;
            moneyInvestedText.text = moneyInvested.ToString("#") + " KM";
        }

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
