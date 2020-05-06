using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proizvod : MonoBehaviour
{
    public bool isFabrika = false;
    private AudioSource _audioSource;
    [SerializeField] AudioClip[] clips;

    [SerializeField] bool isDomaci = false;
    DomaciProizvodiManager manager;

    [SerializeField] Sprite[] domaciProizvodiSprites;
    [SerializeField] Sprite[] straniProizvodiSprites;

    public float appearInSec = 1f;
    public float beActiveForSec = 3f;

    [SerializeField]  public float timePassedActive = 0f;
    [SerializeField]  public float timePassedAppear = 0f;

    [SerializeField] bool active = false;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        manager = FindObjectOfType<DomaciProizvodiManager>();
    }

    private void Update()
    {
        if(timePassedAppear >= appearInSec && !active)
        {
            SpriteGetter();
            timePassedActive = 0f;
            timePassedAppear = 0f;
            active = true;
        }

        if(timePassedActive >= beActiveForSec && active)
        {
            active = false;
            timePassedAppear = 0;
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
            if(isDomaci)
            {
                manager.score--;
            }
        }

        timePassedActive += Time.fixedDeltaTime;
        if(!active)
        {
            timePassedAppear += Time.fixedDeltaTime;
        }
        
    }

    private void OnMouseDown()
    {
        if(isFabrika)
        {
            if (isDomaci && active)
            {
                manager.score += UnityEngine.Random.Range(1f, 5f);
                _audioSource.clip = clips[0];
                PlayMusic();
            }
               
            else if (!isDomaci && active)
            {
                manager.score--;
                _audioSource.clip = clips[1];
                PlayMusic();
            }         
        }
        else
        {
            if (isDomaci && active)
            {
                manager.score++;
                _audioSource.clip = clips[0];
                PlayMusic();
            }
                
            else if (!isDomaci && active)
            {
                manager.score -= .5f;
                _audioSource.clip = clips[1];
                PlayMusic();
            }
        }

        

        active = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = null;
    }

    private void SpriteGetter()
    {
        int choice = UnityEngine.Random.Range(1, 3);
        if(choice == 1)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = domaciProizvodiSprites[UnityEngine.Random.Range(0, domaciProizvodiSprites.Length)];
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
            isDomaci = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = straniProizvodiSprites[UnityEngine.Random.Range(0, straniProizvodiSprites.Length)];
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
            isDomaci = false;
        }
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

}
