using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Help : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI helpText;
    [SerializeField] GameObject bosnjo1;

    [SerializeField] TextMeshProUGUI categoryText;

    [SerializeField] GameObject error;
    [SerializeField] GameObject confirmButton;
    [SerializeField] GameObject resetButton;
    [SerializeField] GameObject cancelButton;

    private Animator b1;

    private void Awake()
    {
        b1 = bosnjo1.GetComponent<Animator>();
    }

    private int i = 0;
    private string[] categoriesArray = { "", "Aktivnosti", "Nekretnine", "Posao", "Papiri", "Shopping" };
    private string[] helpTextArray = { "Cilj ove igre ti je da se brineš o meni", "Treba da se brineš da nisam gladan, žedan, bolestan i da mi bude zabavno", "Sve to imaš na vrhu opcije \"Aktivnosti\"",
       "Ti rođo moj znaš da ja volim ponekad otići do kladionice", "Tu možemo zajedno, u opciji \"Kladionica\", odigrati tiket, bingo ili kladiti se na utrke", "Ako prokockam pare ili ako mi jednostavno ponestane, u opciji \"Banka\" mogu podići kredit", "Hoćeš li mi biti žirant? hehe",
       "Kad poželim više da zaradim, onda me upiši na fakultet", "To je zadnja opcija u \"Aktivnosti\"",
       "Meni pare trebaju, pa u opciji \"Posao\" možeš me zaposliti", "Za svaki posao moram biti dovoljno zdrav, sit, hidriran i moram biti dobre volje", "Pošto u Njemačkoj pare padaju s neba, potrebno je da imam pasoš i vizu",
       "Sve to mogu da izganjam u opciji \"Papiri\"", "Kada odaberem tu opciju, otvori mi se WEB stranica Agencije za izdavanje dokumenata", "Prvo trebam izvaditi pasoš da bih dobio vizu", "Pri povratku iz Njemačke mi ona ističe",
       "U opciji \"Shopping\" možemo kupiti automobile i stanove koje pregledamo u opciji \"Nekretnine\"", "Automobilima se možemo vozati, a u stanovima praviti zabave" , "Dalje, u opciji \"Shopping\" možeš da brineš o mom izgledu tako što ćeš me fino obući",
       "Mora se nekada otići i do prodavnice po namirnice gdje volim da kupujem domaće", "Na taj način ostajem zdraviji", "Nek je sa srećom, živ bio!"
    };

    private string DajKategoriju(int i)
    {
        if (i == 0 || i == helpTextArray.Length - 1)
            return categoriesArray[0];
        if (i >= 1 && i <= 8 )
            return categoriesArray[1];
        if (i >= 9 && i <= 11)
            return categoriesArray[3];
        if (i >= 12 && i <= 15)
            return categoriesArray[4];
        if (i >= 16 && i <= 20)
            return categoriesArray[5];

        return "";
    }

    public void OnNextButtonClick()
    {
        b1.SetBool("Hi_01", true);
        i++;
        if(i > helpTextArray.Length - 1)
        {
            i = 0;
        }
        helpText.text = helpTextArray[i];
        categoryText.text = DajKategoriju(i);
    }

    public void OnPreviousButtonClick()
    {
        b1.SetBool("Hi_01", true);
        i--;
        if (i < 0)
        {
            i = helpTextArray.Length - 1;
        }
        helpText.text = helpTextArray[i];
        categoryText.text = DajKategoriju(i);
    }

    public void OnResetButtonCLick()
    {
        resetButton.SetActive(false);
        confirmButton.SetActive(true);
        cancelButton.SetActive(true);
    }

    public void DeleteSaveClick()
    {
        error.SetActive(true);
        FindObjectOfType<GameSaveLogic>().DeleteSave();
    }

    public void OnCancelButtonClick()
    {
        resetButton.SetActive(true);
        confirmButton.SetActive(false);
        cancelButton.SetActive(false);
    }

    public void OnConfirmButtonClick()
    {
        resetButton.SetActive(true);
        confirmButton.SetActive(false);
        cancelButton.SetActive(false);
    }
}
