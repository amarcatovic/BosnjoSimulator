using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BankaReturn : MonoBehaviour
{
    [SerializeField] TMP_InputField input;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI stanjeText;
    [SerializeField] TextMeshProUGUI kreditText;

    [SerializeField] GameObject errorPanel;
    [SerializeField] TextMeshProUGUI errorText;

    Gameplay gp;
    SaveData sd;

    void Start()
    {
        gp = FindObjectOfType<Gameplay>();
        sd = gp.GetSaveData();
        SceneTextManagment();
    }

    private void SceneTextManagment()
    {
        nameText.text = sd.playerName + " " + sd.playerSurname;
        stanjeText.text = "Stanje računa: " + sd.playerMoney.ToString("#.##");
        if (sd.hasKredit)
            kreditText.text = "Preostalo za vratiti: " + sd.kreditValue.ToString("#.##");
        else
            kreditText.text = "Nema Kredit";

        errorPanel.SetActive(false);
    }

    public void VratiKreditButtonClick()
    {
        if (!sd.hasKredit)
        {
            errorPanel.SetActive(true);
            errorText.text = sd.playerName + " nema kredit";
        }
        else
        {
            float unos;
            if(float.TryParse(input.text, out unos))
            {
                if(unos > sd.playerMoney)
                {
                    errorPanel.SetActive(true);
                    errorText.text = "Nemate toliko novca!";
                }
                else
                {
                    if(unos >= sd.kreditValue)
                    {
                        sd.playerMoney -= sd.kreditValue;
                        sd.hasKredit = false;
                        sd.kreditValue = 0;
                        gp.SetNewActivity(sd.playerName + " je vratio kredit!");
                    }
                    else
                    {
                        sd.playerMoney -= unos;
                        sd.kreditValue -= unos;
                        string dodatakNovca = "";
                        if(unos >= 1000f)
                        {
                            int dodatak = Random.Range(0, 100);
                            dodatakNovca = "Banka Vas nagrađuje sa " + dodatak.ToString() + "KM jer ste odgovoran klijent.";
                            sd.playerMoney += dodatak;
                        }

                        gp.SetNewActivity(sd.playerName + " vraća ratu kredita u vrijednosti od " + unos.ToString("#") + "KM. " + dodatakNovca);
                    }
                }

                FindObjectOfType<GameSaveLogic>().Save();
            }
            else
            {
                errorPanel.SetActive(true);
                errorText.text = "Pogrešan unos!";
            }
        }

        SceneTextManagment();
        input.text = "";
    }
}
