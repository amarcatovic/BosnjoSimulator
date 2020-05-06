using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DoctorManager : MonoBehaviour
{
    [SerializeField] Slider playerCurrentHealthSlider;
    SaveData sd;

    private void Start()
    {
        sd = FindObjectOfType<Gameplay>().GetSaveData();
        playerCurrentHealthSlider.value = sd.playerHealth;
    }

    public void OnGoBackButtonClick()
    {
        SceneManager.LoadScene("MainGame");
    }

}
