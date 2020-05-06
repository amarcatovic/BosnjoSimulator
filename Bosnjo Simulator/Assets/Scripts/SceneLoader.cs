using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] GameObject errorPanel;
    [SerializeField] TMPro.TextMeshProUGUI errorText;

    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene((currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }

    public void LoadPreviousScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex - 1);
    }

    public void OnPlayGameButtonClick()
    {
        if(FindObjectOfType<GameSaveLogic>().SaveGameExists())
        {
            PlayMusic();
            SceneManager.LoadScene("MainGame");
        }
        else
        {
            PlayMusic();
            SceneManager.LoadScene("newPlayer");
        }
    }

    public void LoadHomeScene()
    {
        PlayMusic();
        SceneManager.LoadScene("Home");
    }

    public void LoadGameScene()
    {
        PlayMusic();
        SceneManager.LoadScene("MainGame");
    }

    public void LoadHelpScene()
    {
        PlayMusic();
        SceneManager.LoadScene("HelpScene");
    }

    public void LoadAboutScene()
    {
        PlayMusic();
        SceneManager.LoadScene("AboutScene");
    }

    public void LoadIntro()
    {
        PlayMusic();
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadNewPlayer()
    {
        PlayMusic();
        SceneManager.LoadScene("newPlayer");
    }

    public void OnQuitButtonClick()
    {
        PlayMusic();
        Application.Quit();
    }

    public void FabrikaJob()
    {
        PlayMusic();
        SceneManager.LoadScene("Fabrika");
    }

    public void WorkFromHomeJob()
    {
        PlayMusic();
        SceneManager.LoadScene("WorkFromHomeJob");
    }

    public void PromijeniIme()
    {
        PlayMusic();
        SceneManager.LoadScene("changePlayerName");
    }


    // ----------------------------------------------
    //         LOWER MENU IN MAIN GAME
    // ----------------------------------------------

    public void OnWorkButtonClick()
    {
        PlayMusic();
        SceneManager.LoadScene("WorkPanel");
    }

    public void OnShoppingButtonClick()
    {
        PlayMusic();
        SceneManager.LoadScene("shoppingMain");
    }

    public void OnActivitiesButtonClick()
    {
        PlayMusic();
        SceneManager.LoadScene("Activities");
    }

    public void OnPassportButtonClick()
    {
        PlayMusic();
        SceneManager.LoadScene("PapiriMain");
    }

    public void OnNekretnineButtonClick()
    {
        PlayMusic();
        SceneManager.LoadScene("Nekretnine");
    }

    // ----------------------------------------------
    //                  NEKRETNINE
    // ----------------------------------------------

    public void OnNekretnineAutomobiliButtonClick()
    {
        PlayMusic();
        SceneManager.LoadScene("NekretnineAutomobili");
    }

    public void OnNekretnineStanoviButtonClick()
    {
        PlayMusic();
        SceneManager.LoadScene("NekretnineStanovi");
    }

    // ----------------------------------------------
    //                  AKTIVNOSTI
    // ----------------------------------------------

    public void OnKladionicaButtonClick()
    {
        PlayMusic();
        SceneManager.LoadScene("Kladionica");
    }

    public void OnFakultetButtonClick()
    {
        PlayMusic();
        SceneManager.LoadScene("FakultetMenu");
    }

    public void OnBankaButtonClick()
    {
        PlayMusic();
        SceneManager.LoadScene("Banka");
    }

    public void OnBankaReturnButtonClick()
    {
        PlayMusic();
        SceneManager.LoadScene("BankaReturn");
    }

    // ----------------------------------------------
    //                  SHOPPING
    // ----------------------------------------------

    public void OnBuyCarsButtonClick()
    {
        PlayMusic();
        SceneManager.LoadScene("buyCars");
    }

    public void OnOldCarsButtonClick()
    {
        PlayMusic();
        SceneManager.LoadScene("buyOldCars");
    }

    public void OnNewCarsButtonClick()
    {
        PlayMusic();
        SceneManager.LoadScene("buyNewCars");
    }

    public void OnLuxCarsButtonClick()
    {
        PlayMusic();
        SceneManager.LoadScene("buyLuxCars");
    }

    public void OnBuyHomesButtonClick()
    {
        PlayMusic();
        SceneManager.LoadScene("buyHomes");
    }

    public void OnBuyClothesButtonClick()
    {
        PlayMusic();
        SceneManager.LoadScene("buyClothes");
    }

    public void OnDomaciProizvodiIntroClick()
    {
        PlayMusic();
        SceneManager.LoadScene("DomaciProizvodiIntro");
    }

    // ----------------------------------------------
    //                PASSPORT STUFF
    // ----------------------------------------------

    public void OnPassportChoiceButtonClick()
    {
        PlayMusic();
        SceneManager.LoadScene("Passport");
    }

    public void OnVisaButtonClick()
    {
        PlayMusic();
        SceneManager.LoadScene("Visa");
    }


    // ----------------------------------------------
    //                  PLAYER STATUS
    // ----------------------------------------------

    public void OnEatButtonClick()
    {
        PlayMusic();
        SceneManager.LoadScene("BuyFood");
    }

    public void OnDrinksButtonClick()
    {
        PlayMusic();
        SceneManager.LoadScene("BuyDrinks");
    }

    public void OnDoctorButtonClick()
    {
        PlayMusic();
        SceneManager.LoadScene("Doctor");
    }

    public void OnHappinessButtonClick()
    {
        PlayMusic();
        SceneManager.LoadScene("Happiness");
    }

    // ----------------------------------------------
    //                  PLAY GAMES
    // ----------------------------------------------

    // Takes 0.5 Energy and Requires Passport with or without Visa
    public void PlayCoinDropGame()
    {
        PlayMusic();
        SaveData sd = FindObjectOfType<Gameplay>().GetSaveData();
        sd.playerHealth -= 0.5f;
        sd.hasVisa = false;
        FindObjectOfType<GameSaveLogic>().Save();
        SceneManager.LoadScene("MoneyCollectorGame");
        
    }

    // Takes 0.25 Energy
    public void PlayVodoinstalerGame()
    {
        PlayMusic();
        SaveData sd = FindObjectOfType<Gameplay>().GetSaveData();
        if (sd.playerHealth <= 0.25f)
        {
            errorPanel.SetActive(true);
            errorText.text = "Niste dovoljno zdravi za ovaj posao";
        }
        else if(sd.playerHappiness <= 0.20f)
        {
            errorPanel.SetActive(true);
            errorText.text = "Previše vam je dosadno da bi radili";
        }
        else
        {
            sd.playerHealth -= 0.25f;
            FindObjectOfType<GameSaveLogic>().Save();
            int gameLevel = UnityEngine.Random.Range(1, 6);
            SceneManager.LoadScene("VodoinstalaterLevel0" + gameLevel);
        }
    }

    // Takes 0.05 Energy
    public void PlayTaxiGame()
    {
        PlayMusic();
        SaveData sd = FindObjectOfType<Gameplay>().GetSaveData();
        if (sd.playerHealth <= 0.05f)
        {
            errorPanel.SetActive(true);
            errorText.text = "Niste dovoljno zdravi za ovaj posao";
        }
        else if (sd.playerHappiness <= 0.1f)
        {
            errorPanel.SetActive(true);
            errorText.text = "Previše vam je dosadno da bi radili";
        }
        else
        {           
            int gameLevel = UnityEngine.Random.Range(1, 4);
            SceneManager.LoadScene("TaxiLevel0" + gameLevel);
        }
    }

    //Takes 0.1 Enery, Happiness, thirst and hunger if not compleated properly
    public void PlayDomaciProizvodiGame()
    {
        PlayMusic();
        SaveData sd = FindObjectOfType<Gameplay>().GetSaveData();
        if (sd.playerHealth <= 0.1f)
        {
            errorPanel.SetActive(true);
            errorText.text = "Niste dovoljno zdravi za kupovinu";
        }
        else if (sd.playerHunger <= 0.1f)
        {
            errorPanel.SetActive(true);
            errorText.text = "Jedite prije nego što odete u kupovinu";
        }
        else if (sd.playerHappiness <= 0.1f)
        {
            errorPanel.SetActive(true);
            errorText.text = "Previše vam je dosadno da bi išli u kupovinu";
        }
        else if (sd.playerThirst <= 0.1f)
        {
            errorPanel.SetActive(true);
            errorText.text = "Previše ste žedni da bi otišli u kupovinu";
        }
        else
        {
            SceneManager.LoadScene("DomaciProizvodi");
        }
    }

    public void PlayBosnjoJump()
    {
        PlayMusic();
        SaveData sd = FindObjectOfType<Gameplay>().GetSaveData();
        if (sd.playerHealth < 0.1f)
        {
            errorPanel.SetActive(true);
            errorText.text = "Niste dovoljno zdravi za rad";
            return;
        }

        if (sd.playerHunger < 0.1f)
        {
            errorPanel.SetActive(true);
            errorText.text = "Previše ste gladni za rad";
            return;
        }

        SceneManager.LoadScene("SkelaIntro");
    }

    public void PlayBosnjoSkela()
    {
        SceneManager.LoadScene("Scena");
    }

    // Takes 0.1 energy and health
    public void MemoryGamePlay()
    {
        PlayMusic();
        SceneManager.LoadScene("MemoryGame");
    }

    //Takes 0.1 energy and health
    public void FactoryGame()
    {
        PlayMusic();
        SaveData sd = FindObjectOfType<Gameplay>().GetSaveData();
        if (sd.playerHealth < 0.1f)
        {
            errorPanel.SetActive(true);
            errorText.text = "Niste dovoljno zdravi za rad";
            return;
        }

        if (sd.playerHunger < 0.1f)
        {
            errorPanel.SetActive(true);
            errorText.text = "Previše ste gladni za rad";
            return;
        }

        SceneManager.LoadScene("FabrikaGame");
    }

    //Takes 0.1 energy and health
    public void WorkFromHomeGame()
    {
        PlayMusic();
        SaveData sd = FindObjectOfType<Gameplay>().GetSaveData();
        if (sd.playerHealth < 0.1f)
        {
            errorPanel.SetActive(true);
            errorText.text = "Niste dovoljno zdravi za rad";
            return;
        }

        if (sd.playerHunger < 0.05f)
        {
            errorPanel.SetActive(true);
            errorText.text = "Previše ste gladni za rad";
            return;
        }

        if (sd.playerThirst < 0.05f)
        {
            errorPanel.SetActive(true);
            errorText.text = "Previše ste žedni za rad";
            return;
        }

        if (sd.playerHappiness < 0.05f)
        {
            errorPanel.SetActive(true);
            errorText.text = "Previše vam je dosadno za rad";
            return;
        }

        SceneManager.LoadScene("WorkFromHome01");
    }
}
