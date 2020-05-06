using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NekretnineStanovi : MonoBehaviour
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
            "Šator", "Prikolica", "Garsonjera", "Jednosoban Stan", "Dvosoban Stan", "Trosoban stan", "Porodična Kuća", "Vila", "Vikendica"
        };

        carPrices = new List<float>()
        {
            10000f, 20000f, 40000f, 50000f, 70000f, 80000f, 90000f, 300000f, 100000f
        };

        sd = FindObjectOfType<Gameplay>().GetSaveData();

        if (sd.playerHomes.Count > 0)
        {
            nothing.SetActive(false);
        }

        for (short i = 0; i < sd.playerHomes.Count; ++i)
        {
            int index = carNames.IndexOf(sd.playerHomes[i]);

            panels[i].SetActive(true);
            texts[i].text = sd.playerHomes[i];
            images[i].sprite = carPhotos[index];
            carInfos[i].carName = carNames[index];
            carInfos[i].carPrice = carPrices[index];
        }
    }
}
