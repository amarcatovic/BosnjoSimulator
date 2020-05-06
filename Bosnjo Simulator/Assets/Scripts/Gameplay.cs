using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Gameplay : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] AudioClip[] clips;

    //Config Params
    GameSaveLogic gsl;

    public List<string> stackScena; 
    private List<string> stackGameScena; 

    // Game Params
    [SerializeField] SaveData saveData;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        PlayMusic();


        if (FindObjectsOfType<Gameplay>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        saveData = new SaveData();
        gsl = FindObjectOfType<GameSaveLogic>();
        if (gsl.SaveGameExists())
        {
            gsl.Load();
        }
    }

    private void Update()
    {
        var scene = SceneManager.GetActiveScene().name;
        if (scene == "AboutScene" || scene == "Home" || scene == "HelpScene"
            || scene == "MainGame" || scene == "WorkPanel" || scene == "Activities" || scene == "shoppingMain" || scene == "PapiriMain" || scene == "Nekretnine" ||
            scene == "Kladionica" || scene == "Banka")
        {
            _audioSource.clip = clips[0];
            PlayMusic();
        }
        else if(scene == "Scena" || scene == "MoneyCollectorGame" || scene == "VodoinstalaterLevel01" || scene == "VodoinstalaterLevel02" || scene == "VodoinstalaterLevel03"
            || scene == "VodoinstalaterLevel04" || scene == "VodoinstalaterLevel05" || scene == "VodoinstalaterLevel06" || scene == "WorkFromHome01" || scene == "FabrikaGame" || scene == "DomaciProizvodi")
        {
            _audioSource.clip = clips[1];
            PlayMusic();
        }
        else if(scene == "buyCars" || scene == "SkelaIntro" || scene == "buyHomes" || scene == "buyLuxCars" || scene == "buyOldCars" || scene == "buyNewCars" || scene == "domaciProizvodiIntro" || scene == "NekretnineAutomobili" ||
            scene == "NekretnineStanovi" || scene == "BankaReturn" || scene == "buyDrinks" || scene == "buyFood" || scene == "Doctor" || scene == "Happiness" || scene == "Passport" || scene == "Visa" || scene == "DomaciProizvodiIntro" ||
            scene == "Tiket" || scene == "Bingo" || scene == "TrkeKamiona" || scene == "MoneyCollectorGameScore" || scene == "VodoinstalaterKraj" || scene == "buyClothes" || scene == "buyDrinks" || scene == "buyFood"
            || scene == "FakultetMenu" || scene == "FakultetBeforeGame" || scene == "TaxiIntro")
        {
            _audioSource.clip = clips[2];
            PlayMusic();
        }
        else
        {
            StopMusic();
        }

        if(scene == "MemoryGame")
        {
            Screen.orientation = ScreenOrientation.Landscape;
        }
        else
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && scene == "MainGame")
        {
            SceneManager.LoadScene("Home");
        }
    }

    // returns string of how many money went into morgage
    public void KreditManager(ref float money, ref string text)
    {
        if (saveData.hasKredit)
        {
            if(money > saveData.kreditValue)
            {
                money -= saveData.kreditValue;
                saveData.hasKredit = false;
                saveData.playerMoney -= saveData.kreditValue;
                saveData.kreditValue = 0;
                text = "Vratio je kredit.";
            }
            else
            {
                saveData.kreditValue -= money / 10;
                text = "Rata na kredit iznosi " + (money / 10).ToString("#.##") + "KM. Ostalo za vratiti: " + saveData.kreditValue.ToString("#.##") + "KM.";
                money -= money / 10;
            }
        }
        else
        {
            text = "";
        }
    }
    private string readTextFile(string file_path)
    {
        StreamReader inp_stm = new StreamReader(file_path);
        return inp_stm.ReadLine();
    }
    public void SetNewActivity(string activityText)
    {
        for(int i = saveData.playerLatestActivities.Count - 1; i >= 1; i--)
        {
            saveData.playerLatestActivities[i] = saveData.playerLatestActivities[i - 1];
        }

        saveData.playerLatestActivities[0] = activityText;
        gsl.Save();
    }
    public void SetSaveData(SaveData sd) { saveData = sd; }
    public SaveData GetSaveData() { return saveData; }

    public void PlayMusic()
    {
        if(_audioSource != null)
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
