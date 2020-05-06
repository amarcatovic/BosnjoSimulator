using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cijev : MonoBehaviour
{
    [SerializeField] bool isCorrect = false;
    [SerializeField] bool isTwoWay = false;
    private AudioSource _audioSource;

    private void Start()
    {
        PipeCheck();
        _audioSource = GetComponent<AudioSource>();
    }

    void OnMouseDown()
    {
        transform.Rotate(0f, 0f, 90f);
        PlayMusic();
        PipeCheck();
    }

    private void PipeCheck()
    {
        if (!isTwoWay)
        {
            if (transform.rotation.z == 0)
            {
                isCorrect = true;
            }
            else
            {
                isCorrect = false;
            }
        }
        else
        {

            if (transform.rotation.z == 0 || transform.rotation.eulerAngles.z == 180)
            {
                isCorrect = true;
            }
            else
            {
                isCorrect = false;
            }
        }
    }

    public bool GetCorrect() { return isCorrect; }

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
