using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VodoinstalaterEnd : MonoBehaviour
{

    [SerializeField] TMPro.TextMeshProUGUI moneyEarnedText;

    VodoinstalaterManager vm;
    Gameplay gameplay;
    SaveData sd;
    
    void Start()
    {
        vm = FindObjectOfType<VodoinstalaterManager>();
        gameplay = FindObjectOfType<Gameplay>();
        sd = gameplay.GetSaveData();

        if (vm.success)
        {
            // float moneyEarned = UnityEngine.Random.Range(100f, 1000f);
            float moneyEarned = 3.5f * vm.vrijeme;

            string text = "";
            gameplay.KreditManager(ref moneyEarned, ref text);

            moneyEarnedText.text = moneyEarned.ToString("#.##") + "KM";
            sd.playerMoney += moneyEarned;
            gameplay.SetNewActivity(sd.playerName + " uspješno završava posao vodoinstalatera. Zaradio je " + moneyEarned.ToString("#.##") + "KM. " + text);
            sd.playerHappiness -= 0.10f;
            FindObjectOfType<GameSaveLogic>().Save();
        }
        else
        {
            float moneyEarned = UnityEngine.Random.Range(50f, 200f);
            moneyEarnedText.text = "-" + moneyEarned.ToString("#.##");
            sd.playerMoney -= moneyEarned;
            gameplay.SetNewActivity(sd.playerName + " nije uspio završiti posao vodoinstalatera na vrijeme. Zadužio se " + moneyEarned.ToString("#.##") + "KM");
            sd.playerHappiness -= 0.25f;
            FindObjectOfType<GameSaveLogic>().Save();
        }
    }

    public void ReturnToMainGameButtonClick()
    {
        Destroy(vm);
        SceneManager.LoadScene("MainGame");
    }
    
}
