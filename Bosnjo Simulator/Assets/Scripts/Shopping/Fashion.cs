using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fashion : MonoBehaviour
{
    [SerializeField] GameObject bosnjoTShirt;
    [SerializeField] GameObject bosnjoShoe1;
    [SerializeField] GameObject bosnjoShoe2;
    [SerializeField] SpriteRenderer bosnjoTShirtSpriteRenderer;
    [SerializeField] SpriteRenderer bosnjoShoe1SpriteRenderer;
    [SerializeField] SpriteRenderer bosnjoShoe2SpriteRenderer;
    [SerializeField] Color bosnjoCurrentTShirtColor;
    [SerializeField] Color bosnjoCurrentShoesColor;
    [SerializeField] GameObject error;

    [SerializeField] TextMeshProUGUI tshirtPriceText;
    [SerializeField] TextMeshProUGUI shoePriceText;

    SaveData sd;

    List<Color> tShirtColors = new List<Color>{ Color.red, Color.blue, Color.yellow, Color.black, Color.green, Color.gray, Color.cyan };
    List<Color> shoesColors = new List<Color>{ Color.red, Color.blue, Color.yellow, Color.black, Color.green, Color.gray, Color.cyan };
    int tShirtIndex, shoesIndex;
    int startMajica = 0, startCipele = 0;
    int cijena = 0;

    void Start()
    {
        bosnjoTShirtSpriteRenderer = bosnjoTShirt.GetComponent<SpriteRenderer>();
        bosnjoShoe1SpriteRenderer = bosnjoShoe1.GetComponent<SpriteRenderer>();
        bosnjoShoe2SpriteRenderer = bosnjoShoe2.GetComponent<SpriteRenderer>();
        sd = FindObjectOfType<Gameplay>().GetSaveData();

        ColorUtility.TryParseHtmlString(sd.playerTShirtColor, out bosnjoCurrentTShirtColor);
        ColorUtility.TryParseHtmlString(sd.playerShoesColor, out bosnjoCurrentShoesColor);

        Debug.Log(tShirtColors.IndexOf(bosnjoCurrentTShirtColor));

        bosnjoTShirtSpriteRenderer.color = bosnjoCurrentTShirtColor;
        bosnjoShoe1SpriteRenderer.color = bosnjoCurrentShoesColor;
        bosnjoShoe2SpriteRenderer.color = bosnjoCurrentShoesColor;

        startMajica = tShirtIndex = tShirtColors.IndexOf(bosnjoCurrentTShirtColor);
        startCipele = shoesIndex = shoesColors.IndexOf(bosnjoCurrentShoesColor);
    }

    private void Update()
    {
        if(startMajica != tShirtIndex)
        {
            tshirtPriceText.text = "50KM";
        }
        else
        {
            tshirtPriceText.text = "0KM";
        }
        
        if(startCipele != shoesIndex)
        {
            shoePriceText.text = "100KM";
        }
        else
        {
            shoePriceText.text = "0KM";
        }

        if (startMajica != 0 && startCipele != 0)
            cijena = 150;
        else if (startMajica == 0 && startCipele != 0)
            cijena = 100;
        else if (startMajica != 0 && startCipele == 0)
            cijena = 50;
        else
            cijena = 0;
    }

    //T-Shirts
    public void OnLeftButtonTShirtClick()
    {
        --tShirtIndex;
        if (tShirtIndex < 0)
        {
            tShirtIndex = tShirtColors.Count - 1;
        }

        bosnjoTShirtSpriteRenderer.color = tShirtColors[tShirtIndex];
    }

    public void OnRightButtonTShirtClick()
    {
        tShirtIndex = (tShirtIndex + 1) % tShirtColors.Count;
        bosnjoTShirtSpriteRenderer.color = tShirtColors[tShirtIndex];
    }

    //Shoes
    public void OnLeftButtonShoesClick()
    {
        --shoesIndex;
        if (shoesIndex < 0)
        {
            shoesIndex = shoesColors.Count - 1;
        }

        bosnjoShoe1SpriteRenderer.color = shoesColors[shoesIndex];
        bosnjoShoe2SpriteRenderer.color = shoesColors[shoesIndex];
    }

    public void OnRightButtonShoesClick()
    {
        shoesIndex = (shoesIndex + 1) % shoesColors.Count;
        bosnjoShoe1SpriteRenderer.color = shoesColors[shoesIndex];
        bosnjoShoe2SpriteRenderer.color = shoesColors[shoesIndex];
    }

    //Buy
    public void OnBuyButtonClick()
    {
        if(sd.playerMoney < cijena)
        {
            error.SetActive(true);
        }
        else
        {
            sd.playerMoney -= cijena;
            sd.playerTShirtColor = "#" + ColorUtility.ToHtmlStringRGBA(bosnjoTShirtSpriteRenderer.color);
            sd.playerShoesColor = "#" + ColorUtility.ToHtmlStringRGBA(bosnjoShoe1SpriteRenderer.color);

            if(startCipele != shoesIndex && startMajica != tShirtIndex)
                FindObjectOfType<Gameplay>().SetNewActivity(sd.playerName + " se ponovio kupivši novu majicu i patike");

            if (startCipele == shoesIndex && startMajica != tShirtIndex)
                FindObjectOfType<Gameplay>().SetNewActivity(sd.playerName + " se ponovio kupivši novu majicu");

            if (startCipele != shoesIndex && startMajica == tShirtIndex)
                FindObjectOfType<Gameplay>().SetNewActivity(sd.playerName + " se ponovio kupivši nove patike");


            FindObjectOfType<GameSaveLogic>().Save();
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
        }
    }



}
