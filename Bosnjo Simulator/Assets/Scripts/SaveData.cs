using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public float playerMoney;
    public float playerHealth;
    public float playerHappiness;
    public float playerHunger;
    public float playerThirst;

    public bool hasKredit;
    public float kreditValue;

    public bool hasPassport;
    public bool hasVisa;

    public string playerName;
    public string playerSurname;

    public List<string> playerLatestActivities;
    public List<string> playerCars;
    public List<string> playerHomes;

    public List<string> playerFinishedFaculties;
    public List<string> playerCurrentFaculties;
    public List<int> playerCurrentFacultiesYear;

    public string playerTShirtColor;
    public string playerShoesColor;
    
    public void SetDefaults()
    {
        this.playerName = "";
        this.playerSurname = "";
        this.playerMoney = 100f;
        this.playerHealth = 1f;
        this.playerHappiness = 1f;
        this.playerHunger = 1f;
        this.playerThirst = 1f;

        hasKredit = false;
        kreditValue = 0f;

        hasPassport = false;
        hasVisa = false;

        playerCars = new List<string>();
        playerHomes = new List<string>();

        playerFinishedFaculties = new List<string>();
        playerCurrentFaculties = new List<string>();
        playerCurrentFacultiesYear = new List<int>();

        playerLatestActivities = new List<string>();
        this.playerLatestActivities.Add("Novi Bošnjo je Stvoren");
        this.playerLatestActivities.Add(" ");
        this.playerLatestActivities.Add(" ");
        this.playerLatestActivities.Add(" ");
        this.playerLatestActivities.Add(" ");

        playerTShirtColor = "#" + ColorUtility.ToHtmlStringRGBA(Color.green);
        playerShoesColor = "#" + ColorUtility.ToHtmlStringRGBA(Color.gray);
    }
}
