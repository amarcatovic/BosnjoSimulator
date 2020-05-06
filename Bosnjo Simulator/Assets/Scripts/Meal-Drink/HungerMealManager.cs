using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HungerMealManager : MonoBehaviour
{
    [SerializeField] Slider playerCurrentHungerSlider;
    [SerializeField] Slider playerCurrentThirstSlider;
    SaveData sd;

    private void Start()
    {
        sd = FindObjectOfType<Gameplay>().GetSaveData();
        if(playerCurrentHungerSlider != null)
            playerCurrentHungerSlider.value = sd.playerHunger;
        if (playerCurrentThirstSlider != null)
            playerCurrentThirstSlider.value = sd.playerThirst;
    }

    public void OnGoBackButtonClick()
    {
        SceneManager.LoadScene("Activities");
    }

}
