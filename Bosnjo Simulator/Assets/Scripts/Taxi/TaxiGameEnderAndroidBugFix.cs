using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxiGameEnderAndroidBugFix : MonoBehaviour
{
    [SerializeField] Taxi taxi;
    private AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        taxi = FindObjectOfType<Taxi>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(audio != null)
        {
            audio.Play();
        }
        taxi.hits--;
        taxi.damageSlider.value += 0.25f;
        taxi.FinishGame();
    }
}
