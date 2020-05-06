using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MealDrink : MonoBehaviour
{
    [SerializeField] float mealOrDrinkPrice = 5f;
    [SerializeField] float mealOrDrinkEnergyValue = 0.5f;
    [SerializeField] TMPro.TextMeshProUGUI mealOrDrinkPriceText;
    [SerializeField] UnityEngine.UI.Slider mealOrDrinkEnergySlider;
    [SerializeField] TMPro.TextMeshProUGUI mealOrdrinkNameText;
    [SerializeField] bool isAlcohol;

    [SerializeField] GameObject error;

    private void Awake()
    {
        mealOrDrinkPriceText.text = "KUPI " + mealOrDrinkPrice.ToString("#.##") + "KM";
        mealOrDrinkEnergySlider.value = mealOrDrinkEnergyValue;
    }

    public void OnBuyMealButtonClick()
    {
        Gameplay gameplay = FindObjectOfType<Gameplay>();
        SaveData sd = gameplay.GetSaveData();
        if(sd.playerMoney < mealOrDrinkPrice)
        {
            error.SetActive(true);
        }
        else
        {
            sd.playerHunger = Mathf.Clamp(sd.playerHunger + mealOrDrinkEnergyValue, 0f, 1f);
            sd.playerMoney -= mealOrDrinkPrice;


            string[] opcije = { "Najeo se i legao da odspava.", "Sjelo mu je baš dobro.", "Kaže da bi on to bolje spremio.",
                "Mogao bi još jednu porciju pojesti komotno.", "Bilo je ukusno.", "Tražio je više kečapa.", "Najeo se i trlja stomak" };
            gameplay.SetNewActivity(sd.playerName + " kupuje " + PrilagodiIme(mealOrdrinkNameText.text) + ". " + opcije[UnityEngine.Random.Range(0, opcije.Length)]);

            SceneManager.LoadScene("MainGame");
        }
    }

    public void OnBuyDrinkButtonClick()
    {
        Gameplay gameplay = FindObjectOfType<Gameplay>();
        SaveData sd = gameplay.GetSaveData();
        if (sd.playerMoney < mealOrDrinkPrice)
        {
            error.SetActive(true);
        }
        else
        {
            sd.playerThirst = Mathf.Clamp(sd.playerThirst + mealOrDrinkEnergyValue, 0f, 1f);
            sd.playerMoney -= mealOrDrinkPrice;

            if (isAlcohol)
            {
                string[] opcije = { "Popio je previše.", "Napio se i skače.", "Napio se i zaspao.", "Častio je društvo pićem.", "Pije, ali se boji dna flaše." };
                gameplay.SetNewActivity(sd.playerName + " kupuje " + PrilagodiIme(mealOrdrinkNameText.text) + ". " + opcije[UnityEngine.Random.Range(0, opcije.Length)]);
            }
            else
            {
                gameplay.SetNewActivity(sd.playerName + " kupuje " + PrilagodiIme(mealOrdrinkNameText.text) + " da se osvježi.");
            }
            
            SceneManager.LoadScene("MainGame");
        }
    }

    private string PrilagodiIme(string ime)
    {
        if (ime == "Vino")
        {
            return "Vino";
        }
        if (ime == "Pivo")
        {
            return "Pivo";
        }
        if (ime[ime.Length - 1] == 'a' || ime[ime.Length - 1] == 'o')
        {
            return ime.Substring(0, ime.Length - 1) + "u";
        }

        return ime;
    }
}
