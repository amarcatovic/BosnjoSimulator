using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kolizija : MonoBehaviour
{
    [SerializeField]  GameObject FireworksAll;
    [SerializeField] GameObject gordo;
    [SerializeField] GameObject[] podloge;
    private AudioSource _audio;

    private void Awake()
    {
        _audio = gameObject.GetComponent<AudioSource>();
    }

    void Start()
    {
        Debug.Log(podloge[0]);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(gordo.GetComponent<Rigidbody2D>().velocity.y<=0f && collision.gameObject.name.StartsWith("Gordo"))
        {
            if (_audio != null)
            {
                if (_audio.isPlaying) return;
                _audio.Play();
            }
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 450);
            Explode();

        }
        if ((collision.gameObject.name.StartsWith("hammer-2669857_1280")|| collision.gameObject.name.StartsWith("12")))
        {
            Explode();
            collision.gameObject.SetActive(false);
        }

    }
    
    
    void Explode()
    {
        GameObject firework = Instantiate(FireworksAll, gameObject.transform.position, Quaternion.identity);
        firework.GetComponent<ParticleSystem>().Play();
    }

}
