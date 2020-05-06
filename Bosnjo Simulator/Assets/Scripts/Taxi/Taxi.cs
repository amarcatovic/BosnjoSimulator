using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Taxi : MonoBehaviour
{
    private AudioSource sound;
    [SerializeField] AudioClip[] engineSounds;

    public VariableJoystick variableJoystick;
    private Rigidbody2D rb;

    [SerializeField] public Slider damageSlider;
    [SerializeField] public int hits = 0;

    [SerializeField] float accelerationPower = 1f;
    [SerializeField] float steeringPower = 1f;
    float steeringAmount, speed, direction;

    private bool playing = true;
    private bool playingIdle = false;

    Gameplay gp;
    SaveData sd;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
        sound.clip = engineSounds[0];
        sound.Play();
        rb = GetComponent<Rigidbody2D>();
        damageSlider.value = 1f;
        gp = FindObjectOfType<Gameplay>();
        sd = gp.GetSaveData();
    }

    void FixedUpdate()
    {
        steeringAmount = -variableJoystick.Horizontal;
        speed = variableJoystick.Vertical * accelerationPower;
        direction = Mathf.Sign(Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up)));
        rb.rotation += steeringAmount * steeringPower * rb.velocity.magnitude * direction;

        rb.AddRelativeForce(Vector2.up * speed);
        rb.AddRelativeForce(-Vector2.right * rb.velocity.magnitude * steeringAmount / 2);

        if(speed == 0 && !playingIdle)
        {
            playingIdle = true;
            playing = false;
            sound.clip = engineSounds[1];
            sound.loop = true;
            sound.Play();
        }
        else if(speed != 0 && !playing)
        {
            playingIdle = false;
            playing = true;
            sound.loop = true;
            sound.clip = engineSounds[2];
            sound.Play();
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        FinishGame();
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        FinishGame();
    }*/

    public void FinishGame()
    {
        gp = FindObjectOfType<Gameplay>();
        sd = gp.GetSaveData();

        float moneyEarned = (4 - hits) * 10;
        string text = "";
        gp.KreditManager(ref moneyEarned, ref text);

        if (sd.playerCars.Count > 0)
        {
            moneyEarned += Random.Range(10, 50);
            gp.SetNewActivity(sd.playerName + " prevozi mušteriju u svom taksiju, " + sd.playerCars[Random.Range(0, sd.playerCars.Count)] + ", i zaradio je " + moneyEarned.ToString() + "KM. " + text);
        }
        else
        {
            gp.SetNewActivity(sd.playerName + " prevozi mušteriju u taksiju i zaradio je " + moneyEarned.ToString() + "KM. " + text);
        }


        sd.playerMoney += moneyEarned;
        sd.playerHappiness -= 0.05f;
        sd.playerHunger -= 0.05f;
        sd.playerHealth -= 0.05f;
        FindObjectOfType<GameSaveLogic>().Save();
        SceneManager.LoadScene("MainGame");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        damageSlider.value -= 0.25f;
        ++hits;
        if(hits >= 4)
        {
            sd.playerMoney -= 5;
            sd.playerHappiness -= 0.1f;
            sd.playerHunger -= 0.05f;
            sd.playerHealth -= 0.05f;
            gp.SetNewActivity(sd.playerName + " je loše vozio taksi pa je mušterija pobjegla");
            FindObjectOfType<GameSaveLogic>().Save();
            SceneManager.LoadScene("MainGame");
        }
    }
}
