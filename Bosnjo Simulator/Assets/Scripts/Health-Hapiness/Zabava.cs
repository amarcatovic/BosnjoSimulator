using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Zabava : MonoBehaviour
{
    [SerializeField] float tripPrice = 5f;
    [SerializeField] float tripMinimalFunValue = 0.5f;
    [SerializeField] TextMeshProUGUI tripButtonText;
    [SerializeField] UnityEngine.UI.Slider tripSlider;

    [SerializeField] bool isPutovanje = false;
    [SerializeField] bool isMore = false;
    [SerializeField] bool isKoncert = false;
    [SerializeField] bool isKlub = false;
    [SerializeField] bool isUtakmica = false;
    [SerializeField] bool isIzlet = false;

    [SerializeField] GameObject error;
    public HappinessManager hm;

    Gameplay temp;
    SaveData sd;
    public string tripReport;

    private void Awake()
    {
        tripButtonText.text = "ZABAVI SE " + tripPrice.ToString("#.##") + "KM";
        hm = FindObjectOfType<HappinessManager>();
    }

    public void OnBuyTripButtonClick()
    {
        temp = FindObjectOfType<Gameplay>();
        sd = temp.GetSaveData();
        if (sd.playerMoney < tripPrice)
        {
            error.SetActive(true);
        }
        else
        {
            if (isPutovanje)
                DelekoPutovanje();

            if (isMore)
                More();

            if (isKoncert)
                Koncert();

            if (isKlub)
                Klub();

            if (isUtakmica)
                Utakmica();

            if (isIzlet)
                Izlet();
        }
    }

    private void Izadji()
    {
        SceneManager.LoadScene("HappinessEvent");
    }

    private void SacuvajIIzadji()
    {
        sd.playerMoney -= tripPrice;
        FindObjectOfType<GameSaveLogic>().Save();
        FindObjectOfType<HappinessManager>().SetTripReport(this.tripReport);
        Izadji();
    }

    private void Sacuvaj()
    {
        sd.playerMoney -= tripPrice;
        FindObjectOfType<GameSaveLogic>().Save();
        FindObjectOfType<HappinessManager>().SetTripReport(this.tripReport);
    }


    // -----------------------------------
    // ------------  PUTOVANJA -----------
    // -----------------------------------

    private void DelekoPutovanje()
    {
        string[] putovanja = { "na Tajland", "na Maldive", "na Havaje", "u Australiju", "na Arubu", "u Meksiko", "u Los Anđeles", "u New York" };
        string[] provod = { " fenomenalan", "nezaboravan", "prva liga", "majstorski", "legendaran", "dobar rođo moj" };
        string izabranoPutovanje = putovanja[UnityEngine.Random.Range(0, putovanja.Length)];
        float playerEnjoyement = UnityEngine.Random.Range(tripMinimalFunValue, 1f);
        sd.playerHappiness += playerEnjoyement;
        tripReport = sd.playerName + " putuje " + izabranoPutovanje + ". Provod je bio " + provod[UnityEngine.Random.Range(0, provod.Length)] + ". Zadovoljstvo " + (playerEnjoyement * 100).ToString("#") + "%";
        Sacuvaj();
        SceneManager.LoadScene("Odmara");
        return;

    }

    private void More()
    {
        string[] putovanja = { "u Neum", "u Dubrovnik", "u Crnu Goru", "u Bašku Vodu", "u Tučepe", "u Makarsku", "na Korčulu" };
        string[] provod = { "Brčkao se samo u plićaku", "Išao stalno do plovaka", "Pravio kule od pijeska na plaži", "Pocrnio ko ugalj", "Izgorio i mazao se pavlakom" };
        string izabranoPutovanje = putovanja[UnityEngine.Random.Range(0, putovanja.Length)];
        float playerEnjoyement = UnityEngine.Random.Range(tripMinimalFunValue, 1f);
        sd.playerHappiness += playerEnjoyement;
        tripReport = sd.playerName + " odlazi na more " + izabranoPutovanje + ". " + provod[UnityEngine.Random.Range(0, provod.Length)] + ". Zadovoljstvo " + (playerEnjoyement * 100).ToString("#") + "%";
        Sacuvaj();
        SceneManager.LoadScene("Odmara");
        return;
    }

    private void Koncert()
    {
        hm.tripType = "Koncert";
        string[] putovanja = { "Huletov", "Merlinov", "Jalin i Bubin", "Senidah-in" };
        string izabranoPutovanje = putovanja[UnityEngine.Random.Range(0, putovanja.Length)];
        float playerEnjoyement = UnityEngine.Random.Range(tripMinimalFunValue, 1f);
        sd.playerHappiness += playerEnjoyement;
        tripReport = sd.playerName + " odlazi na  " + izabranoPutovanje + " koncert. Ispjevao se i iskakao!" + " Zadovoljstvo " + (playerEnjoyement * 100).ToString("#") + "%";
        SacuvajIIzadji();
    }

    private void Klub()
    {
        hm.tripType = "Klub";
        float playerEnjoyement = UnityEngine.Random.Range(tripMinimalFunValue, 1f);
        sd.playerHappiness += playerEnjoyement;
        tripReport = sd.playerName + " odlazi u klub. Uživao je u zabavi i dobrom društvu." + " Zadovoljstvo " + (playerEnjoyement * 100).ToString("#") + "%";
        SacuvajIIzadji();
    }

    private void Utakmica()
    {
        hm.tripType = "Utakmica";
        string[] putovanja = { "Čelik Zenica", "Željezničar", "Sarajevo", "Velež Mostar", "Zrinjski" };
        string klub1 = putovanja[UnityEngine.Random.Range(0, putovanja.Length)];
        string klub2 = "";
        do
        {
            klub2 = putovanja[UnityEngine.Random.Range(0, putovanja.Length)];
        } while (klub1 == klub2);

        int rezultatDomaci = UnityEngine.Random.Range(0, 5);
        int rezultatGosti = UnityEngine.Random.Range(0, 5);

        float playerEnjoyement = UnityEngine.Random.Range(tripMinimalFunValue, 1f);
        sd.playerHappiness += playerEnjoyement;
        tripReport = sd.playerName + " odlazi na utakmicu " + klub1 + " - " + klub2 + ". Utakmica je bila dobra i završila je rezultatom " + rezultatDomaci.ToString() + ":" + rezultatGosti.ToString() + " Zadovoljstvo " + (playerEnjoyement * 100).ToString("#") + "%";
        SacuvajIIzadji();
    }

    private void Izlet()
    {
        hm.tripType = "Izlet";
        string[] putovanja = { "u prirodu", "da prošeta", "da trči", "da igra fudbal sa društvom" };
        string izabranoPutovanje = putovanja[UnityEngine.Random.Range(0, putovanja.Length)];
        float playerEnjoyement = UnityEngine.Random.Range(tripMinimalFunValue, 1f);
        sd.playerHappiness += playerEnjoyement;
        tripReport = sd.playerName + " odlazi na izlet " + izabranoPutovanje + ". Provod je bio fenomenalan!" + " Zadovoljstvo " + (playerEnjoyement * 100).ToString("#") + "%";
        SacuvajIIzadji();
    }
}
