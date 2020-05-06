using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class BingoBrojevi : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerNum1Text;
    [SerializeField] TextMeshProUGUI playerNum2Text;
    [SerializeField] TextMeshProUGUI playerNum3Text;

    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] Button buttonDone;

    [SerializeField] TextMeshProUGUI[] numbersText;
    [SerializeField] Image[] images;

    [SerializeField] float waitForSeconds = 2f;
    [SerializeField] float secPassed = 2f;
    private float moneyEarned = 0f;

    BingoManager bm;
    List<int> numbers;
    int numCount = 0;
    bool finished = false;
    int matchCounter = 0;

    private new AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    void Start()
    {
        moneyEarned = 0f;
        bm = FindObjectOfType<BingoManager>();
        numbers = new List<int>();

        playerNum1Text.text = bm.number1.ToString();
        playerNum2Text.text = bm.number2.ToString();
        playerNum3Text.text = bm.number3.ToString();
        buttonDone.interactable = false;
    }

    void Update()
    {
        if(secPassed >= waitForSeconds && !finished)
        {
            audio.Play();
            images[numCount].enabled = true;
            int tempRandNum;

            do {
                tempRandNum = UnityEngine.Random.Range(1, 43);
            } while(numbers.Contains(tempRandNum));

            numbersText[numCount++].text = tempRandNum.ToString();
            numbers.Add(tempRandNum);

            secPassed = 0;
            if(numCount >= 15)
            {
                finished = true;
                buttonDone.interactable = true;

                if (numbers.Contains(bm.number1))
                    ++matchCounter;
                if (numbers.Contains(bm.number2))
                    ++matchCounter;
                if (numbers.Contains(bm.number3))
                    ++matchCounter;

                if (matchCounter == 0)
                {
                    priceText.text = "-" + bm.moneyInvested.ToString() + "KM";
                    moneyEarned = -bm.moneyInvested;
                }
                if (matchCounter == 1)
                {
                    priceText.text = bm.moneyInvested.ToString() + "KM";
                    moneyEarned = bm.moneyInvested;
                }
                if (matchCounter == 2)
                {
                    priceText.text = (bm.moneyInvested * 10).ToString() + "KM";
                    moneyEarned = bm.moneyInvested * 10;
                }   
                if (matchCounter == 3)
                {
                    priceText.text = (bm.moneyInvested * 50).ToString() + "KM";
                    moneyEarned = bm.moneyInvested * 50;
                }
            }
        }

        secPassed += Time.deltaTime;
    }

    public void DoneButtonClicked()
    {
        float money = moneyEarned;
        Gameplay gp = FindObjectOfType<Gameplay>();
        SaveData sd = gp.GetSaveData();
        sd.playerMoney += money;
        if(matchCounter == 0)
        {
            sd.playerHappiness -= 0.05f;
            gp.SetNewActivity(sd.playerName + " je izgubio " + money.ToString("#") + "KM  na bingu");
        }
        else
        {
            sd.playerHappiness += 0.05f;
            gp.SetNewActivity(sd.playerName + " je dobio " + money.ToString("#") + "KM na bingu");
        }

        FindObjectOfType<GameSaveLogic>().Save();
        Destroy(FindObjectOfType<BingoManager>().gameObject);
        SceneManager.LoadScene("Kladionica");
    }

    public void SimulateButton()
    {
        waitForSeconds = 0f;
    }
    
}
