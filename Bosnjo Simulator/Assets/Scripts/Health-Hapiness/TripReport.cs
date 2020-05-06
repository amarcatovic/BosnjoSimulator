using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TripReport : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI tripReportMainText;
    [SerializeField] GameObject bosnjo;
    private Animator b1;

    private AudioSource _audioSource;
    public AudioClip[] clips;

    private HappinessManager hm;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        hm = FindObjectOfType<HappinessManager>();
        tripReportMainText.text = hm.GetTripReport();
        b1 = bosnjo.GetComponent<Animator>();

        if(hm.tripType == "Koncert" || hm.tripType == "Klub")
        {
            b1.SetTrigger("Dance");
            _audioSource.clip = clips[0];
            PlayMusic();
        }
        else if(hm.tripType == "Utakmica"){
            // b1.SetTrigger("Jump_01");
            _audioSource.clip = clips[1];
            PlayMusic();
           StartCoroutine(UtakmicaAnimacija());
            
        }
        else
        {
            b1.SetTrigger("Walk_01");
            _audioSource.clip = clips[2];
            PlayMusic();
        }
    }

    public void OnOkButtonClick()
    {
        Destroy(FindObjectOfType<HappinessManager>().gameObject);
        SceneManager.LoadScene("MainGame");
    }

    public void PlayMusic()
    {
        if (_audioSource != null)
        {
            if (_audioSource.isPlaying) return;
            _audioSource.Play();
        }
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }

    IEnumerator UtakmicaAnimacija()
    {
        b1.SetTrigger("Jump_01");
        yield return new WaitForSeconds(1);
        b1.SetTrigger("Idle_01");
    }
}
