using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NekretnineAutomobili : MonoBehaviour
{
    [SerializeField] GameObject nothing;
    [SerializeField] GameObject[] panels;
    [SerializeField] TextMeshProUGUI[] texts;
    [SerializeField] Image[] images;
    [SerializeField] Cars[] carInfos;

    public List<string> carNames;
    public List<float> carPrices;
    public List<Sprite> carPhotos;

    SaveData sd;

    void Start()
    {
        carNames = new List<string>()
        {
            "Peglica", "Yugo", "Golf 2", "Golf 3", "Golf 4", "Renault Megane", "Alfa Romeo 147", "Golf 5", "Škoda", "Audi A3", "Golf 6", "Mercedes A Klasa",
            "Mercedes B Klasa", "Golf 7", "BMW M5", "Audi Q7", "Porsche Cayane", "BMW X6", "Audi A5", "Mercedes C63 AMG", "Volvo XC 90", "Audi R8", "Ferrari Lusso GT", "Lada"
        };

        carPrices = new List<float>()
        {
            750f, 1000f, 2000f, 3000f, 4000f, 5000f, 6000f, 8000f, 10000f, 18000f, 20000f, 25000f, 28000f, 30000f, 40000f, 50000f, 60000f, 70000f, 80000f, 90000f, 100000f, 120000f, 200000f, 300000f
        };

        sd = FindObjectOfType<Gameplay>().GetSaveData();
        if(sd.playerCars.Count > 0)
        {
            nothing.SetActive(false);
        }

        for(short i = 0; i < sd.playerCars.Count; ++i)
        {
            int index = carNames.IndexOf(sd.playerCars[i]);

            panels[i].SetActive(true);
            texts[i].text = sd.playerCars[i];
            images[i].sprite = carPhotos[index];
            carInfos[i].carName = carNames[index];
            carInfos[i].carPrice = carPrices[index];
        }

        
}
}
