using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VodoinstalaterManager : MonoBehaviour
{
    public bool isMaske = false;
    // Game config
    [SerializeField] Cijev[] cijevi;
    [SerializeField] public float vrijeme = 60f;
    public int counter = 0;

    [SerializeField] public bool success = false;
    [SerializeField] public bool finished = false;

    // UI
    [SerializeField] TMPro.TextMeshProUGUI secondsLeftText;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if(!finished)
            CheckGameStatus();
    }

    private void CheckGameStatus()
    {
        counter = 0;
        vrijeme -= Time.deltaTime;
        secondsLeftText.text = vrijeme.ToString("#.##");

        if (vrijeme < 0f && !finished)
        {
            success = false;
            finished = true;
            if(isMaske)
            {
                var gameplay = FindObjectOfType<Gameplay>();
                var sd = gameplay.GetSaveData();
                sd.playerMoney -= 10f;
                sd.playerHappiness -= 0.05f;
                sd.playerHunger -= 0.05f;
                sd.playerThirst -= 0.05f;
                gameplay.SetNewActivity(sd.playerName + " je pokušao raditi od kuće, ali nije uspio završiti na vrijeme i izgubio je 10KM na materijal");
                FindObjectOfType<GameSaveLogic>().Save();
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("VodoinstalaterKraj");
            }
        }

        for (int i = 0; i < cijevi.Length; ++i)
        {
            if (cijevi[i].GetCorrect())
            {
                ++counter;
            }
        }

        if (counter == cijevi.Length)
        {
            success = true;
            finished = true;
            if (isMaske)
            {
                var gameplay = FindObjectOfType<Gameplay>();
                var sd = gameplay.GetSaveData();
                float zarada = UnityEngine.Random.Range(10f, 30f);
                sd.playerMoney += zarada;
                sd.playerHappiness -= 0.05f;
                sd.playerHunger -= 0.05f;
                sd.playerThirst -= 0.05f;
                gameplay.SetNewActivity(sd.playerName + " je radio od kuće i zaradio " + zarada.ToString("#.##") + "KM");
                FindObjectOfType<GameSaveLogic>().Save();
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("VodoinstalaterKraj");
            }
        }
    }
}
